using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using PlainConcepts.LinqProviders;

namespace Ejemplo11_04
{

    class Program
    {
        static void Main(string[] args)
        {
            // colección de prueba
            var src = new ClaseLinqQueryable<Persona>();
            src.Add(new Persona("Judith", 37));
            src.Add(new Persona("Gustavo", 45)); 
            src.Add(new Persona("Irina", 41));
            src.Add(new Persona("Diana", 12));

            var res1 = from p in src where p.Edad > 25 orderby p.Nombre select p.Nombre;
            // var res2 = src.Where(p => p.Edad > 25).Select(p => p.Nombre);

            foreach (var x in res1)
                Console.WriteLine(x);

            Console.WriteLine("------------------------");

            ParameterExpression par = Expression.Parameter(typeof(Persona), "p");

            /* p => p.Edad > 25 */
            Expression<Func<Persona, bool>> predicado = 
                Expression.Lambda<Func<Persona,bool>>(
                    /* > */
                    Expression.GreaterThan(
                        /* p.Edad (int) */
                        Expression.Property(par, typeof(Persona).GetProperty("Edad")),
                        /* 25 */
                        Expression.Constant(25)
                    ),
                    /* par */
                    new ParameterExpression[] { par }
                );
             /* p => p.Nombre */
             Expression<Func<Persona, string>> selección =
                 Expression.Lambda<Func<Persona, string>>(
                     /* p.Nombre */
                     Expression.Property(par, typeof(Persona).GetProperty("Nombre")),
                     /* p */
                     new ParameterExpression[] { par }
                 );

            // var res = Queryable.Where(src, predicado).Select(selección);

            var tmp = Queryable.Where(src, predicado);
            var res = Queryable.Select(tmp, selección);

            foreach (var x in res)
                Console.WriteLine(x);

            Console.WriteLine("------------------------");

            var xx = (from x in src 
                      where x.Nombre.Contains('D') 
                      select x).Count();
            Console.WriteLine(xx);
                      
            Console.ReadLine();
        }
    }
}
