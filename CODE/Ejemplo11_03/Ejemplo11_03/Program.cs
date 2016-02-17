using System;
using System.Collections.Generic;

using PlainConcepts.LinqProviders;

namespace Ejemplo11_03
{
    class Program
    {
        struct Par
        {
            public int x, y;
            public override string ToString()
            {
                return "(" + x + ", " + y + ")";
            }
        }

        struct ParDigito
        {
            public int Digito;
            public string Nombre;
        }

        static void Main(string[] args)
        {
            ClaseLinq<int> lista = new ClaseLinq<int>();
            lista.AddRange(new[] { 1, 7, 5, 8, 3, 6, 2, 4, 9 });

            var q = from x in lista
                    where x > 3
                    orderby x descending
                    select x * x;

            foreach (int x in q)
                Console.WriteLine(x);

            var arr = new ClaseLinq<Par>() { 
                new Par() { x = 7, y = 3 },
                new Par() { x = 4, y = 3 },
                new Par() { x = 2, y = 4 },
                new Par() { x = 2, y = 1 },
                new Par() { x = 8, y = 8 },
                new Par() { x = 6, y = 7 },
                new Par() { x = 2, y = 3 },
                new Par() { x = 6, y = 3 },
                new Par() { x = 9, y = 3 },
                new Par() { x = 7, y = 2 },
                new Par() { x = 1, y = 5 },
                new Par() { x = 4, y = 5 },
                new Par() { x = 2, y = 2 },
                new Par() { x = 4, y = 1 },
            };

            Console.WriteLine("*** where, orderby, select");
            var r = from a in arr
                    where a.x > 1
                    orderby a.x descending, a.y
                    select new { a.x, a.y, suma = a.x + a.y };

            foreach(var x in r)
                Console.WriteLine(x);

            Console.WriteLine("*** join");
            var s = from a in arr
                    join b in arr on a.x equals b.y
                    orderby a.x, b.x
                    select new { a, b };

            foreach (var x in s)
                Console.WriteLine(x);

            Console.WriteLine("*** group by");
            var t = from a in arr
                    join b in arr on a.x equals b.y
                    group new { a, b } by a.x;

            foreach (var x in t)
            {
                Console.WriteLine("Clave: " + x.Key);
                Console.WriteLine("Grupo: ");
                foreach (var y in x)
                    Console.WriteLine(y);
            }

            Console.WriteLine("*** group/join");
            var u = from a in arr
                    join b in arr on a.x equals b.y into z
                    from zz in z 
                    select zz;

            foreach (var x in u)
            {
                Console.WriteLine(x);
            }

            // Logging enumerable

            LoggingEnumerable.Log = Console.Out;  // salida a la consola

            string[] nombres = { "Kerry", "Steve", "Phil", "Dave", "Rich", "Robbie" };

            var erres = from n in nombres
                        let pos = n.IndexOf('e')
                        where pos >= 0
                        orderby pos, n
                        select n.ToUpper();

            foreach (string erre in erres)
                Console.WriteLine(erre);

            int[] numeros = { 27, 43, 52, 87, 99, 45, 72, 29, 61, 58, 94 };
                        
            ParDigito[] digitos = 
              { 
                  new ParDigito { Digito = 0, Nombre = "Cero" },
                  new ParDigito { Digito = 1, Nombre = "Uno" },
                  new ParDigito { Digito = 2, Nombre = "Dos" },
                  new ParDigito { Digito = 3, Nombre = "Tres" },
                  new ParDigito { Digito = 4, Nombre = "Cuatro" },
                  new ParDigito { Digito = 5, Nombre = "Cinco" },
                  new ParDigito { Digito = 6, Nombre = "Seis" },
                  new ParDigito { Digito = 7, Nombre = "Siete" },
                  new ParDigito { Digito = 8, Nombre = "Ocho" },
                  new ParDigito { Digito = 9, Nombre = "Nueve" },
              };

            var grupos = from n in numeros
                         let terminacion = n % 10
                         join d in digitos on terminacion equals d.Digito
                         orderby d.Digito 
                         group n by d.Nombre;

            foreach (var g in grupos)
            {
                Console.WriteLine(g.Key);
                foreach (var elem in g)
                    Console.WriteLine("   " + elem);
            }

            Console.ReadLine();
        }
    }
}
