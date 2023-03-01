using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day05 : DayBase
    {
        private readonly string _stacks;
        public static List<Stack<char>> stacks = new();

        public Day05(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _stacks = File.ReadAllText(path);

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
            var (state, instructions) = ParseInput();
            foreach (var i in instructions)
            {
                // move x from y to z
                for (int j = 0; j < i.count; j++)
                {
                    var c = state[i.from].Pop();
                    state[i.to].Push(c);
                }
            }

            var s = state.Keys.Aggregate("", (current, key) => current + state[key].Peek());

            AOCConsole.WriteLine($"The answer is: {s}");
        }

        private static Dictionary<int, Stack<char>> ParseState(string initState)
        {
            var l = new Dictionary<int, Stack<char>>();
            var lu = new List<int>();
            var rows = initState.Split("\n").Reverse().ToList();
            var indexRow = rows[0];
            foreach (int i in indexRow.Split(" ").Where(x => x != "").Select(int.Parse))
            {
                l[i] = new Stack<char>();
                lu.Add(i);
            }

            for (int i = 1; i < rows.Count; i++)
            {
                for (int j = 0; j < rows[i].Length; j += 4)
                {
                    var container = rows[i][j + 1];
                    if (container == 32) continue;
                    l[lu[j / 4]].Push(container);
                }
            }
            return l;
        }

        private (Dictionary<int, Stack<char>> state, (int count, int from, int to)[] instructions) ParseInput()
        {
            var sections = _stacks.Split($"{Environment.NewLine}{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries);
            var state = ParseState(sections[0]);
            var instructions = sections[1].Split("\n").Where(s => s.Length > 0)
                .Select(s =>
                {

                    var p = s.Split(" ");
                    return (count: int.Parse(p[1]), from: int.Parse(p[3]), to: int.Parse(p[5]));
                }
                ).ToArray();
            return (state, instructions);
        }


        public void Part2()
        {
            var (state, instructions) = ParseInput();

            Stack<char> t;
            foreach (var instruction in instructions)
            {
                t = new();
                for (int i = 0; i < instruction.count; i++)
                {
                    t.Push(state[instruction.from].Pop());
                }
                for (int i = 0; i < instruction.count; i++)
                {
                    state[instruction.to].Push(t.Pop());
                }
            }
            var result = state.Keys.Aggregate("", (current, key) => current + state[key].Peek());

            AOCConsole.WriteLine($"The answer is: {result}");

        }

    }
}