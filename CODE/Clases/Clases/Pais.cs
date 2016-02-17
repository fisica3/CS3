using System;

namespace PlainConcepts.Clases
{
    public class Pais
    {
        #region Campos
        private string codigo, nombre;
        #endregion

        #region Propiedades
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value.ToUpper(); }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        #endregion

        #region Constructor
        public Pais(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
        }
        #endregion
    }
}
