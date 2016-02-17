using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using PlainConcepts.Linq;

namespace Ejemplo03_01
{
    public class PrimeSequence2 : IEnumerable<int>
    {
        public int Limit { get; set; }

        public PrimeSequence2() : this(0) { }
        public PrimeSequence2(int limit)
        {
            Limit = limit;
        }

        //private IEnumerator<int> getEnumerator()
        //{
        //    int i = 1;
        //    while (true)
        //    {
        //        if (i.IsPrime())
        //            yield return i;

        //        if (Limit > 0 && i == Limit)
        //            yield break;
        //        i++;
        //    }
        //}

        private IEnumerator<int> getEnumerator()
        {
            yield return 1;
            yield return 2;
            int i = 3;
            while (true)
            {
                bool esPrimo = true;
                if (i % 2 == 0)
                    esPrimo = false;
                else
                {
                    for (int n = 3; n < i / 2; n += 2)
                        if (i % n == 0)
                        {
                            esPrimo = false;
                            break;
                        }
                }
                if (esPrimo)
                    yield return i;

                if (Limit > 0 && i == Limit)
                    yield break;
                i++;
            }
        }

        // ...
        public IEnumerator<int> GetEnumerator()
        {
            return getEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return getEnumerator();
        }
    }
}
