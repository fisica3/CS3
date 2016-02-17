using System;
using System.Collections.Generic;
using System.Linq;

namespace Ejemplo05_02
{
    class Program
    {
        static void Main(string[] args)
        {
            ClaseParcial p = new ClaseParcial();
            p.Nombre = "SHERLOCK";

            ClaseParcial.MetodoPublico(4);

            Console.ReadLine();
        }
    }
}
