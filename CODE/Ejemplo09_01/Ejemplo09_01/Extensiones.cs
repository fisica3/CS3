using System;
using System.Collections.Generic;
using System.Linq;

using PlainConcepts.Clases;

namespace Ejemplo09_01
{
    public static class Extensiones
    {
        public static IEnumerable<Persona> Where1(
            this IEnumerable<Persona> origen,
            Func<Persona, bool> filtro)
        {
            IEnumerator<Persona> enm = origen.GetEnumerator();
            while (enm.MoveNext())
            {
                if (filtro(enm.Current) &&
                    enm.Current.Sexo == SexoPersona.Mujer)
                {
                    yield return enm.Current;
                }
            }
        }

        public static IEnumerable<Persona> Where2(
            this IEnumerable<Persona> origen,
            Func<Persona, bool> filtro)
        {
            foreach (Persona p in origen)
                if (filtro(p) && p.Sexo == SexoPersona.Mujer)
                {
                    yield return p;
                }
        }

        public static IEnumerable<T> Where<T>(
            this IEnumerable<T> origen, Func<T, bool> filtro)
        {
            if (origen == null || filtro == null)
                throw new ArgumentNullException();
            foreach (T t in origen)
                if (filtro(t))
                {
                    yield return t;
                }
        }

        public static IEnumerable<V> Select<T, V>(
            this IEnumerable<T> origen, Func<T, V> selector)
        {
            if (origen == null || selector == null)
                throw new ArgumentNullException();
            foreach (T t in origen)
                yield return selector(t);
        }

    }
}
