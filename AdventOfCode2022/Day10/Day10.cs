using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day10 : DayBase
    {
        private readonly List<string> _operations;
        private readonly List<int> _cyclesToCatch = new List<int> { 19, 59, 99, 139, 179, 219 };
        private List<int> _signalStrengths = new();

        public Day10(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _operations = File.ReadLines(path).ToList();

            WriteDayPart(part, day);

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
            var sample = new[] { 20, 60, 100, 140, 180, 220 };
            var signalStrengthTotal =  Signal()
                .Where(signal => sample.Contains(signal.cycle))
                .Select(signal => signal.x * signal.cycle)
                .Sum();
            
            AOCConsole.WriteLine($"The answer is: {signalStrengthTotal}");
        }


        public void Part2()
        {
            var result = Signal()
              .Select(signal =>
              {
                  var spriteMiddle = signal.x;
                  var screenColumn = (signal.cycle - 1) % 40;
                  return Math.Abs(spriteMiddle - screenColumn) < 2 ? '#' : ' ';
              })
            .Chunk(40)
            .Select(line => new string(line))
            .Aggregate("", (screen, line) => screen + line + "\n")
            .Ocr();

            AOCConsole.WriteLine($"The answer is: {result}");
        }

        IEnumerable<(int cycle, int x)> Signal()
        {
            var (cycle, x) = (1, 1);
            foreach (var line in _operations)
            {
                var parts = line.Split(" ");
                switch (parts[0])
                {
                    case "noop":
                        yield return (cycle++, x);
                        break;
                    case "addx":
                        yield return (cycle++, x);
                        yield return (cycle++, x);
                        x += int.Parse(parts[1]);
                        break;
                    default:
                        throw new ArgumentException(parts[0]);
                }
            }

        }
    }
}