using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    internal class Day1 : IAdventChallenge
    {
        public void Run()
        {
            throw new Exception($"challenge {typeof(Day1)} requires params");
        }

        public void Run(string[] parameters)
        {
            // parameter should be a file with two columns of input
            // separated by an unknown amount of whitespace

            if(parameters == null || parameters.Length <= 0)
            {
                throw new ArgumentException("invalid paramters");
            }

            if (!File.Exists(parameters[0]))
            {
                throw new ArgumentException($"could not read input file {parameters[0]}");
            }

            var lines = File.ReadAllLines(parameters[0]);

            if (lines == null || lines.Length <= 0)
            {
                throw new InvalidOperationException("could not parse input file");
            }

            var leftLines = new List<int>();
            var rightLines = new List<int>();
            
            foreach (var line in lines)
            {
                // not checking edge cases because we already know the input is well formatted
                var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                leftLines.Add(int.Parse(split[0]));
                rightLines.Add(int.Parse(split[1]));
            }

            var leftSorted = leftLines.OrderBy(x => x).ToList();
            var rightSorted = rightLines.OrderBy(x => x).ToList();
            
            Part1(leftSorted, rightSorted);
            Part2(leftSorted, rightSorted);
        }

        public void Part1(List<int> leftSorted, List<int> rightSorted)
        {
            var distance = 0;
            for (int i = 0; i < leftSorted.Count; i++)
            {
                distance += Math.Abs(leftSorted[i] - rightSorted[i]);
            }

            Console.WriteLine($"distance total: {distance}");
        }

        public void Part2(List<int> leftSorted, List<int> rightSorted)
        {
            var similarityScore = 0;

            foreach (var left in leftSorted)
            {
                var foundCount = 0;
                foreach(var right in rightSorted)
                {
                    if(left == right)
                    {
                        foundCount++;
                    }
                }

                similarityScore += foundCount * left;
            }

            Console.WriteLine(similarityScore);
        }
    }
}
