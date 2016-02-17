using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Ejemplo09_02
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            // versión "clásica"
            List<int> cumplen = new List<int>();
            for (int i1 = 1; i1 <= 9; i1++)
                for (int i2 = 0; i2 <= 9; i2++)
                    for (int i3 = 0; i3 <= 9; i3++)
                        for (int i4 = 0; i4 <= 9; i4++)
                            if (Condicion(i1, i2, i3, i4))
                                cumplen.Add(Numero(i1, i2, i3, i4));
            sw.Stop();
            foreach (int n in cumplen)
                Console.WriteLine(n);
            Console.WriteLine("Transcurrido: " + sw.ElapsedMilliseconds.ToString());

            sw.Start();
            // versión LINQ
            IEnumerable<int> cumplen2 =
                from j1 in Enumerable.Range(1, 9)
                from j2 in Enumerable.Range(0, 9)
                from j3 in Enumerable.Range(0, 9)
                from j4 in Enumerable.Range(0, 9)
                where Condicion(j1, j2, j3, j4)
                select Numero(j1, j2, j3, j4);
            sw.Stop();
            foreach (int n in cumplen2)
                Console.WriteLine(n);
            Console.WriteLine("Transcurrido: " + sw.ElapsedMilliseconds.ToString());

            Console.ReadLine();

        }

        static bool Condicion(int a, int b, int c, int d)
        {
            return Numero(a, b, c, d) == Cuarta(a) + Cuarta(b) + Cuarta(c) + Cuarta(d);
        }

        static int Numero(int a, int b, int c, int d) 
        { 
            return 1000 * a + 100 * b + 10 * c + d; 
        }

        static int Cuadrado(int a) { return a * a; }

        static int Cuarta(int a) { return Cuadrado(a) * Cuadrado(a); }

    }
}
