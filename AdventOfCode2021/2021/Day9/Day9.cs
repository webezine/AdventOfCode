using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2021.Day
{
    public class Day9 
    {
        readonly string[] _input;
        private static int BasinSize = 0;
        private static HashSet<string> visited = new();
        private static int _rowLength = 0;
        private static int _columnLength = 0;


        public Day9(int part, int day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _input = File.ReadAllLines(path);

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
            var result = 0;
            var map = GetMapArray(_input);
            for (var row = 0; row < map.Length; row++)
            {
                for (var column = 0; column < map[0].Length; column++)
                {
                    var curretnt = map[row][column];
                    if (IsLowPoint(map, row, column, curretnt))
                    {
                        result += (curretnt + 1);
                    }
                }
            }

            AOCConsole.WriteLine($"The answer is: {result}");
        }

        public void Part2()
        {
            var result = 0;
            var map = GetMapArray(_input);
            for (var row = 0; row < map.Length; row++)
            {
                for (var column = 0; column < map[0].Length; column++)
                {
                    var current = map[row][column];
                    if (IsLowPoint(map, row, column, current))
                    {
                        result += (current + 1);
                    }
                }
            }
            AOCConsole.WriteLine($"The answer is: {result}");
        }


        private static int[][] GetMapArray(IEnumerable<string> input)
        {
            return input.Select(line => (Array.ConvertAll(line.ToCharArray(), s => int.Parse(s.ToString())))).ToArray();
        }

        private static bool IsLowPoint(IReadOnlyList<int[]> gg, int row, int column, int current)
        {
            if (row != 0 && current >= gg[row - 1][column])
            {
                return false;
            }
            if (row + 1 != gg.Count && current >= gg[row + 1][column])
            {
                return false;
            }
            if (column != 0 && current >= gg[row][column - 1])
            {
                return false;
            }
            return column + 1 == gg[0].Length || current < gg[row][column + 1];
        }
    }
}