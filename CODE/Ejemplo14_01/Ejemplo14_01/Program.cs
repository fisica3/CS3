using System;
using System.Linq;
using System.Data.Linq;
using System.Xml.Linq;
using System.Linq.Dynamic;
using System.Transactions;

using System.Data;

namespace Ejemplo14_01
{
    class Program
    {
        static void Main(string[] args)
        {
            //Consulta1();

            //Consulta1b();

            //Consulta1c();

            //Consulta2();

            //Consulta3();

            // Consulta4();

            // Consulta5();

            //ConsultaDinamica1();

            //ConsultaDinamica2();

            //Actualizaciones();

            //ActualizacionConcurrente();

            //ProcYFunciones();

            //ExportarXml();

            ExportarDataTable();

            Console.ReadLine();
        }

        internal class MyDataTable : DataTable
        {
            internal MyDataTable()
                : base()
            {
                Columns.Add("Nombre", typeof(string));
                Columns.Add("FechaNacimiento", typeof(DateTime));
            }

            internal DataRow MakeDataRow(string nombre, DateTime dt)
            {
                DataRow dr = this.NewRow();
                dr.BeginEdit();
                dr[0] = nombre;
                dr[1] = dt;
                dr.EndEdit();
                return dr;
            }
        }
        private static void ExportarDataTable()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                ctx.Log = Console.Out;
                MyDataTable dt = new MyDataTable();
                dt.TableName = "Portugueses";
                var x = from f in ctx.Futbolista 
                    where f.CodigoPaisNacimiento == "PT"
                    select dt.MakeDataRow(f.Nombre, f.FechaNacimiento.Value);
                x.CopyToDataTable(dt, LoadOption.OverwriteChanges);

                // comprobación
                foreach (DataRow dr in dt.Rows)
                    Console.WriteLine(dr[0]);
            }
        }

        private static void Consulta5()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                ctx.Log = Console.Out;

                int cant =
                   (from f in ctx.Futbolista
                    where f.CodigoPaisNacimiento == "PT"
                    select f).Count();

                Console.WriteLine("Portugueses: {0}", cant);
            }
        }

        private static void Consulta4()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                ctx.Log = Console.Out;

                var lista1 =
                   (from f in ctx.Futbolista
                    orderby f.FechaNacimiento 
                    select new
                    {
                        Jugador = f.Nombre + " (" + f.Posicion + ")",
                        Equipo = f.Club.Nombre
                    }).Take(5);

                Console.WriteLine("Los 5 más viejos");
                foreach (var p in lista1)
                    Console.WriteLine(p);
            }
        }

        static void Consulta1()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                ctx.Log = Console.Out;

                var lista1 =
                    from p in ctx.Futbolista
                    join q in ctx.Club on p.CodigoClub equals q.Codigo
                    where p.FechaNacimiento >= new DateTime(1987, 1, 1)
                    orderby q.Nombre, p.Nombre
                    select new
                    {
                        Jugador = p.Nombre + " (" + p.Posicion + ")",
                        Equipo = q.Nombre
                    };

                // equivalente con métodos extensores
                var lista2 =
                    ctx.Futbolista.
                        Join(ctx.Club,
                             p => p.CodigoClub,
                             q => q.Codigo,
                             (p, q) => new { p, q }).
                        Where(x => x.p.FechaNacimiento >= new DateTime(1987, 1, 1)).
                        OrderBy(x => x.q.Nombre).
                        ThenBy(x => x.p.Nombre).
                        Select(x => new
                        {
                            Jugador = x.p.Nombre + " (" + x.p.Posicion + ")",
                            Equipo = x.q.Nombre
                        });

                Console.WriteLine("Consulta 1");
                foreach (var p in lista1)
                    Console.WriteLine(p);

                Console.WriteLine("Consulta 2");
                foreach (var p in lista2)
                    Console.WriteLine(p);
            }
        }

        static void Consulta1b()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                ctx.Log = Console.Out;

                var lista1 =
                    from p in ctx.Futbolista
                    where p.FechaNacimiento >= new DateTime(1987, 1, 1)
                    orderby p.Club.Nombre, p.Nombre
                    select new
                    {
                        Jugador = p.Nombre + " (" + p.Posicion + ")",
                        Equipo = p.Club.Nombre
                    };

                Console.WriteLine("Consulta 1b");
                foreach (var p in lista1)
                    Console.WriteLine(p);
            }
        }

        static void Consulta1c()
        {
            FutbolDataContext ctx = new FutbolDataContext();
            ctx.Log = Console.Out;

            var lista0 =
                from p in ctx.Futbolista select p;

            foreach (Futbolista p in lista0)
                Console.WriteLine(p);

            var lista1 =
                from p in ctx.Futbolista
                where p.FechaNacimiento >= new DateTime(1987, 1, 1)
                orderby p.Club.Nombre, p.Nombre
                select new
                {
                    Jugador = p.Nombre + " (" + p.Posicion + ")",
                    Equipo = p.Club.Nombre
                };

            Console.WriteLine("Consulta 1c");
            foreach (var p in lista1)
                Console.WriteLine(p);
        }

        static void Consulta2()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                ctx.Log = Console.Out;

                var grupos =
                    from f in ctx.Futbolista
                    group f by f.CodigoPaisNacimiento into tmp
                    orderby tmp.Count() descending
                    join p in ctx.Pais on tmp.Key equals p.Codigo
                    select new { p.Nombre, Cantidad = tmp.Count() };

                Console.WriteLine("Futbolistas por países");
                foreach (var p in grupos)
                    Console.WriteLine(p);
            }
        }

        static void Consulta3()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                ctx.Log = Console.Out;

                string codPais = "BR"; // valor del "parámetro"

                //var compys =
                //    from f1 in ctx.Futbolista
                //    where f1.Pais.Codigo == codPais
                //    orderby f1.Pais.Nombre
                //    join f2 in ctx.Futbolista
                //        on f1.CodigoPaisNacimiento equals f2.CodigoPaisNacimiento into tmp
                //    from t in tmp
                //    where f1.Id < t.Id && f1.CodigoClub == t.CodigoClub
                //    select f1.Nombre + " - " + t.Nombre + " (" + f1.Pais.Nombre + ")";

                var compys = from f1 in ctx.Futbolista
                             where f1.CodigoPaisNacimiento == codPais
                             orderby f1.Pais.Nombre
                             from f2 in ctx.Futbolista
                             where f1.CodigoPaisNacimiento == f2.CodigoPaisNacimiento &&
                                   f1.Id < f2.Id &&
                                   f1.CodigoClub == f2.CodigoClub
                             select f1.Nombre + " - " + f2.Nombre + " (" + f1.Pais.Nombre + ")";

                Console.WriteLine("Compatriotas en equipos");
                foreach (var p in compys)
                    Console.WriteLine(p);
            }
        }

        class Tmp
        {
            public string Ciudad { get; set; }
            public int CantClubes { get; set; }
        }
 
        static void ConsultaDinamica1()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                ctx.Log = Console.Out;

                string ciudad = "MADRID";

                var cm = ctx.ExecuteQuery<Club>(
                    "SELECT * FROM Club WHERE Ciudad = {0}", ciudad);

                Console.WriteLine("Clubes madrileños");
                foreach (var c in cm)
                    Console.WriteLine(c.Nombre);

                var ciudades = ctx.ExecuteQuery<Tmp>(
                   @"SELECT Ciudad, COUNT(*) AS CantClubes FROM Club 
                     GROUP BY Ciudad 
                     ORDER BY 2 DESC");

                Console.WriteLine("Clubes por ciudad");
                foreach (var c in ciudades)
                    Console.WriteLine(c.Ciudad + " - " + c.CantClubes);
            }
        }

        static void ConsultaDinamica2()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                ctx.Log = Console.Out;

                var cm = ctx.Club.
                    Where("Ciudad == \"MADRID\"").
                    OrderBy("Nombre").
                    Select("New(Codigo, Nombre)"); 
                
                foreach (var c in cm)
                    Console.WriteLine(c);
            }
        }

        private static void Actualizaciones()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                ctx.Log = Console.Out;

                var cons1 =
                    from c in ctx.Club
                    where c.Ciudad == "MADRID"
                    select c;

                foreach (var c in cons1)
                    Console.WriteLine(c.Nombre);

                var cons2 =
                    from c in ctx.Club
                    where (from f in ctx.Futbolista 
                           where f.Club.Codigo == c.Codigo && f.CodigoPaisNacimiento != "ES"
                           select c).Count() > 10
                    select c;

                foreach (var c in cons2)
                    Console.WriteLine(c.Nombre);

                // localizar un futbolista
                Futbolista ronie = ctx.Futbolista.
                    First(x => x.Nombre == "RONALDO");

                if (ronie != null)
                    // actualizarlo (el objeto)
                    ronie.Nombre = "RONALDO LUIZ NAZARIO DA LIMA";

                // insertar un país -- CAMBIO POSTERIOR A BETA 2
                ctx.Pais.InsertOnSubmit(new Pais { Codigo = "CU", Nombre = "CUBA" });

                // borrar un país -- CAMBIO POSTERIOR A BETA 2
                Pais tv = ctx.Pais.Single(x => x.Codigo == "TV");
                ctx.Pais.DeleteOnSubmit(tv);

                try
                {
                    // aplicar cambios a la base de datos
                    using (TransactionScope ts = new TransactionScope())
                    {
                        // ctx.ExecuteCommand("exec sp_Cambios");
                        ctx.SubmitChanges();
                        ts.Complete();
                    }

                    //ctx.Transaction = ctx.Connection.BeginTransaction();
                    //try
                    //{
                    //    ctx.ExecuteCommand("exec sp_Cambios"); // o lo que hiciera falta
                    //    ctx.SubmitChanges();
                    //    ctx.Transaction.Commit();
                    //}
                    //catch
                    //{
                    //    ctx.Transaction.Rollback();
                    //    throw;
                    //}
                    //finally
                    //{
                    //    ctx.Transaction = null;
                    //}
                }
                catch (Exception x)
                {
                    Console.WriteLine("ERROR: " + x.Message);
                }
                Console.ReadLine();
            }
        }

        private static void ActualizacionConcurrente()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                ctx.Log = Console.Out;

                // actualizar un futbolista
                Futbolista ronie = ctx.Futbolista.
                    First(x => x.Nombre == "RONALDO");
                if (ronie != null)
                    ronie.Nombre = "RONALDO (REAL MADRID)";

                using (FutbolDataContext ctx2 = new FutbolDataContext())
                {
                    ctx2.Log = Console.Out;

                    // actualizar un futbolista
                    Futbolista ronie2 = ctx2.Futbolista.
                        First(x => x.Nombre == "RONALDO");
                    if (ronie2 != null)
                        ronie2.Nombre = "RONALDO (INTER MILAN)";

                    try
                    {
                        // aplicar cambios de 1º contexto
                        ctx.SubmitChanges();

                        // aplicar cambios de 2º contexto
                        ctx2.SubmitChanges(ConflictMode.ContinueOnConflict);
                    }
                    catch (ChangeConflictException)
                    {
                        // mantener los valores establecidos localmente
                        ctx2.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                        // reintentar los cambios
                        ctx2.SubmitChanges(ConflictMode.FailOnFirstConflict);
                    }
                    Console.ReadLine();
                }
            }
        }

        private static void ProcYFunciones()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                // llamada a UDF
                Console.WriteLine("En total hay " + ctx.FutbolistasDeUnPais("FR") + " franceses.");

                // llamada a SP
                ctx.InsertarFutbolista(
                    "METZELDER", new DateTime(1979, 4, 11), "DE", "RMA", 'D');

                // actualizaciones a medida
                Club c = ctx.Club.Single(x => x.Codigo == "RMA");
                c.Nombre = "REAL MADRID C.F.";
                ctx.SubmitChanges();
            }

            Console.ReadLine();
        }

        private static void ExportarXml()
        {
            using (FutbolDataContext ctx = new FutbolDataContext())
            {
                XElement liga =
                    new XElement("Liga",
                        from c in ctx.Club orderby c.Codigo
                        select new XElement("Club",
                            new XAttribute("Codigo", c.Codigo),
                            new XElement("Nombre", c.Nombre),
                            new XElement("Ciudad", c.Ciudad),
                            new XElement("Jugadores", 
                                from f in c.Futbolista
                                //from f in ctx.Futbolista 
                                //where f.CodigoClub == c.Codigo
                                select new XElement("Jugador",
                                    new XAttribute("Codigo", f.Id),
                                    new XElement("Nombre", f.Nombre),
                                    new XElement("Posicion", f.Posicion)))));
                liga.Save("Liga2006.XML"); 
            }
        }

    }
}
