using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ejemplo04_01
{
    class Program
    {
        static void Main(string[] args)
        {
            //int? n = null;
            //int? m = 275;

            int i = 27;
            int? j = i;         // int -> int?, Ok
            double? x = i;      // int -> double -> double?, Ok
            double? y = j;      // int? -> double?, Ok
            //int? k = y;         // ERROR DE COMPILACION
            int? l = (int?)y;   // double? -> int?, OK
            int m = (int)y;     // double? -> int? -> int
                                //   excepción si y es null

            int? a = 27;
            int? b = 50;
            int? c = null;
            int? d = a + b;     // d = 77
            int? e = a + c;     // e = null

            bool b1 = a < b;    // true
            bool b2 = a < c;    // false (algún operando es null)
            bool b3 = a == c;   // false (uno de los dos es null)
            bool b4 = c == null;   // true

            int? f = a ?? b;    // f = 27
            int? g = c ?? b;    // g = 50

            // boxing, etc.
            int? w = null;
            Console.WriteLine(w.ToString());  // imprime la cadena vacía

            object o = w;
            Console.WriteLine(o.ToString());  // produce excepción

            Console.ReadLine();
        }

        static void imprimir(int? x)
        {
            if (x.HasValue)
                Console.WriteLine(x.Value);
            else
                Console.WriteLine("??");
        }
    }
}
