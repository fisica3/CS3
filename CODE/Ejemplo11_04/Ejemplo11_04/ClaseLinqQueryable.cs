using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace PlainConcepts.LinqProviders
{
    class ClaseLinqQueryable<T> : List<T>, IOrderedQueryable<T>
    {
        #region IQueryable Members

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public Expression Expression
        {
            get { return Expression.Constant(this); }
        }

        public IQueryProvider Provider
        {
            get { return new Proveedor(); }
        }

        #endregion
    }
}
