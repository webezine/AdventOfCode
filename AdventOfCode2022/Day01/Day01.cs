using AdventOfCode;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day01
    {
        private readonly string[] _calories;

        public Day01(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _calories = File.ReadAllLines(path);

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
            var elfCal = 0;
            var summarisedElfCal = new List<int>();
            foreach (var entry in _calories)
            {
                if (string.IsNullOrEmpty(entry))
                {
                    summarisedElfCal.Add(elfCal);
                    elfCal = 0;
                    continue; ;
                }
                elfCal += int.Parse(entry);
            }
            AOCConsole.WriteLine($"The answer is: {summarisedElfCal.Max()}");
        }

        public void Part2()
        {
            var elfCal = 0;
            var summarisedElfCal = new List<int>();
            foreach (var entry in _calories)
            {
                if (string.IsNullOrEmpty(entry))
                {
                    summarisedElfCal.Add(elfCal);
                    elfCal = 0;
                    continue; ;
                }
                elfCal += int.Parse(entry);
            }
            summarisedElfCal.Sort();
            summarisedElfCal.Reverse();

            var top3 = summarisedElfCal[0] + summarisedElfCal[1] + summarisedElfCal[2];
            AOCConsole.WriteLine($"The answer is: {top3}");

        }
    }
}