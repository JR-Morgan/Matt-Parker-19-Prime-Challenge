using BigMath;

namespace MPPrime
{
    public struct SolverState
    {
        public static SolverState DEFAULT = new SolverState() { primeCounter = 0, primeSum = 0, groupIndex = 1, part = 1 };

        public long primeCounter;
        public Int256 primeSum;
        public int groupIndex;
        public int part;

        public override string ToString() => $"{groupIndex},{part};{primeCounter},{primeSum}";
        public readonly string ToHRString() => $"Checked upto {{group:{groupIndex}, part:{part}}},\n" +
            $"{{{primeCounter}}} primes have been checked.\n" +
            $"Current sum is {{{primeSum}}}\n";
    }
}
