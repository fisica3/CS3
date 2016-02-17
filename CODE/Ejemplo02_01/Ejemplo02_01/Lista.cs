using System;
using System.Collections.Generic;
using System.Collections;

namespace PlainConcepts.Clases
{
    public class Lista<T>
    {
        private ArrayList lista = new ArrayList();

        public void Add(T t)
        {
            lista.Add(t);
        }

        public int Count
        {
            get { return lista.Count; }
        }

        public T this[int i]
        {
            get { return (T)lista[i]; }
        }
    }
}
