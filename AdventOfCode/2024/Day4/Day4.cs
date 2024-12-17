using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    public class Day4 : IAdventChallenge
    {
        public void Run()
        {
            throw new Exception("requires params");
        }

        public void Run(string[] parameters)
        {
            var input = File.ReadAllLines(parameters[0]);
            var grid = new char[input[0].Length, input.Length];

            for (int i = 0; i < input.Length; i++) 
            {
                for (int j = 0; j < input[i].Length; j++) 
                {
                    var line = input[i];
                    grid[i, j] = line[j];
                }
            }

            Part1(grid);
            Part2(grid);
        }

        private void Part1(char[,] grid)
        {
            var target = "XMAS";
            var numFound = 0;

            for(int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j = 0; j < grid.GetLength(1); j++)
                {
                    numFound += EvalLeftToRight(grid, j, i, target) ? 1 : 0;
                    numFound += EvalRightToLeft(grid, j, i, target) ? 1 : 0;
                    numFound += EvalTopToBottom(grid, j, i, target) ? 1 : 0;
                    numFound += EvalBottomToTop(grid, j, i, target) ? 1 : 0;
                    numFound += EvalLeftToRightTopToBottom(grid, j, i, target) ? 1 : 0;
                    numFound += EvalLeftToRightBottomToTop(grid, j, i, target) ? 1 : 0;
                    numFound += EvalRightToLeftTopToBottom(grid, j, i, target) ? 1 : 0;
                    numFound += EvalRightToLeftBottomToTop(grid, j, i, target) ? 1 : 0;
                }
            }

            Console.WriteLine($"numFound: {numFound}");
        }

        private void Part2(char[,] grid)
        {
            var target = "MAS";
            var targetReversed = "SAM";
            var numFound = 0;
            int currentFound;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    currentFound = 0;

                    // Check all four diagonal directions for "MAS" and "SAM"
                    currentFound += EvalTopLeftBottomRight(grid, j, i, target) ? 1 : 0;
                    currentFound += EvalBottomLeftTopRight(grid, j, i, target) ? 1 : 0;
                    currentFound += EvalBottomRightTopLeft(grid, j, i, target) ? 1 : 0;
                    currentFound += EvalTopRightBottomLeft(grid, j, i, target) ? 1 : 0;

                    // If we found at least 2 valid "MAS" patterns, count this center as an X-MAS
                    if (currentFound >= 2)
                    {
                        numFound++;
                    }
                }
            }

            Console.WriteLine($"numFound: {numFound}");
        }

        private bool EvalLeftToRight(char[,] grid, int x, int y, string target)
        {
            /*
             * X X M X A S
             * M A S A S A
             */
            
            if (x + target.Length > grid.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < target.Length; i++)
            {
                if (grid[y, x + i] != target[i]) 
                { 
                    return false; 
                }
            }

            return true;
        }

        private bool EvalRightToLeft(char[,] grid, int x, int y, string target)
        {
            if (x - target.Length + 1 < 0) 
            { 
                return false; 
            }

            for (int i = 0; i < target.Length; i++)
            {
                if (grid[y, x - i] != target[i])
                {
                    return false;
                }
            }

            return true;
        }

        private bool EvalTopToBottom(char[,] grid, int x, int y, string target)
        {
            if (y + target.Length > grid.GetLength(0)) 
            { 
                return false; 
            }

            for (int i = 0; i < target.Length; i++)
            {
                if (grid[y + i, x] != target[i])
                {
                    return false;
                }
            }

            return true;
        }

        private bool EvalBottomToTop(char[,] grid, int x, int y, string target)
        {
            if (y - target.Length + 1 < 0) 
            { 
                return false; 
            }

            for (int i = 0; i < target.Length; i++)
            {
                if (grid[y - i, x] != target[i]) 
                { 
                    return false; 
                }
            }

            return true;
        }

        private bool EvalLeftToRightTopToBottom(char[,] grid, int x, int y, string target)
        {
            if (x + target.Length > grid.GetLength(1) || 
                y + target.Length > grid.GetLength(0))
            {
                return false;
            }

            for (int i = 0; i < target.Length; i++)
            {
                if (grid[y + i, x + i] != target[i]) 
                { 
                    return false; 
                }
            }

            return true;
        }

        private bool EvalLeftToRightBottomToTop(char[,] grid, int x, int y, string target)
        {
            if (x + target.Length > grid.GetLength(1) || y - target.Length + 1 < 0)
            {
                return false;
            }

            for (int i = 0; i < target.Length; i++)
            {
                if (grid[y - i, x + i] != target[i])
                {
                    return false;
                }
            }

            return true;
        }

        private bool EvalRightToLeftTopToBottom(char[,] grid, int x, int y, string target)
        {
            if (x - target.Length + 1 < 0 || y + target.Length > grid.GetLength(0)) 
            { 
                return false; 
            }

            for (int i = 0; i < target.Length; i++)
            {
                if (grid[y + i, x - i] != target[i]) 
                {    
                    return false; 
                }
            }

            return true;
        }

        private bool EvalRightToLeftBottomToTop(char[,] grid, int x, int y, string target)
        {
            if (x - target.Length + 1 < 0 || y - target.Length + 1 < 0) 
            { 
                return false; 
            }

            for (int i = 0; i < target.Length; i++)
            {
                if (grid[y - i, x - i] != target[i]) 
                { 
                    return false; 
                }
            }

            return true;
        }

        private bool EvalTopLeftBottomRight(char[,] grid, int x, int y, string target)
        {
            if (x - 1 < 0 || 
                y - 1 < 0 || 
                x + 1 >= grid.GetLength(1) || 
                y + 1 >= grid.GetLength(0))
            {
                return false;
            }
            
            return grid[y - 1, x - 1] == target[0] &&
                   grid[y, x] == target[1] &&
                   grid[y + 1, x + 1] == target[2];
        }

        private bool EvalBottomLeftTopRight(char[,] grid, int x, int y, string target)
        {
            if (x - 1 < 0 || 
                y + 1 >= grid.GetLength(0) || 
                x + 1 >= grid.GetLength(1) || 
                y - 1 < 0)
            {
                return false;
            }

            return grid[y + 1, x - 1] == target[0] &&
                   grid[y, x] == target[1] &&
                   grid[y - 1, x + 1] == target[2];
        }

        private bool EvalBottomRightTopLeft(char[,] grid, int x, int y, string target)
        {
            if (x + 1 >= grid.GetLength(1) || 
                y + 1 >= grid.GetLength(0) || 
                x - 1 < 0 || 
                y - 1 < 0) 
            { 
                return false; 
            }

            return grid[y + 1, x + 1] == target[0] &&
                   grid[y, x] == target[1] &&
                   grid[y - 1, x - 1] == target[2];
        }

        private bool EvalTopRightBottomLeft(char[,] grid, int x, int y, string target)
        {
            if (x + 1 >= grid.GetLength(1) || 
                y - 1 < 0 || 
                x - 1 < 0 || 
                y + 1 >= grid.GetLength(0))
            {
                return false;
            }

            return grid[y - 1, x + 1] == target[0] &&
                   grid[y, x] == target[1] &&
                   grid[y + 1, x - 1] == target[2];
        }
    }
}
