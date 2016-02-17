using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Ejemplo05_02
{
    public partial class ClaseParcial
    {
        // definiciones
        static partial void MetodoParcial2()
        {
            Console.WriteLine("Llamada a MetodoParcial2");
        }

        private static int Calculo(int n)
        {
            int x = 0;
            // cálculo que tarda cierto tiempo...
            return x;
        }

        public static void MetodoPublico(int n)
        {
            MetodoParcial1(Calculo(n)); 
            MetodoParcial2(); 
        }

        private static Stopwatch mon = new Stopwatch();

        partial void OnCambioNombre(string viejo, string nuevo)
        {
            lock (mon)
            {
                Console.WriteLine("Antes:   " + viejo);
                Console.WriteLine("Después: " + nuevo);
            }
        }
    }
}
