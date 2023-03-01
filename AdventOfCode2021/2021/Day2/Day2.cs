using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2021.Day
{
    public class Day2 : DayBase
    {
        readonly List<(string Name, int Value)> commands;

        public Day2(int part, int day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            commands = File.ReadAllLines(path)
                           .Select(x => x.Split())
                           .Select(x => (x[0], int.Parse(x[1])))
                           .ToList();

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
            var horizontalPosition = 0;
            var depth = 0;

            foreach (var (name, value) in commands)
            {
                switch (name)
                {
                    case "forward":
                        horizontalPosition += value;
                        break;
                    case "down":
                        depth += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                }
            }

            AOCConsole.WriteLine($"The answer is: {horizontalPosition * depth}");
        }

        public void Part2()
        {
            var horizontalPosition = 0;
            var depth = 0;
            var aim = 0;

            foreach (var (name, value) in commands)
            {
                switch (name)
                {
                    case "forward":
                        horizontalPosition += value;
                        depth += aim * value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }
            }

            AOCConsole.WriteLine($"The answer is: {horizontalPosition * depth}");
        }
    }
}