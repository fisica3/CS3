using System;
using System.Collections.Generic;
using System.Linq;

using PlainConcepts.Clases;

namespace Ejemplo02_01
{
    class Program
    {
        static void Main(string[] args)
        {
            // un par de enteros
            ParOrdenado<int> p1 = new ParOrdenado<int>(4, 5);
            Console.WriteLine(p1.ToString());

            // un par de cadenas
            ParOrdenado<string> p2 =
                new ParOrdenado<string>("a", "b");
            Console.WriteLine(p2.ToString());

            Console.ReadLine();
        }
    }
}
