using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PlainConcepts.Clases;

namespace Ejemplo05_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 2;        // equivale a int n = 2;
            var s = "Hola";   // equivale a string s = "Hola";

            var m = 3 * n;    // m es de tipo int
            var dosPi = 2 * Math.PI;
            // dosPi es de tipo double

            var p1 = new Persona("Ana", SexoPersona.Mujer);
            // p1 es de tipo Persona
            var p2 = p1;
            // p2 también
            var listaEnteros = new List<int>();
            // listaEnteros es de tipo List<int>
            var elMesQViene = DateTime.Now.AddMonths(1);
            // elMesQViene es de tipo DateTime

            //var p;           // la inicialización es obligatoria
            //var q = null;    // imposible inferir el tipo de q
            //var z1 = 3,
            //    z2 = 5;      // no se admiten declaraciones múltiples
            //var r = { 1, 2, 3 };
            //    // un inicializador de array no es permitido
            //    // (se verá más adelante)

            foreach (var k in listaEnteros)   // k es de tipo int
                Console.WriteLine(k);

            //using (var con = new SqlConnection(cadenaConexion))
            //    // con es de tipo SqlConnection
            //{
            //    con.Open();
            //    // interactuar con la base de datos
            //}

            var p = new Persona
                    {
                        Nombre = "Amanda",
                        Sexo = SexoPersona.Mujer,
                        FechaNac = new DateTime(1998, 10, 23)
                    };

            var listaEnteros2 = new List<int> { 1, 2, 3 };

            var hijos = new List<Persona> {
                new Persona { Nombre = "Diana", Sexo = SexoPersona.Mujer,
                              FechaNac = new DateTime(1996, 2, 4) },
                new Persona { Nombre = "Dennis", Sexo = SexoPersona.Varón,
                              FechaNac = new DateTime(1983, 12, 27) },
                p
            };
            var plurales = new Dictionary<string, string> {
                { "nombre", "nombres" },
                { "arroz", "arroces" },
            };

            var revista1 = new
                   {
                       Nombre = "DotNetManía",
                       EjemplaresPorAño = 11
                   };
            // revista1.EjemplaresPorAño = 4; // error de compilación

            var revista2 = new
               {
                   Nombre = "MSDN Magazine",
                   EjemplaresPorAño = 12
               };

            var a = new[] { 2, 3, 5, 7 };     // int []
            // new[] es obligatorio ¡!
            var b = new[] { "x", "y1", "z" }; // string []

            var contactos = new[] {
                new {
                      Nombre = "Antonio López",
                      Telefonos = new[] { "914792573", "607409115" }
                    },
                new {
                      Nombre = "Pedro Posada",
                      Telefonos = new[] { "981234118" }
                    }
            };

        }
    }

    public class Libro
    {
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private int añoPublicación;

        public int AñoPublicación
        {
            get { return añoPublicación; }
            set { añoPublicación = value; }
        }

        // ...
    }

    public class Revista
    {
        // interno
        private string nombre;
        private int ejemplares;

        // propiedades (solo lectura)
        public string Nombre { get { return nombre; } }
        public int EjemplaresPorAño { get { return ejemplares; } }
        // constructor
        public Revista(String nombre, int ejemplares)
        {
            this.nombre = nombre;
            this.ejemplares = ejemplares;
        }
    }

}
