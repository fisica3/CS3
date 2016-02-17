using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ejemplo03_01
{
    public class NaturalNumbersSequence : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 1; i <= 1000; i++)
                yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 1; i <= 1000; i++)
                yield return i;
        }
    }
}
