using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2021.Day
{
    public class Day1: DayBase
    {
        private readonly List<int> _depths;

        public Day1(int part, int day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _depths = File.ReadAllLines(path).Select(x => int.Parse(x)).ToList();
            
            WriteDayOnePart(part, day);

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

            for (var i = 1; i < _depths.Count; i++)
            {
                if (_depths[i] > _depths[i - 1]) result++;
            }

            AOCConsole.WriteLine($"The answer is: {result}");
        }

        public void Part2()
        {
            var result = 0;

            for (var i = 3; i < _depths.Count; i++)
            {
                if (_depths[i] + _depths[i - 1] + _depths[i - 2] > _depths[i - 1] + _depths[i - 2] + _depths[i - 3])
                {
                    result++;
                }
            }

            AOCConsole.WriteLine($"The answer is: {result}");
        }
    }
}