using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PlainConcepts.Clases;

namespace Ejemplo02_04
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 5, y = 7;
            Console.WriteLine("ANTES {0} {1}", x, y);
            MetodosGenericos.Swap<int>(ref x, ref y); // <int> es opcional
            Console.WriteLine("DESPUES {0} {1}", x, y);

            Persona tb1 = new Persona("Dick Tracy", SexoPersona.Varón);
            Persona tb2 = new Persona("Batman", SexoPersona.Varón);
            Console.WriteLine("El mayor es " +
                MetodosGenericos.Max(tb1, tb2));
            // *** equivale a
            Console.WriteLine("El mayor es " +
                MetodosGenericos.Max<Persona>(tb1, tb2));

            // sin genericidad
            Persona tb3 = (Persona)
                Activator.CreateInstance(typeof(Persona));
            // con genericidad
            Persona tb4 = Activator.CreateInstance<Persona>();
            tb4.Nombre = "Superman";

            int[] arr = { 25, 1, 9, 4, 16 };

            Array.Sort(arr);  // ordenar el array
            foreach (int i in arr)
                Console.WriteLine(i);

            if (Array.IndexOf(arr, 9) != -1)
                Console.WriteLine("El 9 está presente.");

            Action<int> imprimir = imprimirEntero; 
            for (int i = 0; i < 5; i++)
                imprimir.BeginInvoke(arr[i], null, null);

            foreach (int i in new NaturalNumbersSequence())
                Console.WriteLine(i);

            Console.ReadLine();
        }

        private static Random semilla = new Random();

        static void imprimirEntero(int n)
        {
            System.Threading.Thread.Sleep(semilla.Next(5000));
            Console.WriteLine(n);
        }
    }
}
