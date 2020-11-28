using BigMath;
using System;
using System.Collections.Generic;
using System.IO;

namespace MPPrime
{
    public delegate string FilePath(int group, int part);
    public delegate void SaveStrategy(IEnumerable<Solution> solutionsFound, SolverState state);

    public class Solver
    {
        private Int256 primeSum;
        private long primeCounter;
        private int groupIndex;
        private int part;

        private FilePath FilePath;
        private SaveStrategy Save;


        public Solver(SolverState state, FilePath FilePath, SaveStrategy Save)
        {
            this.FilePath = FilePath;
            this.Save = Save;
            primeSum = state.primeSum;
            primeCounter = state.primeCounter;
            groupIndex = state.groupIndex;
            part = state.part;
        }

        public SolverState State => new SolverState()
        {
            primeSum = primeSum,
            primeCounter = primeCounter,
            groupIndex = groupIndex,
            part = part,
        };


        public void Solve(int numberOfGroups)
        {
            #if DEBUG
            checked {
            #endif
            for (; groupIndex <= numberOfGroups; groupIndex++, part = SolverState.DEFAULT.part) //For each group of files
            {
                
                for (string fileName = FilePath(groupIndex, part); File.Exists(fileName); fileName = FilePath(groupIndex, ++part)) //For each file
                {
                    Save(SearchFile(fileName), State);
                }

                //Sanity check for a missing file
                if (File.Exists(FilePath(groupIndex, part + 1))) throw new Exception("Missing " + FilePath(groupIndex, part));

            }
            #if DEBUG
            }
            #endif
        }

        private List<Solution> SearchFile(string fileName)
        {
            #if DEBUG
            checked {
            #endif

            List<Solution> solutionsFound = new List<Solution>();
            foreach (string line in File.ReadAllLines(fileName)) //For each line
            {
                string[] number = line.Split("\t");
                foreach (string numberString in number) //For each prime
                {
                    long prime = long.Parse(numberString);
                    primeCounter += 1;
                    primeSum += (Int256)((Int128)prime * prime);

                    if (primeSum % primeCounter == 0)
                    {
                        solutionsFound.Add(new Solution()
                        {
                            numberOfPrimes = primeCounter,
                            sum = primeSum,
                            lastPrime = prime
                        });
                    }
                }
            }
            return solutionsFound;
            #if DEBUG
            }
        #endif
        }



    }
}
