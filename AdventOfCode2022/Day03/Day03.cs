using AdventOfCode;
using System;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day03 
    {
        private readonly string _inventory;


        public Day03(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _inventory = File.ReadAllText(path);

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

        private static int GetValue(char c)
        {
            var i = (int)c;
            return i switch
            {
                > 64 and <= 90 => i - 38,
                >= 97 and <= 122 => i - 96,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static int CalculatePartOne(string input)
        {
            var r = input[(input.Length / 2)..] ?? throw new ArgumentNullException("input[(input.Length / 2)..]");
            return input.Take(input.Length / 2).Where(c => r.Contains(c)).Select(GetValue).First();
        }

        public void Part1()
        {
            var prioritySum = _inventory.Split("\n").Select(CalculatePartOne).Sum();
            AOCConsole.WriteLine($"The answer is: {prioritySum}");
        }

        public void Part2()
        {
            var result = 0;
            var elves = _inventory.Split("\n");
            for (var i = 0; i < elves.Length; i += 3)
            {
                var elf = elves[i];
                result += elf[..].Where(c => elves[i + 1].Contains(c) && elves[i + 2].Contains(c)).Select(GetValue).First();
            }
            AOCConsole.WriteLine($"The answer is: {result}");
        }

    }
}