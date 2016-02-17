using System;
using System.Collections.Generic;
using System.Linq;

namespace PlainConcepts.Clases
{
    public class Matrimonio: ParOrdenado<Persona>
    {
        public DateTime? FechaBoda { get; set; }
        public string LugarBoda { get; set; }

        // constructor(es)
        public Matrimonio(Persona p1, Persona p2, 
            DateTime? fecha, string lugar)
            : base(p1, p2)
        {
            FechaBoda = fecha;
            LugarBoda = lugar;
        }
        public Matrimonio(Persona p1, Persona p2)
            : this(p1, p2, null, null)
        {
        }
        public Matrimonio(
            string nombre1, SexoPersona sexo1, 
            string nombre2, SexoPersona sexo2, 
            DateTime fecha, string lugar)
            : this(new Persona(nombre1, sexo1), 
                   new Persona(nombre2, sexo2), fecha, lugar)
        {
        }

        // propiedades
        public Persona Marido
        {
            get { return Primero; }
            set { Primero = value; }
        }
        public Persona Mujer
        {
            get { return Segundo; }
            set { Segundo = value; }
        }
    }
}
