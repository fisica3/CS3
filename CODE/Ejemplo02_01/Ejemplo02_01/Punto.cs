using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainConcepts.Clases
{
    public class Punto : ParOrdenado<int>
    {
        public Punto(int x, int y)
            : base(x, y)
        {
        }

        // desplazar punto en el plano
        public void Mover(int dx, int dy)
        {
            Primero += dx;
            Segundo += dy;
        }
    }
}
