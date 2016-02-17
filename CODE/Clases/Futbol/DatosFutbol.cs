using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace PlainConcepts.Clases.Futbol
{
    public static class DatosFutbol
    {
        public static List<Club> Clubes = new List<Club>();
        public static List<Pais> Paises = new List<Pais>();
        public static List<Futbolista> Futbolistas = new List<Futbolista>();

        static DatosFutbol()
        {
            using (SqlConnection con = new SqlConnection(
                       "Data Source=.\\SQLExpress;Initial Catalog=FUTBOL2006;" +
                       "Integrated Security=True"))
            {
                con.Open();
                // Paises
                using (SqlCommand cmd = new SqlCommand("SELECT Codigo, Nombre FROM Pais", con))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Paises.Add(new Pais(rdr[0].ToString(), rdr[1].ToString()));
                        }
                    }
                }
                // Clubes
                using (SqlCommand cmd = new SqlCommand("SELECT Codigo, Nombre, Ciudad FROM Club", con))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Clubes.Add(new Club(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString()));
                        }
                    }
                }
                // Futbolistas
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Nombre, FechaNacimiento, CodigoPaisNacimiento, CodigoClub, Posicion FROM Futbolista", con))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Futbolistas.Add(new Futbolista(
                                                rdr[0].ToString(), 
                                                DateTime.Parse(rdr[1].ToString()),
                                                rdr[2].ToString(), rdr[3].ToString(),
                                                ObtenerPosicion(rdr[4].ToString()[0])));
                        }
                    }
                }
            }
        }

        private static Posicion ObtenerPosicion(char ch)
        {
            switch(ch)
            {
                case 'P':
                    return Posicion.Portero;
                case 'D':
                    return Posicion.Defensa;
                case 'M':
                    return Posicion.Medio;
                case 'L':
                    return Posicion.Delantero;
                default:
                    throw new ArgumentException("Posición ilegal");
            }
        }
    }
}
