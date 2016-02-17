using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PlainConcepts.Clases;
using PlainConcepts.Extensions;
using Pokrsoft.Extensions;

namespace Ejemplo06_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Persona
                    {
                        Nombre = "Amanda",
                        Sexo = SexoPersona.Mujer,
                        FechaNac = new DateTime(1998, 10, 23)
                    };
            if (Persona_Helper.CumpleAñosEsteMes(p))
                Console.WriteLine(p.Nombre + " cumple años el día " +
                    p.FechaNac.Value.Day);
            if (p.CumpleAñosEsteMes())
                Console.WriteLine(p.Nombre + " cumple años el día " +
                    p.FechaNac.Value.Day);

            int n = 48;
            if (n.esMultiploDe(4))
                n.Imprimir("{0} se divide entre 4");

            string dir = "ghost@plainconcepts.com";
            if (dir.EsDireccionCorreo())
                p.Imprimir(dir.Inversa());

            var a1 = new[] { -1, 0, 1, 2, 3 };

            var a2 = a1.Corte(1, 3); // a2 = { 0, 1, 2 }
            foreach (var x in a2)
                x.Imprimir();

            // imprime { 0, 0, 1, 1, 2, 2 }
            foreach (var x in a2.Duplicar())
                x.Imprimir();

            Console.ReadLine();
        }
    }
}
