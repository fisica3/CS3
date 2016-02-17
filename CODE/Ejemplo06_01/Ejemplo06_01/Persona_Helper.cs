using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PlainConcepts.Clases;

namespace PlainConcepts.Extensions
{
    public static partial class Persona_Helper
    {
        public static bool CumpleAñosEsteMes(this Persona p)
        {
            Console.WriteLine("Uso del método extensor");
            return p.FechaNac.HasValue &&
                p.FechaNac.Value.Month == DateTime.Today.Month &&
                p.FechaNac.Value.Day >= DateTime.Today.Day;
        }
    }
}
