using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace PlainConcepts.LinqProviders
{
    public static class LoggingEnumerable
    {
        // *** propiedad adicional

        public static TextWriter Log
        {
            set { log = value; }
        }
        private static TextWriter log = null;

        // *** métodos extensores para IEnumerable<T>

        public static IEnumerable<T> Where<T>(this IEnumerable<T> self,
            Func<T, bool> predicate)
        {
            if (log != null) log.WriteLine("WHERE/2");
            return Enumerable.Where(self, predicate);
        }

        public static IEnumerable<U> Select<T, U>(this IEnumerable<T> self, 
            Func<T, U> selector)
        {
            if (log != null) log.WriteLine("SELECT/2");
            return Enumerable.Select(self, selector);
        }

        public static IEnumerable<V> SelectMany<T, U, V>(
            this IEnumerable<T> self, 
            Func<T, IEnumerable<U>> selector,
            Func<T, U, V> resultSelector)
        {
            if (log != null) log.WriteLine("SELECTMANY/3");
            return Enumerable.SelectMany(self, selector, resultSelector);
        }

        public static IEnumerable<V> Join<T, U, K, V>(
            this IEnumerable<T> self,
            IEnumerable<U> inner,
            Func<T, K> outerKeySelector,
            Func<U, K> innerKeySelector,
            Func<T, U, V> resultSelector)
        {
            if (log != null) log.WriteLine("JOIN/5");
            return Enumerable.Join(self, inner, 
                outerKeySelector, innerKeySelector, resultSelector);
        }

        public static IEnumerable<V> GroupJoin<T, U, K, V>(
            this IEnumerable<T> self,
            IEnumerable<U> inner,
            Func<T, K> outerKeySelector,
            Func<U, K> innerKeySelector,
            Func<T, IEnumerable<U>, V> resultSelector)
        {
            if (log != null) log.WriteLine("GROUPJOIN/5");
            return Enumerable.GroupJoin(self, inner,
                outerKeySelector, innerKeySelector, resultSelector);
        }

        public static IOrderedEnumerable<T> OrderBy<T, K>(
            this IEnumerable<T> self,
            Func<T, K> keySelector) 
        {
            if (log != null) log.WriteLine("ORDERBY/2");
            return Enumerable.OrderBy(self, keySelector);
        }

        public static IOrderedEnumerable<T> OrderByDescending<T, K>(
            this IEnumerable<T> self,
            Func<T, K> keySelector) 
        {
            if (log != null) log.WriteLine("ORDERBYDESCENDING/2");
            return Enumerable.OrderByDescending(self, keySelector);
        }

        public static IOrderedEnumerable<T> ThenBy<T, K>(
            this IOrderedEnumerable<T> self,
            Func<T, K> keySelector)
        {
            if (log != null) log.WriteLine("THENBY/2");
            return Enumerable.ThenBy(self, keySelector);
        }

        public static IOrderedEnumerable<T> ThenByDescending<T, K>(
            this IOrderedEnumerable<T> self,
            Func<T, K> keySelector)
        {
            if (log != null) log.WriteLine("THENBYDESCENDING/2");
            return Enumerable.ThenByDescending(self, keySelector);
        }

        public static IEnumerable<IGrouping<K, T>> GroupBy<T, K>(
            this IEnumerable<T> self,
            Func<T, K> keySelector) 
        {
            if (log != null) log.WriteLine("GROUPBY/2");
            return Enumerable.GroupBy(self, keySelector);
        }

        public static IEnumerable<IGrouping<K, E>> GroupBy<T, K, E>(
            this IEnumerable<T> self,
            Func<T, K> keySelector,
            Func<T, E> elementSelector)
        {
            if (log != null) log.WriteLine("GROUPBY/3");
            return Enumerable.GroupBy(self, keySelector, elementSelector);
        }
    }
}
