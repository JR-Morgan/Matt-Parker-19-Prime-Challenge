using BigMath;
using System;
using System.Collections.Generic;
using System.IO;

namespace MPPrime
{
    static class Program
    {
        const string DEFAULT_ROOT = @"Primes\",
                     DEFAULT_SUFFIX = @".txt",
                     DEFAULT_SOLUTION_FILE = @"Solutions.txt",
                     DEFAULT_STATE_FILE = @"State.txt";

        const int DEFAULT_GROUPS = 10;

        static string root, suffix, solutionFile, stateFile;


        private static SolverState LoadSave(string savePath)
        {
            if (File.Exists(savePath))
            {
                string[] save = File.ReadAllLines(stateFile)[0].Split(';');
                string[] fileProperties = save[0].Split(',');
                string[] primeProperties = save[1].Split(',');

                return new SolverState()
                {
                    primeCounter = long.Parse(primeProperties[0]),
                    primeSum = Int256.Parse(primeProperties[1]),
                    groupIndex = int.Parse(fileProperties[0]),
                    part = int.Parse(fileProperties[1]) + 1
                };
            }
            return SolverState.DEFAULT;
        }

        /// <summary>
        /// Argument list
        /// 0 - Groups
        /// 1 - Solution File
        /// 2 - State File
        /// 3 - Root
        /// 4 - Input File Suffix
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args = null)
        {
            int i = 0;
            int groups = args.Length > i ? int.Parse(args[i++]) : DEFAULT_GROUPS;
            solutionFile = args.Length > i ? args[i++] : DEFAULT_SOLUTION_FILE;
            stateFile = args.Length > i ? args[i++] : DEFAULT_STATE_FILE;
            root = args.Length > i ? args[i++] : DEFAULT_ROOT;
            suffix = args.Length > i ? args[i++] : DEFAULT_SUFFIX;

            Solver solver = new Solver(LoadSave(stateFile), FilePath, SaveHandler);

            solver.Solve(groups);

            Console.WriteLine("Finished All Groups");
        }


        private static void SaveHandler(IEnumerable<Solution> solutionsFound, SolverState state)
        {
            List<string> solutions = new List<string>();
            
            foreach(Solution s in solutionsFound)
            {
                solutions.Add(s.ToString());
                Console.WriteLine(s.ToString());
            }
            Console.WriteLine(state.ToHRString());



            File.WriteAllText(stateFile, state.ToString());

            if (!File.Exists(solutionFile)) File.Create(solutionFile).Close();

            File.AppendAllLines(solutionFile, solutions);
            
        }


        private static string FilePath(int group, int part)
        {
            return root + group switch
            {
                1 => $"Ate_100G_part{part}",
                10 => $"De_900G_a_1T_part{part}",
                _ => $"De_{group - 1}00G_a_{group}00G_part{part}",
            } + suffix;
        }



    }
}