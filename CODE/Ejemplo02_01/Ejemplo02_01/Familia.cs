using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainConcepts.Clases
{
    public class Familia
    {
        private Matrimonio padres;
        private Lista<Persona> hijos = new Lista<Persona>();

        public Familia(Persona padre, Persona madre,
            params Persona[] hijos)
        {
            padres = new Matrimonio(padre, madre);
            foreach (Persona p in hijos)
                this.hijos.Add(p);
        }

        public Persona Padre
        {
            get { return padres.Marido; }
        }
        public Persona Madre
        {
            get { return padres.Mujer; }
        }
        public Lista<Persona> Hijos
        {
            get { return hijos; }
        }

        public override string ToString()
        {
            string s = "Padre: " + Padre.ToString() + "\n" +
                "Madre: " + Madre.ToString() + "\n";
            if (hijos.Count > 0)
            {
                s += "Hijos:\n";
                for (int i = 0; i < hijos.Count; i++)
                    s += "  " + hijos[i].ToString() + "\n";
            }
            return s;
        }
    }
}
