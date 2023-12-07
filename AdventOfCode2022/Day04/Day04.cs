using AdventOfCode;
using System;
using System.IO;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day04 
    {
        private readonly string _zones;

        public Day04(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _zones = File.ReadAllText(path);

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
            var matches = 0;
            foreach (var line in _zones.Split('\n', StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    var assingments = line.Split(',');
                    var elf1 = assingments[0].Split('-');
                    var elf2 = assingments[1].Split('-');

                    var elf1Min = int.Parse(elf1[0]);
                    var elf1Max = int.Parse(elf1[1]);
                    var elf2Min = int.Parse(elf2[0]);
                    var elf2Max = int.Parse(elf2[1]);

                    if (elf1Min >= elf2Min && elf1Max <= elf2Max ||
                       elf2Min >= elf1Min && elf2Max <= elf1Max)
                    {
                        matches++;
                    }
                }
            }
            AOCConsole.WriteLine($"The answer is: {matches}");
        }

        public void Part2()
        {

            var matches = 0;
            foreach (var line in _zones.Split('\n', StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    var assingments = line.Split(',');
                    var elf1 = assingments[0].Split('-');
                    var elf2 = assingments[1].Split('-');

                    var elf1Min = int.Parse(elf1[0]);
                    var elf1Max = int.Parse(elf1[1]);
                    var elf2Min = int.Parse(elf2[0]);
                    var elf2Max = int.Parse(elf2[1]);

                    if (elf1Min >= elf2Min && elf1Min <= elf2Max ||
                        elf1Max >= elf2Min && elf1Max <= elf2Max ||
                        elf2Min >= elf1Min && elf2Min <= elf1Max ||
                        elf2Max >= elf1Min && elf2Max <= elf1Max)
                    {
                        matches++;
                    }
                }
            }
            AOCConsole.WriteLine($"The answer is: {matches}");

        }

    }
}