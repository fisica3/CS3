using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using PlainConcepts.Linq;

namespace Ejemplo03_01
{
    public class PrimeSequence : IEnumerable<int>
    {
        private IEnumerator<int> getEnumerator()
        {
            int i = 1;
            while (true)
            {
                if (i.IsPrime())
                    yield return i;

                if (i == int.MaxValue)
                    yield break;
                else
                    i++;
            }
        }
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
