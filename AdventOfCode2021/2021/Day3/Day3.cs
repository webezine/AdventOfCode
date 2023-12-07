using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2021.Day
{
    public class Day3 
    {
        readonly List<string> binaryNumbers;
        readonly int numberOfBits;

        public Day3(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            binaryNumbers = File.ReadAllLines(path).ToList();
            numberOfBits = binaryNumbers[0].Length;

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
            var gammaRate = new string(Enumerable.Range(0, numberOfBits)
                                                 .Select(i => binaryNumbers.Count(c => c[i] == '1') >
                                                              binaryNumbers.Count(c => c[i] == '0') ? '1' : '0')
                                                 .ToArray());

            var epsilonRate = new string(gammaRate.Select(x => x == '1' ? '0' : '1')
                                                  .ToArray());

            var powerConsumption = Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);

            AOCConsole.WriteLine($"The answer is: {powerConsumption}");
        }

        public void Part2()
        {
            var oxygenGeneratorRating = GetOxygenGeneratorRating();
            var co2ScrubberRating = GetCo2ScrubberRating();

            var lifeSupportRating = oxygenGeneratorRating * co2ScrubberRating;

            AOCConsole.WriteLine($"The answer is: {lifeSupportRating}");
        }

        private int GetOxygenGeneratorRating()
        {
            var numbers = binaryNumbers.ToList();

            for (var i = 0; i < numberOfBits; i++)
            {
                var mostCommonValue = numbers.Count(c => c[i] == '1') >= numbers.Count(c => c[i] == '0') ? '1' : '0';

                numbers.RemoveAll(x => x[i] != mostCommonValue);

                if (numbers.Count == 1) break;
            }

            return Convert.ToInt32(numbers.First(), 2);
        }

        private int GetCo2ScrubberRating()
        {
            var numbers = binaryNumbers.ToList();

            for (var i = 0; i < numberOfBits; i++)
            {
                var leastCommonValue = numbers.Count(c => c[i] == '1') < numbers.Count(c => c[i] == '0') ? '1' : '0';

                numbers.RemoveAll(x => x[i] != leastCommonValue);

                if (numbers.Count == 1) break;
            }

            return Convert.ToInt32(numbers.First(), 2);
        }
    }
}