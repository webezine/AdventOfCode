using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day08 
    {
        private readonly List<string> _treeHeights;

        public Day08(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _treeHeights = File.ReadLines(path).ToList();

            DayOutput.WriteDayPart(part, day);

            if (part == 1)
            {
                Part1();
            }
            else
            {
                Part2();
            }
        }


        public void Part1()
        {
            var gridSize = _treeHeights.Count;
            var grid = new int[_treeHeights.Count][];
            
            //populate grid
            for (var i = 0; i < _treeHeights.Count; i++)
            {
                grid[i] = _treeHeights[i].Select(c => int.Parse(c.ToString())).ToArray();
            }

            var treesVisible = 0;

            for (var i = 0; i < grid.Length; i++)
            {
                var row = grid[i];
                for (var j = 0; j < row.Length; j++)
                {
                    if (IsTreeVisible(i, j, grid[i][j], gridSize, grid))
                    {
                        treesVisible++;
                    }
                }
            }
            AOCConsole.WriteLine($"The answer is: {treesVisible}");
        }



        bool IsTreeVisible(int row, int col, int value, int gridSize, int[][] grid)
        {
            // Check edge
            if (row == 0 || col == 0 || row == gridSize - 1 || col == gridSize - 1)
            {
                return true;
            }

            var valuesToTheLeft = grid[row].Take(col);
            if (valuesToTheLeft.All(val => val < value))
            {
                return true;
            }

            var valuesToTheRight = grid[row].Skip(col + 1);
            if (valuesToTheRight.All(val => val < value))
            {
                return true;
            }

            var valuesToTheTop = grid.Select(r => r[col]).Take(row);
            if (valuesToTheTop.All(val => val < value))
            {
                return true;
            }

            var valuesToTheBottom = grid.Select(r => r[col]).Skip(row + 1);
            return valuesToTheBottom.All(val => val < value);
        }

        
        public void Part2()
        {

            var gridSize = _treeHeights.Count;
            var grid = new int[_treeHeights.Count][];

            //populate grid
            for (var i = 0; i < _treeHeights.Count; i++)
            {
                grid[i] = _treeHeights[i].Select(c => int.Parse(c.ToString())).ToArray();
            }

            var topScore = 0;

            for (var i = 0; i < grid.Length; i++)
            {
                var row = grid[i];
                for (var j = 0; j < row.Length; j++)
                {
                    var score = GetScore(i, j, grid[i][j], gridSize, grid);
                    if (score > topScore)
                    {
                        topScore = score;
                    }
                }
            }
            AOCConsole.WriteLine($"The answer is: {topScore}");
        }

        int GetScore(int row, int col, int value, int gridSize, int[][] grid)
        {
            if (row == 0 || col == 0 || row == gridSize - 1 || col == gridSize - 1)
            {
                return 0;
            }
            // check edge
            var valuesToTheLeft = new Stack<int>(grid[row].Take(col));
            var leftSideScore = 0;
            while (valuesToTheLeft.Any())
            {
                var nextValue = valuesToTheLeft.Pop();
                leftSideScore++;

                if (nextValue >= value)
                {
                    break;
                }
            }


            // check right
            var valuesToTheRight = new Stack<int>(grid[row].Skip(col + 1).Reverse());
            var rightSideScore = 0;
            while (valuesToTheRight.Any())
            {
                var nextValue = valuesToTheRight.Pop();
                rightSideScore++;

                if (nextValue >= value)
                {
                    break;
                }
            }

            // check top 
            var valuesToTheTop = new Stack<int>(grid.Select(r => r[col]).Take(row));
            var topSideScore = 0;
            while (valuesToTheTop.Any())
            {
                var nextValue = valuesToTheTop.Pop();
                topSideScore++;

                if (nextValue >= value)
                {
                    break;
                }
            }

            // check bottom 
            var valuesToTheBottom = new Stack<int>(grid.Select(r => r[col]).Skip(row + 1).Reverse());
            var bottomSideScore = 0;
            while (valuesToTheBottom.Any())
            {
                var nextValue = valuesToTheBottom.Pop();
                bottomSideScore++;

                if (nextValue >= value)
                {
                    break;
                }
            }

            return leftSideScore * rightSideScore * topSideScore * bottomSideScore;
        }


    }
}