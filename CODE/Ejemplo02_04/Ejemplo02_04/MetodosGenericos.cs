using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainConcepts.Clases
{
    public static class MetodosGenericos
    {
        public static void Swap<T>(ref T x1, ref T x2)
        {
            T temp = x1;
            x1 = x2;
            x2 = temp;
        }

        public static T Max<T>(T x1, T x2) where T : IComparable
        {
            if (x1.CompareTo(x2) >= 0)
                return x1;
            else
                return x2;
        }

        public static T Min<T>(T x1, T x2) where T : IComparable
        {
            if (x1.CompareTo(x2) <= 0)
                return x1;
            else
                return x2;
        }
    }
}
