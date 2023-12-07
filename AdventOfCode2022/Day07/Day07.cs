using AdventOfCode;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day07 
    {
        private readonly string[] _commands;

        public Day07(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _commands = File.ReadAllLines(path);

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
            var dirs = new Dictionary<string, int>();
            var cd = new List<string>();

            for (var i = 0; i < _commands.Length; i++)
            {
                if (!_commands[i].StartsWith("$ cd"))
                {
                    continue;
                }

                if (_commands[i] == "$ cd ..") // back
                {
                    cd.RemoveAt(cd.Count() - 1);
                    continue;
                }

                // fwd

                cd.Add(_commands[i].Replace("$ cd ", ""));

                if (!dirs.ContainsKey(_commands[i].Replace("$ cd ", "")))
                {
                    dirs.Add(string.Join("/", cd.ToArray()), 0);
                }
                else
                {
                    continue;
                }

                var x = i + 2; // skip $ ls

                while (true)
                {
                    if (x >= _commands.Length || _commands[x].StartsWith("$ cd"))
                    {
                        break;
                    }

                    if (!_commands[x].StartsWith("dir "))
                    {
                        for (var y = cd.Count() - 1; y >= 0; y--)
                        {
                            dirs[string.Join("/", cd.GetRange(0, y + 1).ToArray())] += int.Parse(_commands[x].Split(" ")[0]);
                        }
                    }
                    x++;
                }
            }
            var directorySum = dirs.Values.Where(x => x <= 1e5).Sum();

            AOCConsole.WriteLine($"The answer is: {directorySum}");
        }

    
        public void Part2()
        {
            var dirs = new Dictionary<string, int>();
            var cd = new List<string>();

            for (var i = 0; i < _commands.Length; i++)
            {
                if (!_commands[i].StartsWith("$ cd"))
                {
                    continue;
                }

                if (_commands[i] == "$ cd ..") // back
                {
                    cd.RemoveAt(cd.Count() - 1);
                    continue;
                }

                // fwd

                cd.Add(_commands[i].Replace("$ cd ", ""));

                if (!dirs.ContainsKey(_commands[i].Replace("$ cd ", "")))
                {
                    dirs.Add(string.Join("/", cd.ToArray()), 0);
                }
                else
                {
                    continue;
                }

                var x = i + 2; // skip $ ls

                while (true)
                {
                    if (x >= _commands.Length || _commands[x].StartsWith("$ cd"))
                    {
                        break;
                    }

                    if (!_commands[x].StartsWith("dir "))
                    {
                        for (var y = cd.Count() - 1; y >= 0; y--)
                        {
                            dirs[string.Join("/", cd.GetRange(0, y + 1).ToArray())] += int.Parse(_commands[x].Split(" ")[0]);
                        }
                    }
                    x++;
                }
            }

            var smallestDir = dirs.OrderBy(x => x.Value).Where(x => x.Value >= 3e7 - (7e7 - dirs.First().Value)).First().Value;

            AOCConsole.WriteLine($"The answer is: {smallestDir}");

        }

    }
}