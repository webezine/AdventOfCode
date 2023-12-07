using AdventOfCode;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day02 
    {
        private readonly string _moves;
        private enum Shape
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        private static Shape GetOpponentShape(string s) => s switch
        {
            "A" => Shape.Rock,
            "B" => Shape.Paper,
            "C" => Shape.Scissors,
            _ => throw new ArgumentOutOfRangeException()
        };

        public Day02(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _moves = File.ReadAllText(path);

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
            var totalPoints = _moves.Split("\n").Select(CalculateRoundScorePartOne).Sum();

            AOCConsole.WriteLine($"The answer is: {totalPoints}");
        }

        private static int CalculateRoundScorePartOne(string moves)
        {
            var res = 0;
            var p = moves.Split(" ");
            Debug.Assert(p.Length == 2);
            var a = GetOpponentShape(p[0]);
            p[1] = p[1].Replace("\r", "");
            var x = p[1] switch
            {
                "X" => Shape.Rock,
                "Y" => Shape.Paper,
                "Z" => Shape.Scissors,
                _ => throw new ArgumentOutOfRangeException()
            };
            res += (int)x;
            if (a == x)
            {
                res += 3;
            }
            else if ((a == Shape.Rock && x == Shape.Paper) || (a == Shape.Paper && x == Shape.Scissors) || (a == Shape.Scissors && x == Shape.Rock))
            {
                res += 6;
            }
            return res;
        }

        public void Part2()
        {
            var totalPoints = _moves.Split("\n").Select(CalculateRoundScorePartTwo).Sum();

            AOCConsole.WriteLine($"The answer is: {totalPoints}");
        }

        private static int CalculateRoundScorePartTwo(string moves)
        {
            var res = 0;
            var p = moves.Split(" ");
            Debug.Assert(p.Length == 2);
            var a = GetOpponentShape(p[0]);
            p[1] = p[1].Replace("\r", "");
            res += p[1] switch
            {
                "X" => a switch
                {
                    Shape.Rock => (int)Shape.Scissors,
                    Shape.Paper => (int)Shape.Rock,
                    Shape.Scissors => (int)Shape.Paper,
                    _ => throw new ArgumentOutOfRangeException()
                },
                "Y" => 3 + (int)a,
                "Z" => a switch
                {
                    Shape.Rock => (int)Shape.Paper,
                    Shape.Paper => (int)Shape.Scissors,
                    Shape.Scissors => (int)Shape.Rock,
                    _ => throw new ArgumentOutOfRangeException()
                } + 6,
                _ => throw new ArgumentOutOfRangeException()
            };

            return res;
        }
    }
}