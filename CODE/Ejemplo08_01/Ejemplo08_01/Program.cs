using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using PlainConcepts.Expressions;

namespace Ejemplo08_01
{
    class Program
    {
        static Expression<Func<double, double>>
            cuboExpr = x => x * x * x;

        static void Main(string[] args)
        {
            Console.WriteLine(cuboExpr);
            // imprime 'x => ((x * x) * x)

            // aquí se genera IL !
            var cubo = cuboExpr.Compile();

            Console.WriteLine("Cubo(5) = " + cubo(5));
            // imprime 'Cubo(5) = 25'

            Func<double, double> area =
                (radio) => Math.PI * radio * radio;

            Func<double, double> area2 =
                delegate(double radio)
                {
                    return Math.PI * radio * radio;
                };

            Expression<Func<double, double>> areaExpr =
                (radio) => Math.PI * radio * radio;

            ParameterExpression parm =
                Expression.Parameter(typeof(double), "radio");
            Expression<Func<double, double>> areaExpr2 =
                Expression.Lambda<Func<double, double>>(
                    Expression.Multiply(
                        Expression.Constant(Math.PI),
                        Expression.Multiply(
                            parm,
                            parm)),
                    parm);

            Func<double, double> area3 = areaExpr.Compile();
            Console.WriteLine("Area(5) = " + area3(5));

            ParameterExpression px = 
                Expression.Parameter(typeof(double), "x");
            ParameterExpression py = 
                Expression.Parameter(typeof(double), "y");
            ParameterExpression [] parms = { px, py };

            Expression<Func<double, double, double >> hipotenusa = 
                Expression.Lambda<Func<double, double,double>>
                    (Expression.Call(
	                    typeof(Math).GetMethod("Sqrt"),
	                    new Expression [] {
		                    Expression.Add(
			                    Expression.Multiply(px, px),
			                    Expression.Multiply(py, py))
		                    }),
                    parms);

            Func<double, double, double> hipot2 =
                hipotenusa.Compile();
            Console.WriteLine(hipot2(3, 4));

            Expression<Func<string, string, string>> combinación = 
                (s1, s2) => (s1.Trim() + " " + s2.Trim()).ToUpper();

            ParameterExpression ps1 = 
                Expression.Parameter(typeof(string), "nombre");
            ParameterExpression ps2 = 
                Expression.Parameter(typeof(string), "apellido");
            ParameterExpression [] parms2 = { ps1, ps2 };

            Expression<Func<string, string, string>> combinación1 =
                Expression.Lambda<Func<string, string, string>>(
                    Expression.Call(
                        Expression.Call(
                            typeof(string).GetMethod("Concat",
                                new[] {typeof(string), typeof(string), typeof(string)}),
                            new Expression[] {
                                Expression.Call(
                                    ps1,
                                    typeof(string).GetMethod("Trim", new Type[] {}), 
                                    null),
                                Expression.Constant(" "),
                                Expression.Call(
                                    ps2,
                                    typeof(string).GetMethod("Trim", new Type[] {}), 
                                    null)
                            }
                        ),
                        typeof(string).GetMethod("ToUpper", new Type[] {}), 
                        null),
                    parms2);

            Func<string, string, string> nombreCompleto =
                combinación1.Compile();
            Console.WriteLine(nombreCompleto(" Pepe ", " Reyes "));

            ExprMatematica em = new ExprMatematica("5*(x + 3)/(x - 1)");
            Console.WriteLine(em.Evaluar(3)); // imprime 10

            Expression<Func<double, double>> seno = x => Math.Sin(x);
            Console.WriteLine(ExpressionExtensions.Derivada(seno));
            Expression<Func<double, double>> dSeno = seno.Derivada();
            Console.WriteLine(dSeno);

            ParameterExpression p5 = 
                Expression.Parameter(typeof(double), "x");
            ParameterExpression [] parms5 = { p5 };

            Expression<Func<double, double>> pot = 
                Expression.Lambda<Func<double, double>>(
                    Expression.Power(
                        Expression.Add(
                            p5,
                            Expression.Constant(1.0)),
                        Expression.Constant(5.0)),
                     parms5);
            Console.WriteLine(ExpressionExtensions.Derivada(pot));

            Console.ReadLine();
        }
    }
}

