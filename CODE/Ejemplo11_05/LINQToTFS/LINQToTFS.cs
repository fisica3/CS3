using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace PlainConcepts.LinqToTfs
{
    public static class QueryableStore
    {
        public static QueryableItemStore<WorkItem> ToQueryable(this WorkItemStore wis)
        {
            return new QueryableItemStore<WorkItem>(wis);
        }
    }

    internal class OrderCriteria
    {
        public string FieldName
        {
            get; set;
        }
        public bool Ascending
        {
            get; set;
        }
    }

    internal static class Utils
    {
        private static Dictionary<string, string> translations = new Dictionary<string, string> {
                { "CreatedBy",     "[Created By]" },
                { "CreatedDate",   "[Created Date]" },
                { "Id",            "[Id]" },
                { "Title",         "[Title]" },
                { "Project.Name",  "[System.TeamProject]" }
                // some others...
            };

        public static string TranslateCondition(Expression condition)
        {
            if (condition == null)
                return "";
            switch (condition.NodeType)
            {
                case ExpressionType.Lambda:
                    {
                        Expression lambda = (condition as LambdaExpression).Body;
                        return TranslateCondition(lambda);
                    }

                // logical
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return TranslateCondition((condition as BinaryExpression).Left) +
                        " AND " +
                        TranslateCondition((condition as BinaryExpression).Right);
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return TranslateCondition((condition as BinaryExpression).Left) +
                        " OR " +
                        TranslateCondition((condition as BinaryExpression).Right);
                // relational
                case ExpressionType.Equal:
                    return TranslateCondition((condition as BinaryExpression).Left) +
                        " = " +
                        TranslateCondition((condition as BinaryExpression).Right);
                case ExpressionType.NotEqual:
                    return TranslateCondition((condition as BinaryExpression).Left) +
                        " <> " +
                        TranslateCondition((condition as BinaryExpression).Right);
                case ExpressionType.GreaterThan:
                    return TranslateCondition((condition as BinaryExpression).Left) +
                        " > " +
                        TranslateCondition((condition as BinaryExpression).Right);
                case ExpressionType.GreaterThanOrEqual:
                    return TranslateCondition((condition as BinaryExpression).Left) +
                        " >= " +
                        TranslateCondition((condition as BinaryExpression).Right);
                case ExpressionType.LessThan:
                    return TranslateCondition((condition as BinaryExpression).Left) +
                        " < " +
                        TranslateCondition((condition as BinaryExpression).Right);
                case ExpressionType.LessThanOrEqual:
                    return TranslateCondition((condition as BinaryExpression).Left) +
                        " <= " +
                        TranslateCondition((condition as BinaryExpression).Right);
                case ExpressionType.Constant:
                    {
                        ConstantExpression c = condition as ConstantExpression;
                        if (c.Type == typeof(string))
                            return "'" + c.Value.ToString() + "'";
                        else
                            return c.Value.ToString();
                    }
                case ExpressionType.MemberAccess:
                    {
                        MemberExpression m = condition as MemberExpression;
                        string name = m.ToString();
                        string pName = name.Substring(name.IndexOf('.') + 1);
                        return translations[pName];
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public static string TranslateOrderCriteria(OrderCriteria criteria)
        {
            return translations[criteria.FieldName] +
                (criteria.Ascending ? "" : " DESC");
        }
    }

    public class QueryableItemStore<WorkItem> : IOrderedQueryable<WorkItem>
    {
        private WorkItemStore _wis = null;

        public QueryableItemStore(WorkItemStore wis)
        {
            _wis = wis;
        }

        #region State

        private List<Expression> whereConditions = new List<Expression>();
        internal List<Expression> WhereConditions
        {
            get { return whereConditions; }
        }

        private List<OrderCriteria> orderFields = new List<OrderCriteria>();
        internal List<OrderCriteria> OrderFields
        {
            get { return orderFields; }
        }

        #endregion

        #region IQueryable Members

        public Type ElementType
        {
            get { return typeof(WorkItem); }
        }

        public Expression Expression
        {
            get { return Expression.Constant(this); }
        }

        public IQueryProvider Provider
        {
            get { return new QueryableItemProvider<WorkItem>(); }
        }

        #endregion

        #region IEnumerable<WorkItem> Members

        private IEnumerator<WorkItem> getEnumerator()
        {
            var entrada =
                (this.Expression as ConstantExpression).Value as QueryableItemStore<WorkItem>;

            // * generar consulta *
            StringBuilder sb = new StringBuilder("SELECT [Id] FROM WorkItems");
            if (entrada.WhereConditions.Count > 0)
            {
                sb.Append("\n WHERE ");
                foreach (Expression w in entrada.WhereConditions)
                    sb.Append("(" + Utils.TranslateCondition(w) + ") AND ");
                sb.Remove(sb.Length - 5, 5);
            }
            if (entrada.OrderFields.Count > 0)
            {
                sb.Append("\n ORDER BY ");
                foreach (OrderCriteria o in entrada.OrderFields)
                    sb.Append(Utils.TranslateOrderCriteria(o) + ", ");
                sb.Remove(sb.Length - 2, 2);
            }

            string WIQLSentence = sb.ToString();
            WorkItemCollection col = _wis.Query(WIQLSentence);
            foreach (WorkItem wi in col)
                yield return wi;
        }

        public IEnumerator<WorkItem> GetEnumerator()
        {
            return getEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return getEnumerator();
        }

        #endregion
    }

    public class QueryableItemProvider<WorkItem> : IQueryProvider
    {
        #region IQueryProvider Members

        private IQueryable<WorkItem> createQuery<WorkItem>(Expression e)
        {
            MethodCallExpression mc = e as MethodCallExpression;
            switch (mc.Method.Name)
            {
                case "Where":
                    {
                        QueryableItemStore<WorkItem> entrada =
                            (mc.Arguments[0] as ConstantExpression).Value as QueryableItemStore<WorkItem>;

                        LambdaExpression expr =
                            (mc.Arguments[1] as UnaryExpression).Operand as LambdaExpression;

                        entrada.WhereConditions.Add(expr);
                        return entrada;
                    }
                case "OrderBy":
                case "ThenBy":
                case "OrderByDescending":
                case "ThenByDescending":
                    {
                        QueryableItemStore<WorkItem> entrada =
                            (mc.Arguments[0] as ConstantExpression).Value as QueryableItemStore<WorkItem>;

                        LambdaExpression expr1 =
                            (mc.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                        string fName = (expr1.Body as MemberExpression).Member.Name;

                        entrada.OrderFields.Add(
                            new OrderCriteria { 
                                FieldName = fName, 
                                Ascending = ! mc.Method.Name.Contains("Descending") 
                            });
                        return entrada;
                    }
                case "Select":
                    throw new NotImplementedException("Select() not used here");
                default:
                    throw new NotImplementedException();
            }
        }

        public IQueryable<WorkItem> CreateQuery<WorkItem>(Expression e)
        {
            return createQuery<WorkItem>(e);
        }

        public IQueryable CreateQuery(Expression e)
        {
            return createQuery<WorkItem>(e);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
