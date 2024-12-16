using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    public class Day2 : IAdventChallenge
    {
        public void Run()
        {

        }

        public void Run(string[] parameters)
        {
            // parameter should be a file with lines of unknown length
            // of ints separated by whitespace, with each line representing
            // a "report" in the context of this challenge

            if (parameters == null || parameters.Length <= 0)
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

            var lineList = lines.ToList();

            Part1(lineList);
            Part2(lineList);
        }

        public void Part1(List<string> input)
        {
            var totalSafe = 0;
            foreach (var line in input)
            {
                var split = line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x))
                    .ToList();

                if(split.Count <= 1)
                {
                    continue;
                }

                // we have to either trend positive or negative
                var expectedDirection = split[0] - split[1] > 0 ? "neg" : "pos";
                var safe = true;
                for(int i = 0; i < split.Count - 1; i++)
                {
                    var diff = split[i] - split[i + 1];
                    var direction = diff > 0 ? "neg" : "pos";
                    if (diff == 0 || 
                        Math.Abs(diff) > 3 ||
                        !expectedDirection.Equals(direction))
                    {
                        safe = false;
                        break;
                    }                    
                }

                if (safe)
                {
                    totalSafe++;
                }
            }

            Console.WriteLine($"total safe: {totalSafe}");
        }

        public void Part2(List<string> input)
        {
            var totalSafe = 0;
            foreach(var line in input)
            {
                var split = line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x))
                    .ToList();

                // attempt to try with original input
                if(Evaluate(split))
                {
                    totalSafe++;
                }

                // but if that fails, try every permutation of input with one removed
                else
                {
                    for(int i = 0; i < split.Count; i++)
                    {
                        var newInput = CopyWithout(split, i);
                        if (Evaluate(newInput))
                        {
                            totalSafe++;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine($"safe with dampener: {totalSafe}");
        }

        private bool Evaluate(List<int> input)
        {
            if (input.Count <= 1)
            {
                return false;
            }

            var safe = true;
            var expectedDirection = input[0] - input[1] > 0 ? "neg" : "pos";
            
            for (int i = 0; i < input.Count - 1; i++)
            {
                var diff = input[i] - input[i + 1];
                var direction = diff > 0 ? "neg" : "pos";
                if (diff == 0 ||
                    Math.Abs(diff) > 3 ||
                    !expectedDirection.Equals(direction))
                {
                    safe = false;
                    break;
                }
            }

            return safe;
        }

        private List<int> CopyWithout(List<int> input, int indexToRemove)
        {
            var newInput = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                if(i != indexToRemove)
                {
                    newInput.Add(input[i]);
                }
            }

            return newInput;
        }
    }
}
