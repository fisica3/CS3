using System;

namespace PlainConcepts.Clases
{
    public enum SexoPersona { Mujer, Varón }

    public class Persona: IComparable, IComparable<Persona>
    {
        #region Propiedades
        private string nombre = null;
        private SexoPersona? sexo = null;
        private DateTime? fechaNac = null;
        private string codigoPaisNac = null;
        #endregion

        #region Constructores
        public Persona() { }
        public Persona(string nombre, SexoPersona sexo)
        {
            this.Nombre = nombre;
            this.Sexo = sexo;
        }
        public Persona(string nombre, SexoPersona sexo, DateTime fechaNac)
            : this(nombre, sexo)
        {
            this.FechaNac = fechaNac;
        }
        public Persona(string nombre, SexoPersona sexo, DateTime fechaNac,
            string codPaisNac)
            : this(nombre, sexo, fechaNac)
        {
            this.CodigoPaisNac = codPaisNac;
        }
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public SexoPersona? Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }
        public DateTime? FechaNac
        {
            get { return fechaNac; }
            set { fechaNac = value; }
        }

        public static int? _Edad(DateTime? fechaNac)
        {
            if (fechaNac != null)
                return (int)
                  (DateTime.Today.Subtract(fechaNac.Value).TotalDays /
                   365.25);
            else
                return null;
        }
        public int? Edad
        {
            get
            {
                return Persona._Edad(this.FechaNac);
            }
        }

        public string CodigoPaisNac
        {
            get { return codigoPaisNac; }
            set { codigoPaisNac = value; }
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return (nombre != null ? nombre : "ANONIMO") +
              (sexo != null ? (sexo.Value == SexoPersona.Mujer ? " (M)"
                                                : " (V)") : "") +
              (fechaNac != null ? " (" + fechaNac.Value.ToString("dd/MM/yyyy") + ")" : "") +
              (codigoPaisNac != null ? " (" + CodigoPaisNac + ")" : "");
        }

        public bool CumpleAñosEsteMes()
        {
            Console.WriteLine("Uso del método de la clase");
            return FechaNac.HasValue &&
                FechaNac.Value.Month == DateTime.Today.Month &&
                FechaNac.Value.Day >= DateTime.Today.Day;
        }
        #endregion

        #region Interfaz IComparable
        public int CompareTo(object otro)
        {
            if (otro == null || !(otro is Persona))
                throw new ArgumentException("No es Persona!");

            return this.Nombre.CompareTo((
                otro as Persona).Nombre);
        }
        #endregion

        #region Interfaz IComparable<Persona>
        public int CompareTo(Persona otra)
        {
            if (otra == null)
                throw new ArgumentException("No es Persona!");

            return this.Nombre.CompareTo(otra.Nombre);
        }

        #endregion
    }
}
