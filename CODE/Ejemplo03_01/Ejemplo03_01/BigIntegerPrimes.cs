using System;
using System.Collections.Generic;
using System.Linq;
using System.Numeric;

namespace Ejemplo03_01
{
    public class BigIntegerPrimes : IEnumerable<BigInteger>
    {
        // props
        public BigInteger Desde { get; set; }
        public BigInteger Hasta { get; set; }

        // cons
        public BigIntegerPrimes(BigInteger desde,
                                BigInteger hasta)
        {
            Desde = desde;
            Hasta = hasta;
        }
        public BigIntegerPrimes(long desde, long hasta)
        {
            Desde = new BigInteger(desde);
            Hasta = new BigInteger(hasta);
        }

        // iterator
        private IEnumerator<BigInteger> getEnumerator()
        {
            BigInteger cero = BigInteger.Zero;
            BigInteger uno = BigInteger.One;
            BigInteger dos = new BigInteger(2);
            BigInteger tres = new BigInteger(3);

            BigInteger candidato = Desde;
            while (true)
            {
                if (candidato == uno || candidato == dos)
                    yield return candidato;
                else
                {
                    bool esPrimo = true;
                    if (candidato % 2 == cero)
                        esPrimo = false;
                    else
                    {
                        BigInteger fin = candidato / dos;
                        for (BigInteger n = tres; n < fin; n += 2)
                            if (candidato % n == cero)
                            {
                                esPrimo = false;
                                break;
                            }
                    }
                    if (esPrimo)
                        yield return candidato;
                }
                if (candidato == Hasta)
                    yield break;
                else
                    candidato++;
            }
        }
        public IEnumerator<BigInteger> GetEnumerator()
        {
            return getEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return getEnumerator();
        }
    }
}
