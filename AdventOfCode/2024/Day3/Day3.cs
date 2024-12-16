using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    public class Day3 : IAdventChallenge
    {
        public void Run()
        {
            throw new Exception("requires params");
        }

        public void Run(string[] parameters)
        {
            var input = File.ReadAllText(parameters[0]);
            Part1(input);
            Part2(input);
        }

        public void Part1(string input)
        {
            /*
             * mul = string literal "mul"
             * \( = literal paren match
             * ((\d+)) = capture group for one or more digits (left)
             * , = literal comma
             * ((\d+)) = capture group for one or more digits (right)
             * \) = literal end paren
             */
            var pattern = @"mul\((\d+),(\d+)\)";

            // RegexOptions.Multiline enables the regex to work on multiple lines
            // in large strings
            var regex = new Regex(pattern, RegexOptions.Multiline);

            var matches = regex.Matches(input);

            var total = 0;
            foreach (Match match in matches)
            {
                var left = int.Parse(match.Groups[1].Value);
                var right = int.Parse((match.Groups[2].Value));
                total += left * right;
            }

            Console.WriteLine($"total: {total}");
        }

        public void Part2(string input)
        {
            var doPattern = @"do\(\)";
            var dontPattern = @"don't\(\)";
            var mulPattern = @"mul\((\d+),(\d+)\)";

            var doRegex = new Regex(doPattern, RegexOptions.Multiline);
            var dontRegex = new Regex(dontPattern, RegexOptions.Multiline);
            var mulRegex = new Regex(mulPattern, RegexOptions.Multiline);

            var doMatches = doRegex.Matches(input);
            var dontMatches = dontRegex.Matches(input);
            var mulMatches = mulRegex.Matches(input);

            var doMatchIndexes = doMatches.Select(d => d.Index);
            var dontMatchIndexes = dontMatches.Select(d => d.Index);
            var controlFlowConcat = dontMatches.Concat(doMatches);
            var controlFlowOrdered = controlFlowConcat.OrderBy(x => x.Index).ToList();

            var controlFlow = new List<int>() { 0 };
            var controlFlowEnabled = true;
            foreach (var index in controlFlowOrdered) 
            {
                if(controlFlowEnabled && dontMatchIndexes.Contains(index.Index))
                {
                    controlFlow.Add(index.Index);
                    controlFlowEnabled = !controlFlowEnabled;
                }
                else if(!controlFlowEnabled && doMatchIndexes.Contains(index.Index))
                {
                    controlFlow.Add(index.Index);
                    controlFlowEnabled = !controlFlowEnabled;
                }
            }

            var total = 0;
            var enabled = true;
            
            for(int i = 0; i < controlFlow.Count; i++)
            {
                if (enabled)
                {
                    var matchesInRange = new List<Match>();
                    if (i < controlFlow.Count - 1)
                    {
                        matchesInRange =
                            mulMatches.Where(
                                x =>
                                    x.Index >= controlFlow[i] &&
                                    x.Index < controlFlow[i + 1])
                            .ToList();
                    }
                    else
                    {
                        // edge case: snag any ops which exist beyond the last control flow switch if enabled
                        matchesInRange = mulMatches.Where(x => x.Index >= controlFlow[i]).ToList();
                    }

                    foreach(Match match in matchesInRange)
                    {
                        var left = int.Parse(match.Groups[1].Value);
                        var right = int.Parse((match.Groups[2].Value));
                        total += left * right;
                    }
                }

                enabled = !enabled;
            }

            Console.WriteLine($"total with control flow {total}");
        }
    }
}
