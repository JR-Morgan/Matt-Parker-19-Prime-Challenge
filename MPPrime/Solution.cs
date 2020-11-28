using BigMath;

namespace MPPrime
{
    public struct Solution
    {
        public long numberOfPrimes;
        public Int256 sum;
        public long lastPrime;


        public override string ToString() => $"N:{numberOfPrimes}\t- Last Prime: {lastPrime}\t- Sum:{{{sum}}}";
    }
}
