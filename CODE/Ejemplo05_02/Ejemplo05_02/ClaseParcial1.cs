using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ejemplo05_02
{
    public partial class ClaseParcial
    {
        // declaraciones
        static partial void MetodoParcial1(int n);
        static partial void MetodoParcial2();

        private string nombre;
        partial void OnCambioNombre(string viejo, string nuevo);
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                OnCambioNombre(nombre, value);
                nombre = value;
            }
        }
    }
}
