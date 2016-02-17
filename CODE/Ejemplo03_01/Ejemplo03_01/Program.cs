using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using FindFiles;

namespace Ejemplo03_01
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (int i in new NaturalNumbersSequence())
                Console.WriteLine(i);

            //foreach (int i in new PrimeSequence2())
            //{
            //    Console.WriteLine(i);
            //    System.Threading.Thread.Sleep(1000);
            //}

            //BigIntegerPrimes p = new BigIntegerPrimes(
            //new BigInteger(1),
            //BigInteger.Parse("1000000000000000000000"));
            //foreach (var x in p)
            //    Console.WriteLine(x);

            // enumeración de ficheros
            string[] files = System.IO.Directory.GetFiles(
                "C:\\Users\\Octavio\\Pictures", "*.jpg",
                SearchOption.AllDirectories);
            foreach (string s in files)
                Console.WriteLine(s);

           FileSystemEnumerator fse = 
                new FileSystemEnumerator("C:\\Users\\Octavio\\Pictures",
                    "*.jpg", true);
            foreach (FileInfo fi in fse.Matches())
                Console.WriteLine(fi.FullName);

            Console.ReadLine();
        }
    }
}
