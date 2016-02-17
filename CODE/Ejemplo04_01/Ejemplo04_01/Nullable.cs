using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ejemplo04_01
{
    public struct Nullable<T> where T : struct
    {
        // propiedad HasValue
        private bool hasValue;

        public bool HasValue
        {
            get { return hasValue; }
        }

        //propiedad Value
        private T value;

        public T Value
        {
            get
            {
                if (!hasValue)
                    throw new InvalidOperationException();
                return value;
            }
        }

        // constructor
        public Nullable(T valor)
        {
            this.value = valor;
            this.hasValue = true;
        }

        // otros métodos...
    }
}
