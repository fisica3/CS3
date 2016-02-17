using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using PlainConcepts.Clases.Futbol;

namespace Ejemplo10_01
{
    // tipo delegado a método que devuelve una secuencia
    delegate IEnumerable<string> ListaCadenas(string x);

    static class Program
    {
        static void Main(string[] args)
        {
            // Where()
            // nombres de futbolistas del Real Madrid con más de 30 años
            var s1 = from f in DatosFutbol.Futbolistas
                     where f.CodigoClub == "RMA" && f.Edad > 20
                     select f.Nombre;

            var s2 = DatosFutbol.Futbolistas.
                Where(f => f.CodigoClub == "RMA" && f.Edad > 30).
                Select(f => f.Nombre);

            var s3 = DatosFutbol.Futbolistas.
                OrderByDescending(f => f.Edad).
                Where((f, n) => f.CodigoClub == "RMA" && f.Edad > 30 && n < 10).
                Select(f => f.Nombre);

            var s4 = from f in DatosFutbol.Futbolistas
                     where f.CodigoClub == "BAR" && f.Posicion == Posicion.Delantero
                     select new { f.Nombre, f.FechaNac };

            var s5 = DatosFutbol.Futbolistas.
                Where(f => f.CodigoClub == "BAR" && 
                           f.Posicion == Posicion.Delantero).
                Select(f => new { f.Nombre, f.FechaNac });

            var s6 = DatosFutbol.Futbolistas.
                Select((f, i) => new { Futbolista = f, Indice = i }).
                Where(f => f.Futbolista.CodigoClub == "BAR" &&
                           f.Futbolista.Posicion == Posicion.Delantero).
                Select(x => x.Indice);

            var s7 = from c in DatosFutbol.Clubes
                     where c.Ciudad == "MADRID"
                     from f in DatosFutbol.Futbolistas
                     where f.CodigoClub == c.Codigo && f.Posicion == Posicion.Delantero
                     select new { Jugador = f.Nombre, Club = c.Nombre };

            var s8 = DatosFutbol.Clubes.
                     Where(c => c.Ciudad == "MADRID").
                     SelectMany(c => 
                         DatosFutbol.Futbolistas.
                         Where(f => f.CodigoClub == c.Codigo &&  f.Posicion == Posicion.Delantero).
                         Select(f => new { Jugador = f.Nombre, Club = c.Nombre }));

            var s8a = DatosFutbol.Clubes.
                     Where(c => c.Ciudad == "MADRID").
                     SelectMany(c =>
                         DatosFutbol.Futbolistas.
                         Where(f => f.CodigoClub == c.Codigo && f.Posicion == Posicion.Delantero),
                         (c, coll) => new { Jugador = coll.Nombre, Club = c.Nombre });

            var s9 = DatosFutbol.Clubes.
                    Where(c => c.Ciudad == "MADRID").
                    SelectMany(
                        // primer argumento - selección de colección
                        c =>
                        DatosFutbol.Futbolistas.
                        Where(f => f.CodigoClub == c.Codigo && f.Posicion == Posicion.Delantero).
                        Select(f => new { Jugador = f.Nombre, Club = c.Nombre }),
                       // segundo argumento - selección de resultado
                       (x, coll) => coll.Jugador + " (" + x.Nombre + ", " + x.Ciudad + ")");

            var s10 = from f in DatosFutbol.Futbolistas
                      orderby f.CodigoClub, f.Edad descending
                      select new { f.Nombre, f.CodigoClub, f.Edad };

            var s11 = DatosFutbol.Futbolistas.
                OrderBy(f => f.CodigoClub).
                ThenByDescending(f => f.Edad).
                Select( f => new { f.Nombre, f.CodigoClub, f.Edad });

            var s12 = DatosFutbol.Futbolistas.
                OrderBy(f => f.Nombre, 
                    StringComparer.CurrentCultureIgnoreCase).
                Select(f => f.Nombre);

            var s13 = DatosFutbol.Clubes.
                OrderBy(f => f.Nombre,
                    StringComparer.CurrentCultureIgnoreCase).
                Select(f => f.Nombre);

            // cuántos de cada edad
            IEnumerable<IGrouping<int?, string>> s14 =
                from f in DatosFutbol.Futbolistas
                group f.Nombre by f.Edad into g
                orderby g.Count() descending
                select g;

            IEnumerable<IGrouping<int?, string>> s15 =
                DatosFutbol.Futbolistas.GroupBy(f => f.Edad, f => f.Nombre);

            //foreach (var s in s15)
            //    Console.WriteLine(s.Key + " - " + s.Count());

            var s16 = from p in DatosFutbol.Paises
                      join f in DatosFutbol.Futbolistas
                          on p.Codigo equals f.CodigoPaisNac
                      // orderby p.Nombre, f.Nombre
                      select p.Nombre + " - " + f.Nombre;

            var s17 = DatosFutbol.Paises.
                Join(DatosFutbol.Futbolistas, 
                    p => p.Codigo, 
                    f => f.CodigoPaisNac, 
                    (p, f) => new { p, f }).  // entidad "artificial"
                OrderBy(x => x.p.Nombre).
                ThenBy(x => x.f.Nombre).
                Select(x => x.p.Nombre + " - " + x.f.Nombre);

            foreach (var s in s16)
                Console.WriteLine(s);

            var s18 = from p in DatosFutbol.Paises
                      join f in DatosFutbol.Futbolistas 
                          on p.Codigo equals f.CodigoPaisNac into pares
                      select new { Pais = p.Nombre, Cantidad = pares.Count() };

            var s19 = DatosFutbol.Paises.
                GroupJoin(DatosFutbol.Futbolistas,
                    p => p.Codigo,
                    f => f.CodigoPaisNac,
                    (p, pares) => new { Pais = p.Nombre, Cantidad =  pares.Count() } );

            // encuentro interno por la izquierda
            var s20 = from p in DatosFutbol.Paises
                      join f in DatosFutbol.Futbolistas
                          on p.Codigo equals f.CodigoPaisNac into pares
                      from par in pares.DefaultIfEmpty(null)
                      select new { Pais = p.Nombre, Jugador = (par == null ? "(Ninguno)" : par.Nombre) };

            var s21 = (from f in DatosFutbol.Futbolistas
                      group f.Edad by f.CodigoClub into g
                      orderby g.Average() descending
                      select new { Club = g.Key, Media = g.Average() }).Take(5);

            var s22 = (from f in DatosFutbol.Futbolistas
                       group f.Edad by f.CodigoClub into g
                       orderby g.Average() descending
                       select new { Club = g.Key, Media = g.Average() }).Skip(5);

            var s23 = (from f in DatosFutbol.Futbolistas
                       group f.Edad by f.CodigoClub into g
                       orderby g.Average() descending
                       select new { 
                           Club = g.Key, Media = g.Average() }).
                      TakeWhile(g => g.Media >= 25);

            var s24 = (from f in DatosFutbol.Futbolistas
                       group f.Edad by f.CodigoClub into g
                       orderby g.Average() descending
                       select new {
                          Club = g.Key, Media = g.Average() }).
                      SkipWhile(g => g.Media >= 25);

            var s25 = (from f in DatosFutbol.Futbolistas
                       select f.CodigoPaisNac).
                      Distinct().
                      Join(DatosFutbol.Paises,
                           c => c,      // identidad
                           p => p.Codigo,
                           (c, p) => p.Nombre).
                      OrderBy(n => n);

            var mad = JugadoresDeEquipo("RMA");
            var bcn = JugadoresDeEquipo("BAR");

            var enAlguno = mad.Union(bcn).OrderBy(x => x);
            var enAmbos = mad.Intersect(bcn).OrderBy(x => x);
            var soloMadrid = mad.Except(bcn).OrderBy(x => x);

            string[] s26 = 
                (from f in DatosFutbol.Futbolistas
                 where f.CodigoPaisNac == "FR"
                 join c in DatosFutbol.Clubes
                     on f.CodigoClub equals c.Codigo
                 select c.Nombre).Distinct().ToArray();

            List<string> s27 =
                (from f in DatosFutbol.Futbolistas
                 where f.CodigoPaisNac == "BR"
                 join c in DatosFutbol.Clubes
                     on f.CodigoClub equals c.Codigo
                 select c.Nombre).Distinct().ToList();

            Dictionary<int, int> año_cantJugadores =
                 (from f in DatosFutbol.Futbolistas
                  group f by f.FechaNac.Value.Year).
                 OrderBy(g => g.Key).
                 ToDictionary(
                     x => x.Key,
                     y => y.Count());

            Console.WriteLine("ToLookup");
            ILookup<int, Futbolista> año_Jugadores =
                 DatosFutbol.Futbolistas.ToLookup(
                     f => f.FechaNac.Value.Year,
                     f => f);

            int nacidos1977 = año_Jugadores[1977].Count();
            Console.WriteLine("En 1977 nacieron " + nacidos1977);

            foreach (var s in año_Jugadores)
                Console.WriteLine(s.Key + " - " + s.Count());

            ArrayList a = new ArrayList();
            a.Add("Uno");
            a.Add("Dos");
            a.Add("Tres");

            var consultaArrayList = from s in a.Cast<string>()
                                    where s.Length == 3
                                    select s;

            var s28 = from Futbolista f in DatosFutbol.Futbolistas
                      select new { f.Nombre, f.CodigoClub, f.Edad };

            var s29 = DatosFutbol.Futbolistas.
                      Cast<Futbolista>().
                      Select(f => new { f.Nombre, f.CodigoClub, f.Edad });

            //List<Persona> personas = null;
            //IEnumerable<Empleado> empleados = personas.OfType<Empleado>();

            ArrayList b = new ArrayList();
            b.Add("Uno");
            b.Add(2);
            b.Add("Tres");

            var consultaArrayList2 = from s in b.OfType<string>()
                                     where s.Length == 3
                                     select s;

            int[] cuadrados = Enumerable.Range(1, 100).
                Select(x => x * x).
                ToArray();

            int[] arr = Enumerable.Repeat(1, 500).ToArray();

            bool contiene25 = arr.Contains(25);

            IEnumerable<int> inv = Enumerable.Repeat(1, 10).Reverse();

            IEnumerable<Futbolista> ninguno = Enumerable.Empty<Futbolista>();

            IEnumerable<Futbolista> madrileños =
                (from f in DatosFutbol.Futbolistas where f.CodigoClub == "RMA" select f).
                Concat(
                    (from f in DatosFutbol.Futbolistas where f.CodigoClub == "ATM" select f)).
                Concat(
                    (from f in DatosFutbol.Futbolistas where f.CodigoClub == "GET" select f));
                    
            bool b1 = DatosFutbol.Futbolistas.Any(f => f.CodigoPaisNac == "CU");
            bool b2 = DatosFutbol.Futbolistas.All(f => f.Edad < 40);

            //Futbolista f1 = DatosFutbol.Futbolistas.First(f => f.Edad > 35);
            //Futbolista f2 = DatosFutbol.Futbolistas.First(f => f.Nombre == "RAUL");

            var s30 = (from f in DatosFutbol.Futbolistas
                       where f.Edad > 30
                       group f by f.CodigoClub into grupos
                       orderby grupos.Count() descending
                       select new { grupos.Key, Cant = grupos.Count() }).ElementAt(2).Key;

            int s31 = (from f in DatosFutbol.Futbolistas
                       where f.CodigoPaisNac == "DE"
                       select f).Count();

            var s32 = (from f in DatosFutbol.Futbolistas
                       group f.Edad by f.CodigoClub into grupos
                       select new { grupos.Key, Max = grupos.Max() });

            string letra =
              // "Man in the wilderness", STYX 1976
              @"Sometimes I feel like a man in the wilderness
                Like a lonely soldier of a war
                Sent away to die never quite knowing why
                Sometimes it makes no sense at all";
            
            var palabras = letra.Split(new char[] { ' ', '\t', '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries);

            var maxRepeticiones =
                (from p in palabras
                 group p by p.ToUpper() into pares
                 select new { pares.Key, Cant = pares.Count() }).Max(p => p.Cant);

            double[] arr3 = { 2.75, 4.15, 6.25 };

            var sumaCuadrados = arr3.Sum(x => x * x);

            var s33 = (from c in DatosFutbol.Clubes
                       orderby c.Nombre
                       select new { 
                           c.Nombre, 
                           MediaEdad = 
                               (from f in DatosFutbol.Futbolistas
                                where f.CodigoClub == c.Codigo
                                select f.Edad).Average() });

            foreach(var s in s33)
                Console.WriteLine(s);

            var s77 = from f in DatosFutbol.Futbolistas
                      where f.Edad < 25
                      select f;

            var s77a = DatosFutbol.Futbolistas.
                Where(f => f.Edad < 25).
                Select(f => f);

            int f5 = Factorial(5);

            Console.ReadLine();
        }

        // método que produce una secuencia de los
        // distintos países representados en un club determinado
        static ListaCadenas JugadoresDeEquipo =
            (codigo =>
                 (from f in DatosFutbol.Futbolistas
                  where f.CodigoClub == codigo
                  select f.CodigoPaisNac).Distinct());

        public static int Factorial(int n)
        {
            Debug.Assert(n > 0);
            return Enumerable.Range(1, n).
                Aggregate(
                    1,
                    (x, i) => x * i);
        }

    }

    class Persona { }
    class Empleado : Persona { }

    class ComparadorPrimeraLetra : IComparer<string>
    {
        public int Compare(string a, string b)
        {
            return a == null ? (b == null ? 0  : -1)
                             : (b == null ? +1 : 
                string.Compare(a[0].ToString(), b[0].ToString()));
        }
    }

    class TPar
    {
        public string Jugador;
        public string Club;
    };

}
