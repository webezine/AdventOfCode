using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2021.Day
{
    public class Day6 : DayBase
    {
        readonly string[] _input;
        private int[] _inputs;

        public Day6(int part, int day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _input = File.ReadAllText(path).Split(",");
            _inputs = Array.ConvertAll(_input, int.Parse);

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
            for (var iteration = 0; iteration < 80; iteration++)
            {
                var newDay = new List<int>();
                for (var i = 0; i < _input.Length; i++)
                {
                    if (_inputs[i] == 0)
                    {
                        _inputs[i] = 7;
                        newDay.Add(8);
                    }

                    _inputs[i]--;
                }

                var t = new List<int>();
                t.AddRange(_inputs);
                t.AddRange(newDay);

                _inputs = t.ToArray();
            }

            AOCConsole.WriteLine($"The answer is: {_inputs.Length}");
        }

        public void Part2()
        {

            var fishies = new long[9];
            foreach (var i in _inputs)
            {
                fishies[i]++;
            }

            for (var day = 0; day < 256; day++)
            {
                var newFishies = fishies[0];
                for (var i = 1; i < fishies.Length; i++)
                {
                    fishies[i - 1] = fishies[i];
                }

                fishies[8] = newFishies;
                fishies[6] += newFishies;
            }

            var result = fishies.Sum();
            AOCConsole.WriteLine($"The answer is: {result}");
        }
    }
}