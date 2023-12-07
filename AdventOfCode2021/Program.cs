using AdventOfCode2021.Day;
using AOCConsole = System.Console;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            AOCConsole.WriteLine("Welcome to the Yearly Advent Of Code (SOC) challenge for 2021.");
            AOCConsole.WriteLine("Completed by Daniel Butcher");
            AOCConsole.WriteLine();
            AOCConsole.WriteLine("Please enter the day you wish to view: (just the number as a numeric format ie 7");
            var day = System.Console.ReadLine();
            AOCConsole.WriteLine($"Thanks! You wish to see Day: {day}");
            AOCConsole.WriteLine("Which part would you like to review: Part 1 or 2 - again please enter as an int");
            var part = System.Console.ReadLine();
            AOCConsole.WriteLine($"Part {part} coming right up...");
            AOCConsole.WriteLine();

            switch (day)
            {
                case "1":
                    new Day1(int.Parse(part), day);
                    break;
                case "2":
                    new Day2(int.Parse(part), day);
                    break;
                case "3":
                    new Day3(int.Parse(part), day);
                    break;
                case "4":
                    new Day4(int.Parse(part), day);
                    break;
                case "5":
                    new Day5(int.Parse(part), day);
                    break;
                case "6":
                    new Day6(int.Parse(part), day);
                    break;
                case "7":
                    new Day7(int.Parse(part), day);
                    break;
                case "8":
                    new Day8(int.Parse(part), day);
                    break;
                case "9":
                    new Day9(int.Parse(part), day);
                    break;
            }
        }

    }
}
