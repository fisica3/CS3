using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainConcepts.Linq
{
    public static class Extensions
    {
        public static bool IsPrime(this int number)
        {
            for (int i = 2; i <= number / 2; i++)
                if (number % i == 0)
                    return false;
            return true;
        }
    }
}
