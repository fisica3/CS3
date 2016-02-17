using System;
using System.Collections.Generic;
using System.Linq;

namespace PlainConcepts.LinqProviders
{
    class ClaseLinq<T>: List<T>
    {
        //private bool toggle = false;

        //public ClaseLinq<T> Where(Func<T, bool> predicate)
        //{
        //    ClaseLinq<T> result = new ClaseLinq<T>();
        //    foreach (T t in this)
        //        if (predicate(t))
        //        {
        //            if (toggle) result.Add(t);
        //            toggle = !toggle;
        //        }
        //    return result;
        //}

        public ClaseLinq<T> Where(Func<T, bool> predicate)
        {
            ClaseLinq<T> result = new ClaseLinq<T>();
            foreach (T t in this)
                if (predicate(t))
                    result.Add(t);
            return result;
        }

        public ClaseLinq<U> Select<U>(Func<T, U> selector)
        {
            ClaseLinq<U> result = new ClaseLinq<U>();
            foreach (T t in this)
                result.Add(selector(t));
            return result;
        }

        public ClaseLinq<V> SelectMany<U, V>(Func<T, ClaseLinq<U>> selector,
            Func<T, U, V> resultSelector)
        {
            ClaseLinq<V> result = new ClaseLinq<V>();
            foreach (T t in this)
            {
                ClaseLinq<U> lista2 = selector(t);
                foreach (U u in lista2)
                    result.Add(resultSelector(t, u));
            }
            return result;
        }

        public ClaseLinq<V> Join<U, K, V>(ClaseLinq<U> inner,
            Func<T, K> outerKeySelector,
            Func<U, K> innerKeySelector,
            Func<T, U, V> resultSelector)
        {
            Dictionary<K, List<U>> dict = new Dictionary<K,List<U>>();
            foreach (U u in inner)
            {
                K clave2 = innerKeySelector(u);
                if (dict.ContainsKey(clave2))
                    dict[clave2].Add(u);
                else
                {
                    List<U> list2 = new List<U>();
                    list2.Add(u);
                    dict.Add(clave2, list2);
                }
            }
            ClaseLinq<V> result = new ClaseLinq<V>();
            foreach (T t in this)
            {
                K clave1 = outerKeySelector(t);
                if (dict.ContainsKey(clave1))
                    foreach (U u in dict[clave1])
                        result.Add(resultSelector(t, u));
            }
            return result;
        }

        public ClaseLinq<V> GroupJoin<U, K, V>(ClaseLinq<U> inner,
            Func<T, K> outerKeySelector,
            Func<U, K> innerKeySelector,
            Func<T, ClaseLinq<U>, V> resultSelector)
        {
            Dictionary<K, ClaseLinq<U>> dict = new Dictionary<K, ClaseLinq<U>>();
            foreach (U u in inner)
            {
                K clave2 = innerKeySelector(u);
                if (dict.ContainsKey(clave2))
                    dict[clave2].Add(u);
                else
                {
                    ClaseLinq<U> list2 = new ClaseLinq<U>();
                    list2.Add(u);
                    dict.Add(clave2, list2);
                }
            }
            ClaseLinq<V> result = new ClaseLinq<V>();
            foreach (T t in this)
            {
                K clave1 = outerKeySelector(t);
                if (dict.ContainsKey(clave1))
                    result.Add(resultSelector(t, dict[clave1]));
                else
                    result.Add(resultSelector(t, new ClaseLinq<U>()));
            }
            return result;
        }

        public ClaseLinqOrdenada<T> OrderBy<K>(Func<T, K> keySelector) 
        {
            ClaseLinqOrdenada<T> result = new ClaseLinqOrdenada<T>();
            foreach (T t in this)
                result.Add(t);
            // ordenar
            for (int i = 0; i < result.Count - 1; i++)
                for (int j = i + 1; j < result.Count; j++)
                    if (Comparer<K>.Default.Compare(keySelector(result[i]), keySelector(result[j])) > 0)
                    {
                        T tmp = result[i];
                        result[i] = result[j];
                        result[j] = tmp;
                    }
            // preparar array de relaciones con anterior
            if (result.Count > 0)
            {
                result.igualAlAnterior.Add(false);
                for (int i = 1; i < result.Count; i++)
                    result.igualAlAnterior.Add(
                        Comparer<K>.Default.Compare(keySelector(result[i]), keySelector(result[i - 1])) == 0);
            }
            return result;
        }

        public ClaseLinqOrdenada<T> OrderByDescending<K>(Func<T, K> keySelector) 
        {
            ClaseLinqOrdenada<T> result = new ClaseLinqOrdenada<T>();
            foreach (T t in this)
                result.Add(t);
            // ordenar
            for (int i = 0; i < result.Count - 1; i++)
                for (int j = i + 1; j < result.Count; j++)
                    if (Comparer<K>.Default.Compare(keySelector(result[i]), keySelector(result[j])) < 0)
                    {
                        T tmp = result[i];
                        result[i] = result[j];
                        result[j] = tmp;
                    }
            // preparar array de relaciones con anterior
            if (result.Count > 0)
            {
                result.igualAlAnterior.Add(false);
                for (int i = 1; i < result.Count; i++)
                    result.igualAlAnterior.Add(
                        Comparer<K>.Default.Compare(keySelector(result[i]), keySelector(result[i - 1])) == 0);
            }
            return result;
        }

        public ClaseLinq<GrupoLinq<K, T>> GroupBy<K>(Func<T, K> keySelector) 
        {
            Dictionary<K, ClaseLinq<T>> dict = new Dictionary<K, ClaseLinq<T>>();
            foreach (T t in this)
            {
                K clave = keySelector(t);
                if (dict.ContainsKey(clave))
                    dict[clave].Add(t);
                else
                {
                    ClaseLinq<T> list2 = new ClaseLinq<T>();
                    list2.Add(t);
                    dict.Add(clave, list2);
                }
            }
            ClaseLinq<GrupoLinq<K, T>> result = new ClaseLinq<GrupoLinq<K, T>>();
            foreach (K k in dict.Keys)
            {
                GrupoLinq<K, T> g = new GrupoLinq<K, T>(k);
                foreach (T t in dict[k])
                    g.Add(t);
                result.Add(g);
            }
            return result;
        }

        public ClaseLinq<GrupoLinq<K, E>> GroupBy<K, E>(Func<T, K> keySelector,
            Func<T, E> elementSelector)
        {
            Dictionary<K, ClaseLinq<E>> dict = new Dictionary<K, ClaseLinq<E>>();
            foreach (T t in this)
            {
                E elem = elementSelector(t);
                K clave = keySelector(t);
                if (dict.ContainsKey(clave))
                    dict[clave].Add(elem);
                else
                {
                    ClaseLinq<E> list2 = new ClaseLinq<E>();
                    list2.Add(elem);
                    dict.Add(clave, list2);
                }
            }
            ClaseLinq<GrupoLinq<K, E>> result = new ClaseLinq<GrupoLinq<K, E>>();
            foreach (K k in dict.Keys)
            {
                GrupoLinq<K, E> g = new GrupoLinq<K, E>(k);
                foreach (E e in dict[k])
                    g.Add(e);
                result.Add(g);
            }
            return result;
        }
    }

    class ClaseLinqOrdenada<T> : ClaseLinq<T>
    {
        // guardar relación entre cada elemento y su predecesor
        // esta info la utilizarán los operadores ThenBy() subsiguientes
        internal List<bool> igualAlAnterior = new List<bool>();

        public ClaseLinqOrdenada<T> ThenBy<K>(Func<T, K> keySelector)
        {
            ClaseLinqOrdenada<T> result = new ClaseLinqOrdenada<T>();
            for (int i = 0; i < this.Count; i++)
            {
                result.Add(this[i]);
                result.igualAlAnterior.Add(this.igualAlAnterior[i]);
            }

            if (result.Count > 0)
            {
                int i = 0; 
                while (i < result.Count)
                {
                    int j = i + 1;
                    if (j < result.Count && result.igualAlAnterior[j])
                    {
                        while (j < result.Count && result.igualAlAnterior[j])
                        {
                            int res = Comparer<K>.Default.Compare(keySelector(result[i]), keySelector(result[j]));
                            if (res > 0)
                            {
                                T tmp = result[i];
                                result[i] = result[j];
                                result[j] = tmp;
                            }
                            j++;
                        }
                        result.igualAlAnterior[i + 1] =
                            Comparer<K>.Default.Compare(keySelector(result[i]), keySelector(result[i + 1])) == 0;
                        }
                    i++;
                }
            }

            return result;
        }

        public ClaseLinqOrdenada<T> ThenByDescending<K>(Func<T, K> keySelector)
        {
            ClaseLinqOrdenada<T> result = new ClaseLinqOrdenada<T>();
            for (int i = 0; i < this.Count; i++)
            {
                result.Add(this[i]);
                result.igualAlAnterior.Add(this.igualAlAnterior[i]);
            }

            if (result.Count > 0)
            {
                int i = 0;
                while (i < result.Count)
                {
                    int j = i + 1;
                    if (j < result.Count && result.igualAlAnterior[j])
                    {
                        while (j < result.Count && result.igualAlAnterior[j])
                        {
                            int res = Comparer<K>.Default.Compare(keySelector(result[i]), keySelector(result[j]));
                            if (res < 0)
                            {
                                T tmp = result[i];
                                result[i] = result[j];
                                result[j] = tmp;
                            }
                            j++;
                        }
                        result.igualAlAnterior[i + 1] =
                            Comparer<K>.Default.Compare(keySelector(result[i]), keySelector(result[i + 1])) == 0;
                    }
                    i++;
                }
            }

            return result;
        }
    }

    class GrupoLinq<K, T> : ClaseLinq<T>
    {
        private K key;

        public K Key 
        {
            get
            {
                return key;
            }
        }

        public GrupoLinq(K k)
        {
            key = k;
        }
    }

}
