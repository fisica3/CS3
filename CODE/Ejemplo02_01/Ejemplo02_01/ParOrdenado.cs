using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainConcepts.Clases
{
    public class ParOrdenado<T>
    {
        // campos
        private T primero = default(T);
        private T segundo = default(T);

        // propiedades
        public T Primero
        {
            get { return primero; }
            set { primero = value; }
        }
        public T Segundo
        {
            get { return segundo; }
            set { segundo = value; }
        }

        // constructores
        public ParOrdenado(T primero, T segundo)
        {
            Primero = primero;
            Segundo = segundo;
        }

        // métodos
        public override string ToString()
        {
            return "(" + Primero.ToString() + ", " +
              Segundo.ToString() + ")";
        }

        // indizador (solo lectura)
        public T this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return Primero;
                    case 1:
                        return Segundo;
                    default:
                        throw new Exception("ParOrdenado[]");
                }
            }
        }
    }
}
