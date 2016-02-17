using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace PlainConcepts.LinqProviders
{
    class Proveedor: IQueryProvider
    {
        #region IQueryProvider Members

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression e)
        {
            Console.WriteLine("CREATE QUERY: " + e);

            MethodCallExpression mc = (MethodCallExpression) e;
            switch (mc.Method.Name)
            {
                case "Where":
                    // filtrar los elementos de la colección de entrada
                    // que satisfacen la condición
                    ClaseLinqQueryable<TElement> objeto1 =
                        (ClaseLinqQueryable<TElement>)((mc.Arguments[0] as ConstantExpression).Value);
                    LambdaExpression expr1 = 
                        (mc.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                    Func<TElement, bool> predicado = (Func<TElement, bool>)expr1.Compile();
                    ClaseLinqQueryable<TElement> resultadoW =
                        new ClaseLinqQueryable<TElement>();
                    foreach (TElement t in objeto1)
                    {
                        if (predicado(t))
                            resultadoW.Add(t);
                    }
                    return resultadoW;
                case "Select":
                    // transformar los elementos de la colección de entrada
                    ClaseLinqQueryable<Persona> objeto2 =
                        (ClaseLinqQueryable<Persona>)((mc.Arguments[0] as ConstantExpression).Value);
                    LambdaExpression expr2 =
                        (mc.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                    Func<Persona, TElement> trans = (Func<Persona, TElement>)expr2.Compile();
                    ClaseLinqQueryable<TElement> resultadoS =
                        new ClaseLinqQueryable<TElement>();
                    foreach (Persona p in objeto2)
                    {
                        resultadoS.Add(trans(p));
                    }
                    return resultadoS;
                case "OrderBy":
                    // ordenar los elementos de la colección de entrada
                    // según el criterio especificado
                    ClaseLinqQueryable<TElement> objeto3 =
                        (ClaseLinqQueryable<TElement>)((mc.Arguments[0] as ConstantExpression).Value);
                    LambdaExpression expr3 =
                        (mc.Arguments[1] as UnaryExpression).Operand as LambdaExpression;
                    Type tipo = expr3.Body.Type;

                    Delegate criterio = expr3.Compile();
                    // copiar la colección original
                    ClaseLinqQueryable<TElement> resultadoO =
                        new ClaseLinqQueryable<TElement>();
                    for (int i = 0; i < objeto3.Count; i++)
                        resultadoO.Add(objeto3[i]);
                    // ordenar
                    for (int i = 0; i < resultadoO.Count - 1; i++)
                        for (int j = i + 1; j < resultadoO.Count; j++)
                        {
                            IComparable c1 = (IComparable)criterio.DynamicInvoke(resultadoO[i]);
                            IComparable c2 = (IComparable)criterio.DynamicInvoke(resultadoO[j]);
                            if (c1.CompareTo(c2) > 0)
                            {
                                TElement tmp  = resultadoO[i];
                                resultadoO[i] = resultadoO[j];
                                resultadoO[j] = tmp;
                            }
                        }
                    return resultadoO;
                default:
                    throw new NotImplementedException("NO IMPLEMENTADO");
            }
        }

        public T Execute<T>(Expression e)
        {
            Console.WriteLine("EXECUTE: " + e);

            MethodCallExpression mc = (MethodCallExpression) e;
            switch (mc.Method.Name)
            {
                case "Count":
                    // contar cuántos (se ignora el posible argumento)
                    IEnumerable obj =
                        (IEnumerable)((mc.Arguments[0] as ConstantExpression).Value);
                    int n = 0;
                    foreach(var x in obj)
                        n++;
                    return (T) Convert.ChangeType(n, typeof(T));
                default:
                    throw new NotImplementedException("NO IMPLEMENTADO");
            }  
        }

        public IQueryable CreateQuery(Expression e)
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
