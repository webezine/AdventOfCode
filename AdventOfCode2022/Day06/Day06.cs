using System.IO;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day06 : DayBase
    {
        private readonly string _packet;

        public Day06(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _packet = File.ReadAllText(path);

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
            var i = 0;
            var j = 4;
            var slice = _packet[i..j];
            while (!AreUnique(slice))
            {
                i++;
                j++;
                slice = _packet[i..j];
            }

            AOCConsole.WriteLine($"The answer is: {j}");
        }

        private static bool AreUnique(string str)
        {
            var checker = 0;

            foreach (var c in str)
            {
                var val = c - 'a';
                if ((checker & (1 << val)) > 0)
                {
                    return false;
                }

                checker |= 1 << val;
            }
            return true;
        }

        public void Part2()
        {
            int i = 0;
            int j = 14;
            var slice = _packet[i..j];
            while (!AreUnique(slice))
            {
                i++;
                j++;
                slice = _packet[i..j];
            }


            AOCConsole.WriteLine($"The answer is: {j}");

        }

    }
}