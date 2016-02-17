using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainConcepts.Clases
{
    public class Complejo : ParOrdenado<double>
    {
        public Complejo(double a, double b)
            : base(a, b)
        {
        }

        // operaciones aritméticas
        public static Complejo operator +(Complejo c1,
            Complejo c2)
        {
            return new Complejo(
                c1.Primero + c2.Primero,
                c1.Segundo + c2.Segundo);
        }
        public static Complejo operator *(Complejo c1,
            Complejo c2)
        {
            return new Complejo(
              c1.Primero * c2.Primero - c1.Segundo * c2.Segundo,
              c1.Primero * c2.Segundo + c1.Segundo * c2.Primero);
        }

        // conversión implícita
        public static implicit operator Complejo(double d)
        {
            return new Complejo(d, 0);
        }

        // igualdad
        public override bool Equals(object obj)
        {
            Complejo cObj = obj as Complejo;
            if ((object)cObj == null)
                return false;
            else
                return Primero == cObj.Primero &&
                       Segundo == cObj.Segundo;
        }
        public override int GetHashCode()
        {
            return ((int)Math.Sqrt(Primero * Primero +
                Segundo * Segundo)).GetHashCode();
        }
        public static bool operator ==(Complejo a, Complejo b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            return a.Primero == b.Primero &&
                   a.Segundo == b.Segundo;
        }
        public static bool operator !=(Complejo a, Complejo b)
        {
            return !(a == b);
        }

        // presentación como cadena
        public override string ToString()
        {
            return Primero.ToString() + " + " +
                Segundo.ToString() + "*i";
        }
    }
}
