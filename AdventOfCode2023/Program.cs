﻿using AdventOfCode2023.Day;
using AOCConsole = System.Console;

namespace Console;

class Program
{
    static void Main(string[] args)
    {
        AOCConsole.WriteLine("Welcome to the Yearly Advent Of Code (SOC) challenge for 2023.");
        AOCConsole.WriteLine("Completed by Daniel Butcher");
        AOCConsole.WriteLine();
        AOCConsole.WriteLine("Please enter the day you wish to view: (just the number as a numeric format ie 7");
        var day = AOCConsole.ReadLine();
        AOCConsole.WriteLine($"Thanks! You wish to see Day: {day}");
        AOCConsole.WriteLine("Which part would you like to review: Part 1 or 2 ?");
        var part = AOCConsole.ReadLine();
        AOCConsole.WriteLine($"Part {part} coming right up...");
        AOCConsole.WriteLine();
    
        switch (day)
        {
            case "01":
            case "1":
                new Day01(int.Parse(part));
                break;

            case "02":
            case "2":
                new Day02(int.Parse(part));
                break;

            case "03":
            case "3":
                new Day03(int.Parse(part));
                break;

        }
    }

}
