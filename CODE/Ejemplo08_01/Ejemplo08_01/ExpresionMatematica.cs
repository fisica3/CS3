using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PlainConcepts.Expressions
{
    public class ExprMatematica
    {
        private Expression<Func<double, double>> expresión;

        public ExprMatematica(string cadena)
        {
            // recibe una cadena de caracteres y 
            // la transforma en un árbol de expresión,
            // que guarda en 'expresión’
            expresión = new Parser(cadena).ResultTree;
        }

        public double Evaluar2(double x)
        {
            // evalúa 'expresión' para el valor 'x'
            Func<double, double> f = expresión.Compile();
            return f(x);
        }

        public double Evaluar(double x)
        {
            Expression e = expresión.Body;
            return Evaluar(e, x);
        }

        private static double Evaluar(Expression e, double valor)
        {
            switch (e.NodeType)
            {
                // operadores binarios
                case ExpressionType.Add:
                    BinaryExpression be1 = (BinaryExpression)e;
                    return Evaluar(be1.Left, valor) + Evaluar(be1.Right, valor);
                case ExpressionType.Subtract:
                    BinaryExpression be2 = (BinaryExpression)e;
                    return Evaluar(be2.Left, valor) - Evaluar(be2.Right, valor);
                case ExpressionType.Multiply:
                    BinaryExpression be3 = (BinaryExpression)e;
                    return Evaluar(be3.Left, valor) * Evaluar(be3.Right, valor);
                case ExpressionType.Divide:
                    BinaryExpression be4 = (BinaryExpression)e;
                    return Evaluar(be4.Left, valor) / Evaluar(be4.Right, valor);
                // operador unario
                case ExpressionType.Negate:
                    UnaryExpression ue = (UnaryExpression)e;
                    return -Evaluar(ue.Operand, valor);
                // constante
                case ExpressionType.Constant:
                    ConstantExpression ce = (ConstantExpression)e;
                    return (double)ce.Value;
                // variable
                case ExpressionType.Parameter:
                    return valor;
                // otra cosa
                default:
                    throw new ArgumentException("Nodo no soportado");
            }
        }

        public void Invertir()
        {
            expresión =
                Expression.Lambda<Func<double, double>>(
                    Expression.Divide(
                        Expression.Constant(1.0), expresión),
                    expresión.Parameters);
        }

        // *** Tipos comunes y scanner

        enum TokenType
        {
            Var, Number, Plus, Minus, UnaryMinus,
            Mult, Div, ParOpen, ParClose, End
        };
        struct Token
        {
            public readonly TokenType Type;
            public readonly double Value; // only used for constants

            public Token(TokenType tt)
            {
                Type = tt; Value = 0;
            }
            public Token(TokenType tt, double val)
            {
                Type = tt; Value = val;
            }
        }

        private static int[,] canFollow = 
        {
            // Var Number Plus Minus UMinus Mul Div Par( Par) END
            { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0 }, // Var		 	
            { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0 }, // Number
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0 }, // Plus
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 }, // Minus
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 }, // UnaryMinus
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0 }, // Mul
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0 }, // Div
            { 0, 0, 1, 1, 0, 1, 1, 1, 0, 0 }, // ParOpen
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0 }, // ParClose
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0 }  // END
        };

        class Scanner : IEnumerable<Token>
        {
            private int pos = 0;
            private string cadena;

            public Scanner(string cadena)
            {
                // PRECOND: cadena!= null
                this.cadena = cadena.Trim().ToUpper();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<Token> GetEnumerator()
            {
                TokenType previous = TokenType.ParOpen,
                    current = TokenType.End;
                int balance = 0;
                double value;

                while (pos < cadena.Length)
                {
                    value = 0.0;
                loop:
                    switch (cadena[pos])
                    {
                        case ' ':
                        case '\t':
                        case '\n':
                            pos++;
                            goto loop;
                        case '(':
                            current = TokenType.ParOpen;
                            balance++;
                            break;
                        case ')':
                            current = TokenType.ParClose;
                            balance--;
                            if (balance < 0)
                                throw new ArgumentException("SYNTAX: Nesting error");
                            break;
                        case '+':
                            current = TokenType.Plus; break;
                        case '-':
                            if (previous != TokenType.Var &&
                                previous != TokenType.Number)
                                current = TokenType.UnaryMinus;
                            else
                                current = TokenType.Minus;
                            break;
                        case '*':
                            current = TokenType.Mult; break;
                        case '/':
                            current = TokenType.Div; break;
                        case 'X':
                            current = TokenType.Var; break;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            // a number
                            bool hasDot = false;
                            StringBuilder sb = new StringBuilder();
                            while (pos < cadena.Length &&
                                ".0123456789".Contains(cadena[pos]))
                            {
                                if (cadena[pos] == '.')
                                    if (hasDot)
                                        throw new ArgumentException("SCAN: Invalid constant");
                                    else
                                        hasDot = true;
                                sb.Append(cadena[pos]);
                                pos++;
                            }
                            current = TokenType.Number;
                            value = double.Parse(sb.ToString());
                            pos--;
                            break;
                        default:
                            throw new ArgumentException("SCAN: Illegal character");
                    }
                    if (canFollow[(int)current, (int)previous] == 1)
                    {
                        previous = current;
                        yield return new Token(current, value);
                        pos++;
                    }
                    else
                        throw new ArgumentException("SCAN: syntax error");
                }
                if (canFollow[(int)TokenType.End, (int)previous] == 1)
                    yield return new Token(TokenType.End);
                else
                    throw new ArgumentException("SCAN: Syntax error");
            }
        }

        // *** Parser
        class Parser
        {
            private Scanner scanner = null;
            private Expression<Func<double, double>> expr = null;

            public Expression<Func<double, double>> ResultTree
            {
                get { return expr; }
            }

            public Parser(string cadena)
            {
                scanner = new Scanner(cadena);
                // generate tree
                tokens = scanner.GetEnumerator();
                GetToken();
                Expression exp = null;
                Gen1(out exp);
                expr = Expression.Lambda<Func<double, double>>(
                    exp, 
                    new[] {Expression.Parameter(typeof(double), "x")}
                );
            }
            private IEnumerator<Token> tokens;
            private Token curr;
            private void GetToken()
            {
                tokens.MoveNext();
                curr = tokens.Current;
            }

            private void Gen1(out Expression exp)
            {
                Token t;
                Expression part;

                Gen2(out exp);
                while ((t = curr).Type == TokenType.Plus ||
                    t.Type == TokenType.Minus)
                {
                    GetToken();
                    Gen2(out part);
                    switch (t.Type)
                    {
                        case TokenType.Plus:
                            exp = Expression.Add(exp, part);
                            break;
                        case TokenType.Minus:
                            exp = Expression.Subtract(exp, part);
                            break;
                    }
                }
            }
            
            private void Gen2(out Expression exp)
            {
                Token t;
                Expression part;

                Gen3(out exp);
                while ((t = curr).Type == TokenType.Mult ||
                    t.Type == TokenType.Div)
                {
                    GetToken();
                    Gen3(out part);
                    switch (t.Type)
                    {
                        case TokenType.Mult:
                            exp = Expression.Multiply(exp, part);
                            break;
                        case TokenType.Div:
                            exp = Expression.Divide(exp, part);
                            break;
                    }
                }
            }

            private void Gen3(out Expression exp)
            {
                bool isMinus = false;
            
                if (curr.Type == TokenType.UnaryMinus)
                {
                    isMinus = true;
                    GetToken();
                }
                Gen4(out exp);
                if (isMinus)
                    exp = Expression.Negate(exp);
            }

            private void Gen4(out Expression exp)
            {
                if (curr.Type == TokenType.ParOpen)
                {
                    GetToken();
                    Gen1(out exp);
                    if (curr.Type != TokenType.ParClose)
                        throw new ArgumentException(
                            "PARSE: Nesting error");
                    GetToken();
                }
                else
                    Atom(out exp);
            }

            private void Atom (out Expression exp)
            {
                switch (curr.Type)
                {
                    case TokenType.Number:
                        exp = Expression.Constant(curr.Value);
                        GetToken();
                        break;
                    case TokenType.Var:
                        exp = Expression.Parameter(typeof(double), "x");
                        GetToken();
                        break;
                    default:
                        exp = null;
                        break;
                }
            }
        }	

    }
}

