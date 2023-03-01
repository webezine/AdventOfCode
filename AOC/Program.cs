using AdventOfCode2022.Day;
using System.Globalization;
using AOCConsole = System.Console;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            AOCConsole.WriteLine("Welcome to the Yearly Advent Of Code (SOC) challenge for 2022.");
            AOCConsole.WriteLine("Completed by Daniel Butcher");
            AOCConsole.WriteLine();
            AOCConsole.WriteLine("Please enter the day you wish to view: (just the number as a numeric format ie 7");
            var day = AOCConsole.ReadLine();
            AOCConsole.WriteLine($"Thanks! You wish to see Day: {day}");
            AOCConsole.WriteLine("Which part would you like to review: Part 1 or 2 ?");
            var part = AOCConsole.ReadLine(); 
            AOCConsole.WriteLine($"Part {part} coming right up...");
            AOCConsole.WriteLine();
            if (day.Length == 1)
            {
                day = string.Concat("0", day);
            }

            switch (day)
            {
                case "01":
                    new Day01(int.Parse(part), day);
                    break;
                case "02":
                    new Day02(int.Parse(part), day);
                    break;
                case "03":
                    new Day03(int.Parse(part), day);
                    break;
                case "04":
                    new Day04(int.Parse(part), day);
                    break;
                case "05":
                    new Day05(int.Parse(part), day);
                    break;
            }
        }

    }
}
