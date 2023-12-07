using AdventOfCode2022.Day11;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day11 
    {
        private readonly string _monkeyBusiness;
        delegate bool TryParse(string pattern, out string arg);

        public Day11(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _monkeyBusiness = File.ReadAllText(path);

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
            var monkeys = _monkeyBusiness.Split($"{Environment.NewLine}{Environment.NewLine}").Select(ParseMonkey).ToList();
            RunAllMonkeys(20, monkeys, w => w / 3);
            
            var result = monkeys
            .OrderByDescending(monkey => monkey.inspectedItems)
            .Take(2)
            .Aggregate(1L, (res, monkey) => res * monkey.inspectedItems);

            AOCConsole.WriteLine($"The answer is: {result}");
        }

        Monkey ParseMonkey(string input)
        {
            var monkey = new Monkey();

            foreach (var line in input.Split($"{Environment.NewLine}"))
            {
                var tryParse = LineParser(line);
                if (tryParse(@"Monkey (\d+)", out var arg))
                {
                    // pass
                }
                else if (tryParse("Starting items: (.*)", out arg))
                {
                    monkey.items = new Queue<long>(arg.Split(", ").Select(long.Parse));
                }
                else if (tryParse(@"Operation: new = old \* old", out _))
                {
                    monkey.operation = old => old * old;
                }
                else if (tryParse(@"Operation: new = old \* (\d+)", out arg))
                {
                    monkey.operation = old => old * int.Parse(arg);
                }
                else if (tryParse(@"Operation: new = old \+ (\d+)", out arg))
                {
                    monkey.operation = old => old + int.Parse(arg);
                }
                else if (tryParse(@"Test: divisible by (\d+)", out arg))
                {
                    monkey.mod = int.Parse(arg);
                }
                else if (tryParse(@"If true: throw to monkey (\d+)", out arg))
                {
                    monkey.passToMonkeyIfDivides = int.Parse(arg);
                }
                else if (tryParse(@"If false: throw to monkey (\d+)", out arg))
                {
                    monkey.passToMonkeyOtherwise = int.Parse(arg);
                }
                else if (line == "\r")
                {
                    //window`s parse carriage return
                }
                else
                {
                    throw new ArgumentException(string.Concat("Whoops we have an issue: ", line));
                }
            }
            return monkey;
        }

        TryParse LineParser(string line)
        {
            bool match(string pattern, out string arg)
            {
                var match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    arg = match.Groups[match.Groups.Count - 1].Value;
                    return true;
                }
                else
                {
                    arg = "";
                    return false;
                }
            }
            return match;
        }

        void RunAllMonkeys(int rounds, List<Monkey> monkeys, Func<long, long> updateWorryLevel)
        {
            for (var i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.items.Any())
                    {
                        monkey.inspectedItems++;

                        var item = monkey.items.Dequeue();
                        item = monkey.operation(item);
                        item = updateWorryLevel(item);

                        var targetMonkey = item % monkey.mod == 0 ?
                            monkey.passToMonkeyIfDivides :
                            monkey.passToMonkeyOtherwise;

                        monkeys[targetMonkey].items.Enqueue(item);
                    }
                }
            }
        }


        public void Part2()
        {
            var monkeys = _monkeyBusiness.Split($"{Environment.NewLine}{Environment.NewLine}").Select(ParseMonkey).ToList();
            var mod = monkeys.Aggregate(1, (mod, monkey) => mod * monkey.mod);
            RunAllMonkeys(10_000, monkeys, w => w % mod);
            var result = monkeys
                     .OrderByDescending(monkey => monkey.inspectedItems)
                     .Take(2)
                     .Aggregate(1L, (res, monkey) => res * monkey.inspectedItems);
            AOCConsole.WriteLine($"The answer is: {result}");
        }

       
    }
}