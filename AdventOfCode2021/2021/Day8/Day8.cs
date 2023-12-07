using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AOCConsole = System.Console;

namespace AdventOfCode2021.Day
{
    public class Day8
    {
        readonly string[] _input;
        private static string wordRegex = @"(\w+)";

        public Day8(int part, int day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _input = File.ReadAllLines(path);

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
            var result = 0;
            foreach (var line in _input)
            {
                var numbers = line.Split("|")[1];
                var matches = Regex.Matches(numbers, wordRegex);
                for (var i = 0; i < matches.Count; i++)
                {
                    var match = matches[i].Value;
                    if(match.Length is 2 or 4 or 3 or 7) {
                        result++;
                    }
                }
            }
            AOCConsole.WriteLine($"The answer is: {result}");
        }

        public void Part2()
        {
            var result = 0;
            foreach (var line in _input)
            {
                var decode = line.Split("|")[0];
                var matchesDecode = Regex.Matches(decode, wordRegex);
                var onePattern = "";
                var fourPattern = "";
                var sevenPattern = "";
                var eightPattern = "";

                for (var i = 0; i < matchesDecode.Count; i++)
                {
                    var match = matchesDecode[i].Value;
                    switch (match.Length)
                    {
                        case 2:
                            onePattern = match;
                            break;
                        case 3:
                            sevenPattern = match;
                            break;
                        case 4:
                            fourPattern = match;
                            break;
                        case 7:
                            eightPattern = match;
                            break;
                    }
                }

                var fourAndEightDiff = eightPattern.ToCharArray().Where(x => !fourPattern.Contains(x)).ToArray();
                var numbers = line.Split("|")[1];
                var sb = new StringBuilder();
                var matchesNumbers = Regex.Matches(numbers, wordRegex);
                for (var i = 0; i < matchesNumbers.Count; i++)
                {
                    var match = matchesNumbers[i].Value;
                    switch (match.Length)
                    {
                        case 2:
                            sb.Append("1");
                            break;
                        case 3:
                            sb.Append("7");
                            break;
                        case 4:
                            sb.Append("4");
                            break;
                        case 5:
                            sb.Append(TwoThreeOrFive(onePattern, fourAndEightDiff, match));
                            break;
                        case 6:
                            sb.Append(ZeroSixOrNine(fourPattern, sevenPattern, match));
                            break;
                        case 7:
                            sb.Append("8");
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }

                result += int.Parse(sb.ToString());
            }

            AOCConsole.WriteLine($"The answer is: {result}");
        }
        private static string TwoThreeOrFive(string onePattern, IEnumerable<char> fourAndEightDiff, string numberSequence)
        {
            if (onePattern.ToCharArray().All(numberSequence.Contains))
            {
                return "3";
            }
            
            return fourAndEightDiff.All(x => numberSequence.ToCharArray().Contains(x)) ? "2" : "5";
        }

        private static string ZeroSixOrNine(string fourPattern, string sevenPattern, string numberSequence)
        {
            if (fourPattern.ToCharArray().All(numberSequence.Contains))
            {
                return "9";
            }

            return sevenPattern.ToCharArray().All(numberSequence.Contains) ? "0" : "6";
        }
    }
}