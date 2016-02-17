using System;

using PlainConcepts.Clases;

namespace PlainConcepts.Clases.Futbol
{
    public class Club
    {
        #region Campos
        private string codigo, nombre, ciudad;
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
        public string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value.ToUpper(); }
        }
        #endregion

        #region Constructor
        public Club(string codigo, string nombre, string ciudad)
        {
            Codigo = codigo;
            Nombre = nombre;
            Ciudad = ciudad;
        }
        #endregion
    }

    public enum Posicion
    {
        Portero,
        Defensa,
        Medio,
        Delantero
    }

    public class Futbolista: Persona
    {
        #region Campos añadidos
        private string codigoClub;
        private Posicion posicion;
        #endregion

        #region Propiedades añadidas
        public string CodigoClub
        {
            get { return codigoClub; }
            set { codigoClub = value.ToUpper(); }
        }
        public Posicion Posicion
        {
            get { return posicion; }
            set { posicion = value; }
        }
        #endregion

        #region Constructor
        public Futbolista(string nombre, DateTime fechaNac, string codigoPaisNac, 
                          string codigoClub, Posicion posicion): 
            base(nombre, SexoPersona.Varón, fechaNac, codigoPaisNac)
        {
            this.CodigoClub = codigoClub;
            this.Posicion = posicion;
        }
        #endregion
    }
}
