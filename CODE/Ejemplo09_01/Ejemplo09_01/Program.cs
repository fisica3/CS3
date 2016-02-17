using System;
using System.Collections.Generic;
using System.Linq;
// using PlainConcepts.LinqProviders;

using System.Reflection;
using System.IO;

using PlainConcepts.Clases;
using FindFiles;

namespace Ejemplo09_01
{
    class TipoMeses
    {
        public string Nombre { get; set; }
        public int CantidadDias { get; set; }
    };
 
    class Program
    {
        static void Main(string[] args)
        {
            // colección de objetos en memoria
            List<Persona> Generación = new List<Persona> { 
                new Persona { Nombre = "Diana",
                    Sexo = SexoPersona.Mujer,
                    FechaNac = new DateTime(1996, 2, 4),
                    CodigoPaisNac = "ES" },
                new Persona { Nombre = "Dennis",
                    Sexo = SexoPersona.Varón,
                    FechaNac = new DateTime(1983, 12, 27),
                    CodigoPaisNac = "RU" },
                new Persona { Nombre = "Claudia",
                    Sexo = SexoPersona.Mujer,
                    FechaNac = new DateTime(1989, 7, 26),
                    CodigoPaisNac = "CU" },
                new Persona { Nombre = "Jennifer",
                    Sexo = SexoPersona.Mujer,
                    FechaNac = new DateTime(1982, 8, 12),
                    CodigoPaisNac = "CU" },
                //new Persona { Nombre = "JOHN DOE",
                //    Sexo = SexoPersona.Varón,
                //    FechaNac = new DateTime(0001, 1, 1),
                //    CodigoPaisNac = "?" },
            };

            List<Pais> Paises = new List<Pais> {
                new Pais("ES", "ESPAÑA"),
                new Pais("CU", "CUBA"),
                new Pais("RU", "RUSIA"),
                new Pais("US", "ESTADOS UNIDOS")
            };

            var mayores20 = from h in Generación
                            where h.Edad > 20
                            orderby h.Nombre
                            select new { Nombre = h.Nombre.ToUpper(), Edad = h.Edad };

            //var mayores20	= Generación
            //    .Where(h => h.Edad > 20)
            //    .OrderBy(h => h.Nombre)
            //    .Select(h => new { Nombre = h.Nombre.ToUpper(), Edad = h.Edad });

            //var tmp1 = Generación.Where(h => h.Edad > 20);
            //var tmp2 = tmp1.OrderBy(h => h.Nombre);
            //var mayores20 = tmp2.Select(h => new { Nombre = h.Nombre.ToUpper(), Edad = h.Edad });

            foreach (var h in mayores20)
                Console.WriteLine(h.Nombre + " (" + h.Edad + ")");

            int[] arr1 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // lo siguiente produce 81, 49, 25, 9, 1
            var inversa =
                (from n in arr1 where n % 2 == 1 select n * n).Reverse();
            foreach (var x in inversa)
                Console.WriteLine(x);

            string s = "AURELIO TRABAJA EN MICROSOFT";

            // produce en orden alfabético las vocales de la cadena 's'
            var s1 = from c in s
                     where "AEIOU".IndexOf(c) >= 0
                     orderby c
                     select c;
            ObjectDumper.Write(s1);

            // cuenta los espacios en la cadena 's'
            int n1 = (from c in s
                      where c == ' '
                      select c).Count();
            ObjectDumper.Write(n1);

            // palabras diferentes en una oración
            var n2 = (from w in s.Split(new char[] { ' ', '\t', '\n' },
                         StringSplitOptions.RemoveEmptyEntries)
                      orderby w.ToLower()
                      select w.ToLower()).Distinct();
            ObjectDumper.Write(n2);

            // produce una secuencia con los pares en arr1
            var pares = from n in arr1
                        where n % 2 == 0
                        select n;
            // también pudo ser:
            var pares2 = arr1.Where(n => n % 2 == 0);

            // produce la suma de los números de la secuencia
            int suma = arr1.Sum();
            // lo mismo que:
            int suma2 = (from n in arr1 select n).Sum();

            // produce los números de la secuencia, incrementados en 1
            var otra = from n in arr1
                       select n + 1;
            // lo mismo que:
            var otra2 = arr1.Select(n => n + 1);

            string[] ciudades = { "HAVANA", "MADRID", "NEW YORK", "MIAMI", "SEATTLE" };
            // ciudades con más de seis letras,
            // en orden alfabético
            var c7 = from c in ciudades
                     where c.Length > 6
                     orderby c
                     select c;

            var meses = new TipoMeses[] {
                new TipoMeses { Nombre = "Enero", CantidadDias = 31 },
                new TipoMeses { Nombre = "Febrero", CantidadDias = 28 },
                new TipoMeses { Nombre = "Marzo", CantidadDias = 31 },
                new TipoMeses { Nombre = "Abril", CantidadDias = 31 },
                new TipoMeses { Nombre = "Mayo", CantidadDias = 31 },
                new TipoMeses { Nombre = "Junio", CantidadDias = 31 },
                new TipoMeses { Nombre = "Julio", CantidadDias = 31 },
                new TipoMeses { Nombre = "Agosto", CantidadDias = 31 },
                new TipoMeses { Nombre = "Septiembre", CantidadDias = 31 },
                new TipoMeses { Nombre = "Octubre", CantidadDias = 31 },
                new TipoMeses { Nombre = "Noviembre", CantidadDias = 31 },
                new TipoMeses { Nombre = "Diciembre", CantidadDias = 31 },
            };

            var meses31 = from m in meses
                          where m.CantidadDias == 31
                          select m.Nombre;

            // *** PERSONAS

            // nombres que empiezan por 'D'
            var hijos = from h in Generación
                        where h.Nombre.StartsWith("D")
                        select h.Nombre;
            ObjectDumper.Write(hijos);

            // lista ordenada, primero por sexos
            // luego por edad en orden ascendente
            var orden = from h in Generación
                        orderby h.Sexo, h.Edad descending
                        select h;
            ObjectDumper.Write(orden);

            // ** EVALUACION DIFERIDA

            Console.WriteLine("Primera evaluación");
            ObjectDumper.Write(hijos);

            // se modifica la colección subyacente
            Generación[0].Nombre = "Elisa";

            Console.WriteLine("Segunda evaluación");
            ObjectDumper.Write(hijos);

            // ** EVALUACION INMEDIATA

            var hijos_cache = hijos.ToList();

            Console.WriteLine("Primera evaluación");
            ObjectDumper.Write(hijos_cache);

            // se modifica la colección subyacente
            Generación[0].Nombre = "Diana";

            Console.WriteLine("Segunda evaluación");
            ObjectDumper.Write(hijos_cache);

            /* PRODUCTO CARTESIANO */
            var pc1 = from pa in Paises
                      from pe in Generación
                      select new { pa.Nombre, Nombre2 = pe.Nombre };
            ObjectDumper.Write(pc1);

            var pc2 = from p1 in Generación
                      from p2 in Generación
                      where p1.Sexo == SexoPersona.Varón && p2.Sexo == SexoPersona.Mujer
                      select new { El = p1.Nombre, Ella = p2.Nombre };
            ObjectDumper.Write(pc2);

            var pc3 = from p1 in Generación
                      where p1.Sexo == SexoPersona.Varón
                      from p2 in Generación
                      where p2.Sexo == SexoPersona.Mujer
                      select new { El = p1.Nombre, Ella = p2.Nombre };
            ObjectDumper.Write(pc3);

            /* ENCUENTRO */
            var enc1 = from pa in Paises
                       join pe in Generación on pa.Codigo equals pe.CodigoPaisNac
                       orderby pa.Nombre
                       select new { pa.Nombre, Nombre2 = pe.Nombre };
            ObjectDumper.Write(enc1);

            var pc4 = from pa in Paises
                      from pe in Generación
                      where pa.Codigo == pe.CodigoPaisNac
                      select new { pa.Nombre, Nombre2 = pe.Nombre };
            ObjectDumper.Write(pc2);

            /* GRUPOS */
            var gruposSexo =
                from h in Generación
                group new { h.Nombre, h.Edad } by h.Sexo;
            ObjectDumper.Write(gruposSexo, 2);

            var gruposSexo2 =
                Generación.GroupBy(h => h.Sexo,
                    h => new { h.Nombre, h.Edad });
            ObjectDumper.Write(gruposSexo2, 2);

            foreach (var hh in gruposSexo)
            {
                // el valor de la clave
                Console.WriteLine(hh.Key);
                // los elementos del grupo
                foreach (var hhh in hh)
                    Console.WriteLine(" - " + hhh.Nombre + " (" + hhh.Edad + ")");
            }

            Console.WriteLine("GRUPOS POR PAISES");
            var gruposPaises =
                from pa in Paises
                join pe in Generación on pa.Codigo equals pe.CodigoPaisNac
                group new { pe.Nombre, pe.Edad } by pa.Nombre;

            var gruposPaises2 =
                (from pa in Paises
                 join pe in Generación on pa.Codigo equals pe.CodigoPaisNac
                 group new { pe.Nombre, pe.Edad } by pa.Nombre).
                OrderBy(g => g.Key);

            var gruposPaises3 =
                (from pa in Paises
                 join pe in Generación on pa.Codigo equals pe.CodigoPaisNac
                 group new { pe.Nombre, pe.Edad } by pa.Nombre).
                OrderByDescending(g => g.Count());

            var gruposPaises4 =
                from pa in Paises
                join pe in Generación on pa.Codigo equals pe.CodigoPaisNac
                group new { pe.Nombre, pe.Edad } by pa.Nombre
                into tmp
                orderby tmp.Key
                select tmp;

            var resumenPaises =
                from pa in Paises
                join pe in Generación on pa.Codigo equals pe.CodigoPaisNac
                group new { pe.Nombre, pe.Edad } by pa.Nombre
                into tmp
                orderby tmp.Count() descending
                select new { Pais = tmp.Key, Cantidad = tmp.Count() };

            var resumenPaises2 =
                from pa in Paises
                orderby pa.Nombre
                join pe in Generación on pa.Codigo equals pe.CodigoPaisNac
                into gp
                select new { Pais = pa.Nombre, Cantidad = gp.Count() };
            ObjectDumper.Write(resumenPaises2);

            var resumenPaises3 =
                Paises.OrderBy(pa => pa.Nombre).
                GroupJoin(Generación,
                    pa => pa.Codigo,
                    pe => pe.CodigoPaisNac,
                    (p, gp) => new { Pais = p.Nombre, Cantidad = gp.Count() });

            foreach (var hh in gruposPaises4)
            {
                // el valor de la clave
                Console.WriteLine(hh.Key);
                // los elementos del grupo
                foreach (var hhh in hh)
                    Console.WriteLine(" - " + hhh.Nombre +
                                      " (" + hhh.Edad + ")");
            }

            var into1 = from h in Generación
                        orderby h.Nombre
                        select new
                               {
                                   Nombre = h.Nombre,
                                   CodPais = h.CodigoPaisNac
                               }
                        into tmp
                        from p in Paises
                        where tmp.CodPais == p.Codigo
                        select tmp.Nombre + " - " + p.Nombre;

            var into2 = from tmp in
                            (
                            from h in Generación
                            orderby h.Nombre
                            select new
                                   {
                                       Nombre = h.Nombre,
                                       CodPais = h.CodigoPaisNac
                                   }
                            )
                        from p in Paises
                        where tmp.CodPais == p.Codigo
                        select tmp.Nombre + " - " + p.Nombre;

            /*LET */
            Console.WriteLine("LET");
            var let1 = from p in Generación
                       let media = Generación.Average(pp => pp.Edad)
                       where p.Edad >= media
                       select p.Nombre;
            ObjectDumper.Write(let1);

            var let2 = from p in Generación
                       let media = (from p2 in Generación 
                                    where p2.CodigoPaisNac == p.CodigoPaisNac 
                                    select p2).Average(pp => pp.Edad)
                       where p.Edad >= media
                       select p.Nombre;
            ObjectDumper.Write(let2);

            int[] numeros = { 27, 43, 52, 87, 99, 45, 72, 29, 61, 58, 94 };

            var grupos = from n in numeros
                         let terminacion = n % 10
                         orderby terminacion 
                         group n by terminacion;

    var grupos2 = numeros.
        Select(n => new { n, terminacion = n % 10 }).
        OrderBy(x => x.terminacion).
        GroupBy(x => x.terminacion, x => x.n);
            foreach (var g in grupos2)
            {
                Console.WriteLine(g.Key);
                foreach (var elem in g)
                    Console.WriteLine("   " + elem);
            }

            // PROCESOS
            //var procesos =
            //    from p in System.Diagnostics.Process.GetProcesses()
            //    where p.ProcessName != "Idle" &&
            //        DateTime.Now - p.StartTime > new TimeSpan(1, 0, 0)
            //    orderby p.ProcessName
            //    select p;
            //ObjectDumper.Write(procesos);

            // REFLEXION
            printProperties(typeof(TimeSpan));

            // enumeración de ficheros
            string[] ficheros = Directory.GetFiles(
                " C:\\Users\\Octavio\\Pictures", "*.jpg",
                SearchOption.AllDirectories);
            IEnumerable<FileInfo> datosFicheros =
                (from f in ficheros select new FileInfo(f)).Take(10);
            ObjectDumper.Write(datosFicheros);

            // enumeración de ficheros 2
            using (FileSystemEnumerator fse = new FileSystemEnumerator(
                " C:\\Users\\Octavio\\Pictures", "*.jpg", true))
            {
                IEnumerable<FileInfo> datosFicheros2 =
                    (from f in fse.Matches() select f).Take(10);
                ObjectDumper.Write(datosFicheros2);
            }

            // LoggingEnumerable.Log = Console.Out;

            // ENCUENTRO EXTERNO POR LA IZQUIERDA
            var outerJoin1 =
                from pa in Paises
                join pe in Generación on pa.Codigo equals pe.CodigoPaisNac 
                into tmp
                from res in tmp.DefaultIfEmpty() // System.Linq.Enumerable.DefaultIfEmpty(tmp)
                select new {
                    Pais = pa.Nombre,
                    Nombre = res == null ? null : res.Nombre 
                };
            ObjectDumper.Write(outerJoin1);

            // ENCUENTRO EXTERNO "POR LA DERECHA"
            var outerJoin2 =
                from pe in Generación 
                join pa in Paises on pe.CodigoPaisNac equals pa.Codigo
                into tmp
                from res in tmp.DefaultIfEmpty() // System.Linq.Enumerable.DefaultIfEmpty(tmp)
                select new
                       {
                           Pais = res == null ? null : res.Nombre,
                           Nombre = pe.Nombre
                       };
            ObjectDumper.Write(outerJoin2);

            Console.WriteLine("-----------------");
            // ENCUENTRO EXTERNO SIMETRICO
            var outerJoin3 = 
                outerJoin1.Union(outerJoin2);
            ObjectDumper.Write(outerJoin3);

            Console.ReadLine();
        }

        public static void printProperties(Type tipo)
        {
            BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.Static;
            var resumen =
                from prop in tipo.GetProperties(flags)
                orderby prop.PropertyType.FullName, prop.Name
                group new {
                    Tipo = prop.PropertyType.FullName,
                    Nombre = prop.Name,
                    Categoría = (prop.GetGetMethod().IsStatic ?
                          "Estática" : "De instancia")
                      } by prop.PropertyType.FullName;
            ObjectDumper.Write(resumen, 2);
        }

    }
}
