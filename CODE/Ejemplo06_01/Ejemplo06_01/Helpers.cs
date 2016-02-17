using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace PlainConcepts.Extensions
{
    public static class Helpers
    {
        // extensiones para int
        public static bool esPrimo(this int num)
        {
            if (num == 1 || num == 2)
                return true;
            if (num % 2 == 0)
                return false;
            for (int i = 3; i < num / 2; i += 2)
                if (num % i == 0)
                    return false;
            return true;
        }

        public static bool esMultiploDe(this int num, int otro)
        {
            return num % otro == 0;
        }

        public static bool esImpar(this int num)
        {
            return num.esMultiploDe(2);
        }

        // extensiones para double
        public static double ElevadoA(this double a, double b)
        {
            // calcula a^b 
            return Math.Pow(a, b);
        }

        // extensiones para string
        public static bool EsDireccionCorreo(this string s)
        {
            Regex regex = new Regex(
                @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public static bool EsNulaOVacia(this string s)
        {
            return (s == null || s.Trim().Length == 0);
        }

        public static string Inversa(this string s)
        {
            char[] rev = s.ToCharArray();
            Array.Reverse(rev);
            return (new string(rev));
        }

        // extensiones para FileInfo
        public static bool EsOculto(FileInfo fi)
        {
            return (fi.Attributes & FileAttributes.Hidden) != 0;
        }

        // extensiones para object
        public static void Imprimir(this object obj)
        {
            Console.WriteLine(obj);
        }
        public static void Imprimir(this object obj, string fmt)
        {
            Console.WriteLine(fmt, obj);
        }

        // extensiones para T[]
        public static T[] Corte<T>(this T[] arr,
            int index, int count)
        {
            if (index < 0 || count < 0 ||
                arr.Length - index < count)
                throw new ArgumentException("Parámetro inválido");
            T[] result = new T[count];
            Array.Copy(arr, index, result, 0, count);
            return result;
        }

        // extensiones para IEnumerable<T>
        public static IEnumerable<T> Duplicar<T>(
            this IEnumerable<T> secuencia)
        {
            if (secuencia == null)
                throw new ArgumentException("Secuencia nula");
            foreach (T elemento in secuencia)
            {
                yield return elemento;
                yield return elemento;
            }
        }
    }
}
