using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Drawing;
using System.Reflection;

namespace PlainConcepts.Expressions
{
    public static class ExpressionExtensions
    {
        // método auxiliar
        private static Expression Derivada(this Expression e, string param)
        {
            if (e == null)
                throw new ArgumentException("Expresión nula");
            switch (e.NodeType)
            {
                // regla de constante
                case ExpressionType.Constant:
                    return Expression.Constant(0.0);
                // parámetro
                case ExpressionType.Parameter:
                    if (((ParameterExpression) e).Name == param)
                        return Expression.Constant(1.0);
                    else
                        return Expression.Constant(0.0);
                // cambio de signo
                case ExpressionType.Negate: 
                    Expression op = ((UnaryExpression) e).Operand;
                    return Expression.Negate(op.Derivada(param));
                // regla de la suma
                case ExpressionType.Add:
                {
                    Expression dleft = ((BinaryExpression) e).Left.Derivada(param);
                    Expression dright = ((BinaryExpression) e).Right.Derivada(param);
                    return Expression.Add(dleft, dright);
                }
                // regla de la resta
                case ExpressionType.Subtract:
                {
                    Expression dleft = ((BinaryExpression) e).Left.Derivada(param);
                    Expression dright = ((BinaryExpression) e).Right.Derivada(param);
                    return Expression.Subtract(dleft, dright);
                }
                //regla de la multiplicación
                case ExpressionType.Multiply:
                {
                    Expression left = ((BinaryExpression) e).Left;
                    Expression right = ((BinaryExpression) e).Right;
                    Expression dleft = left.Derivada(param);
                    Expression dright = right.Derivada(param);
                    return Expression.Add(
                        Expression.Multiply(left, dright),
                        Expression.Multiply(dleft, right));
                }
                //regla del cociente
                case ExpressionType.Divide:
                {
                    Expression left = ((BinaryExpression) e).Left;
                    Expression right = ((BinaryExpression) e).Right;
                    Expression dleft = left.Derivada(param);
                    Expression dright = right.Derivada(param);
                    return Expression.Divide(
                        Expression.Subtract(
                            Expression.Multiply(left, dright),
                            Expression.Multiply(dleft, dright)),
                        Expression.Multiply(right, right));
                }
                case ExpressionType.Power:
                {
                    Expression left = ((BinaryExpression) e).Left;
                    Expression right = ((BinaryExpression) e).Right;
                    Expression dleft = left.Derivada(param);
                    return Expression.Multiply(
                        dleft,
                        Expression.Power(
                            left,
                            Expression.Subtract(
                                            right,
                                            Expression.Constant(1.0)
                            )));
                }
                // llamada a la función
                case ExpressionType.Call:
                    Expression e1 = null;
                    MethodCallExpression me = 
                        (MethodCallExpression) e;
                    MethodInfo mi = me.Method;
                    // COMPROBACIÓN
                    // el método debe ser estático y su clase Math
                    if (!mi.IsStatic ||
                         mi.DeclaringType.FullName != "System.Math")
                        throw new ArgumentException("NO IMPLEMENTADO");

                    ReadOnlyCollection<Expression> parms = me.Arguments;
                    switch (mi.Name)
                    {
                        case "Pow":
                            // regla de la potencia
                            e1 = Expression.Multiply(
                                parms[1],
                                Expression.Call(
                                    mi, 
                                    new Expression[] {
                                        parms[0],
                                        Expression.Subtract(parms[1], Expression.Constant(1.0))
                                    }));
                            break;
                        case "Sin":
                            // la derivada del seno es el coseno
                            e1 = Expression.Call(
                                    typeof(Math).GetMethod("Cos", new Type[] { typeof(double) }),
                                    new Expression[] {
                                        parms[0]
                                    });
                            break;
                        default: 
                            throw new ArgumentException("PENDIENTE");
                    }
                    // regla de la cadena
                    return Expression.Multiply(e1,
                        parms[0].Derivada(param));
                // otros
                default:
                    throw new ArgumentException("No soportada: " + e.NodeType.ToString());
            }
        }

        public static Expression<T> Derivada<T>(this Expression<T> e)
        {
            if (e == null)
                throw new ArgumentException("Expresión nula");

            // comprobar que hay una sola variable
            if (e.Parameters.Count != 1)
                throw new ArgumentException("Un parámetro!");
            return Expression.Lambda<T>(
                e.Body.Derivada(e.Parameters[0].Name),
                e.Parameters);
        }

        public static Expression <T>
            Derivada <T>(this Expression<T> e, string param)
        {
            if ( e == null)
                throw new ArgumentException("Expresión nula");
            // comprobar que ‘param’ existe
            bool ok = false;
            foreach (ParameterExpression p in e.Parameters)
                if (p.Name == param)
                {
                    ok = true; break;
                }
            if (!ok)
                throw new ArgumentException("Parámetro invalido");
            return Expression.Lambda<T>(
                e.Body.Derivada(e.Parameters[0].Name),
                e.Parameters);
        }

        // *** PENDIENTE ( se deja como ejercicio al lector)

        public static Expression<T> Simplificar <T>(this Expression<T> e)
        {
            if ( e == null)
                throw new ArgumentException("Expresión nula");
            return e;
        }

        public static void Dibujar<T>(this Expression<T> e,
            Graphics dc, Rectangle rect)
        {
            if (e == null)
                throw new ArgumentException("Expresión nula");
        // dibujar el rectángulo ‘rect’ de ‘dc’
        }
    }
}
