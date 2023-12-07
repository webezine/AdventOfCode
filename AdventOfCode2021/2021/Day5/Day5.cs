using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2021.Day
{
    public class Day5
    {
        readonly List<(int x1, int y1, int x2, int y2)> _lines;

        public Day5(int part, int day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");
            _lines = File.ReadAllLines(path)
                        .Select(line => (int.Parse(line.Split(" -> ")[0].Split(",")[0]),
                                         int.Parse(line.Split(" -> ")[0].Split(",")[1]),
                                         int.Parse(line.Split(" -> ")[1].Split(",")[0]),
                                         int.Parse(line.Split(" -> ")[1].Split(",")[1])))
                        .ToList();

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
            var points = new Dictionary<(int, int), int>();

            foreach (var input in _lines)
            {
                if (input.x1 == input.x2)
                {
                    for (var y = Math.Min(input.y1, input.y2); y <= Math.Max(input.y1, input.y2); y++)
                    {
                        if (points.Keys.Contains((input.x1, y)))
                        {
                            points[(input.x1, y)]++;
                        }
                        else
                        {
                            points[(input.x1, y)] = 1;
                        }
                    }
                }

                if (input.y1 != input.y2)
                {
                    continue;
                }
                for (var x = Math.Min(input.x1, input.x2); x <= Math.Max(input.x1, input.x2); x++)
                {
                    if (points.Keys.Contains((x, input.y1)))
                    {
                        points[(x, input.y1)]++;
                    }
                    else
                    {
                        points[(x, input.y1)] = 1;
                    }
                }
            }

            AOCConsole.WriteLine($"The answer is: {points.Values.Count(x => x >= 2)}");
        }

        public void Part2()
        {
            var points = new Dictionary<(int, int), int>();

            foreach (var input in _lines)
            {
                if (input.x1 == input.x2)
                {
                    for (var y = Math.Min(input.y1, input.y2); y <= Math.Max(input.y1, input.y2); y++)
                    {
                        if (points.Keys.Contains((input.x1, y)))
                        {
                            points[(input.x1, y)]++;
                        }
                        else
                        {
                            points[(input.x1, y)] = 1;
                        }
                    }
                }
                else if (input.y1 == input.y2)
                {
                    for (var x = Math.Min(input.x1, input.x2); x <= Math.Max(input.x1, input.x2); x++)
                    {
                        if (points.Keys.Contains((x, input.y1)))
                        {
                            points[(x, input.y1)]++;
                        }
                        else
                        {
                            points[(x, input.y1)] = 1;
                        }
                    }
                }
                else
                {
                    var p = (input.x1, input.y1);
                    var end = (input.x2, input.y2);
                    var dx = Math.Sign(end.x2 - p.x1);
                    var dy = Math.Sign(end.y2 - p.y1);

                    while (p != end)
                    {
                        if (points.Keys.Contains(p))
                        {
                            points[p]++;
                        }
                        else
                        {
                            points[p] = 1;
                        }

                        p.x1 += dx;
                        p.y1 += dy;
                    }

                    if (points.Keys.Contains(p))
                    {
                        points[p]++;
                    }
                    else
                    {
                        points[p] = 1;
                    }
                }
            }

            AOCConsole.WriteLine($"The answer is: {points.Values.Count(x => x >= 2)}");
        }
    }
}