using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2021.Day
{
    public class Day7 
    {
        readonly string[] _input;
        private readonly int[] _inputs;

        public Day7(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _input = File.ReadAllText(path).Split(",");
            _inputs = Array.ConvertAll(_input, int.Parse);

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
            var maxVal = _inputs.Max();

            var results = new int[maxVal];

            for (var i = 1; i < maxVal; i++)
            {
                var result = _inputs.Sum(t => Math.Abs(t - i));
                results[i] = result;
            }
            var cheapestRoute = results.Where(x => x != 0).Min();
            AOCConsole.WriteLine($"The answer is: {cheapestRoute}");
        }

        public void Part2()
        {
            var resultPairs = new Dictionary<int, int>();
            var max = _inputs.Max();
            var results = new int[max];

            for (var i = 1; i < max; i++)
            {
                var result = 0;
                foreach (var t in _inputs)
                {
                    var numberOfSteps = Math.Abs(t - i);

                    if (resultPairs.ContainsKey(numberOfSteps))
                    {
                        result += resultPairs[numberOfSteps];
                    }
                    else
                    {
                        var sum = Enumerable.Range(1, numberOfSteps).ToArray().Sum();
                        resultPairs.Add(numberOfSteps, sum);
                        result += sum;
                    }
                }

                results[i] = result;
            }

            var cheapestRoute = results.Where(x => x != 0).Min();
            AOCConsole.WriteLine($"The answer is: {cheapestRoute}");
        }
    }
}