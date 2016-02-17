using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ejemplo05_02
{
    public partial class Persona
    {
        // propiedad Nombre
        private string nombre;
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                IntentoCambioNombre(this); // llamada a método parcial
                nombre = value;
            }
        }

        // método parcial
        partial void IntentoCambioNombre(Persona p);
    }
}
