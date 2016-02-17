using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ejemplo07_01
{
    delegate int Mapeado_I_I(int x);
    delegate T Mapeado<T>(T x);
    delegate void Accion();

    delegate int AutoFuncion(AutoFuncion self, int x);

    class Program
    {
        static void Main(string[] args)
        {
            Mapeado_I_I cuadrado_I = delegate(int x) { return x * x; };
            Console.WriteLine(cuadrado_I(8));

            Mapeado<int> cuadrado = delegate(int x) { return x * x; };
            Mapeado<int> cuadrado1 = x => x * x;
            Mapeado<int> cuadrado2 = (int x) => x * x;
            Mapeado<int> cuadrado3 = (int x) => { return x * x; };
            Mapeado<int> cuadrado4 = x => { return x * x; };

            Accion saludo = () => Console.WriteLine("Hola");
            Func<int, int> cuadrado6 = x => x * x;

            Func<int, int, bool> esDivisiblePor =
                (int a, int b) => a % b == 0;

            Func<int, bool> esImpar = x => esDivisiblePor(x, 2);

            Func<int, bool> esPrimo =
                x =>
                {
                    for (int i = 2; i <= x / 2; i++)
                        if (esDivisiblePor(x, i))
                            return false;
                    return true;
                };

            Func<double, double, double> hipotenusa =
                (x, y) => Math.Sqrt(x * x + y * y);

            Func<double, double, double> elevadoA =
                (x, y) => Math.Pow(x, y);

            Func<string, bool> esNulaOVacia =
                s => s == null || s.Trim().Length == 0;

            Func<string, string> inversa = s =>
                {
                    char[] rev = s.ToCharArray();
                    Array.Reverse(rev);
                    return (new string(rev));
                };

            if (esPrimo(97))
                Console.WriteLine("97 es primo");
            Console.WriteLine(inversa("ABRACADABRA"));

            Func<int, Func<double, double>> selector =
                x =>
                {
                    switch (x)
                    {
                        case 1:
                            return Math.Sqrt;
                        case 2:
                            return Math.Sin;
                        default:
                            return delegate(double z) { return 2 * z; };
                    }
                };

            int sel = 1;
            double entrada = 4.5;
            double salida = selector(sel)(entrada);
            Console.WriteLine(salida);

            M_TT_T<int> maxAndSwap =
                (ref int a, ref int b) =>
                {
                    if (a.CompareTo(b) < 0)
                    {
                        int temp = a;
                        a = b;
                        b = temp;
                    }
                    return a;
                };

            int m = 36, n = 45;
            int max = maxAndSwap(ref m, ref n);
            Console.WriteLine("m = {0}, n = {1}", m, n);
 
            AutoFuncion F = (f, x) => x == 0 ? 1 : x * f(f, x - 1);
            Func<int, int> factorial = x => F(F, x);

            Console.WriteLine(factorial(5));  // imprime 120

            // Mapeado_I_I ff = (k) => k <= 1 ? 1 : k * ff(k - 1);  // INCORRECTO! 


            // expresión lambda con clausura
            int N = 5;

            Func<int, int> incrementarN = x => x + N;

            Console.WriteLine(incrementarN(4));  // imprime 9
            N = 27;
            Console.WriteLine(incrementarN(4));  // imprime 31



            Console.ReadLine();

        }

        int factorial(int n)
        {
            return n <= 1 ? 1 : n * factorial(n - 1);
        }

    }

    // en System.Core.dll
    // espacio de nombres System.Linq
    public delegate T Func<T>();
    public delegate T Func<A0, T>(A0 a0);
    public delegate T Func<A0, A1, T>(A0 a0, A1 a1);
    public delegate T Func<A0, A1, A2, T>(A0 a0, A1 a1, A2 a2);
    public delegate T Func<A0, A1, A2, A3, T>(A0 a0, A1 a1,
        A2 a2, A3 a3);

    delegate T M_TT_T<T>(ref T a, ref T b) 
        where T: IComparable<T>; 
}
