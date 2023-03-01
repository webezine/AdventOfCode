using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day09 : DayBase
    {
        private readonly List<string> _directions;
        private List<(int x, int y)> _knots = new();
        private HashSet<(int x, int y)> _visited;

        public Day09(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _directions = File.ReadLines(path).ToList();

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

            const int amountOfKnots = 2; 
            for (var i = 0; i < amountOfKnots; i++)
            {
                _knots.Add((0, 0));
            }
            _visited = new HashSet<(int x, int y)> { (0, 0) };
            var history = new Stack<List<(int x, int y)>>();
            history.Push(_knots);

            foreach (var line in _directions)
            {
                var parts = line.Split(" ");
                var direction = parts[0];
                var amount = int.Parse(parts[1]);

                for (var i = 0; i < amount; i++)
                {
                    MoveHead(direction);
                    for (var knotIndex = 0; knotIndex < _knots.Count - 1; knotIndex++)
                    {
                        UpdateFollowingKnot(knotIndex, knotIndex + 1);
                    }

                    history.Push(new(_knots.ToArray()));
                }
            }

            AOCConsole.WriteLine($"The answer is: {_visited.Count}");
        }

        void MoveHead(string direction)
        {
            var head = _knots[0];
            switch (direction)
            {
                case "U":
                    head.y++;
                    break;
                case "R":
                    head.x++;
                    break;
                case "D":
                    head.y--;
                    break;
                case "L":
                    head.x--;
                    break;
                default:
                    throw new ArgumentException("Not valid direction");
            }

            _knots[0] = head;
        }

        void UpdateFollowingKnot(int headIndex, int tailIndex)
        {
            var head = _knots[headIndex];
            var tail = _knots[tailIndex];

            var xDiff = head.x - tail.x;
            var yDiff = head.y - tail.y;

            if (Math.Abs(xDiff) <= 1 && Math.Abs(yDiff) <= 1)
            {
                // Knots are touching, don't move
                return;
            }

            // Should ever only move one tile at a time
            xDiff = Math.Clamp(xDiff, -1, 1);
            yDiff = Math.Clamp(yDiff, -1, 1);

            tail.x += xDiff;
            tail.y += yDiff;

            _knots[tailIndex] = tail;

            if (tailIndex == _knots.Count - 1)
            {
                _visited.Add((tail.x, tail.y));
            }
        }


        public void Part2()
        {
            const int amountOfKnots = 10;
            for (var i = 0; i < amountOfKnots; i++)
            {
                _knots.Add((0, 0));
            }
            _visited = new HashSet<(int x, int y)> { (0, 0) };
            var history = new Stack<List<(int x, int y)>>();
            history.Push(_knots);

            foreach (var line in _directions)
            {
                var parts = line.Split(" ");
                var direction = parts[0];
                var amount = int.Parse(parts[1]);

                for (var i = 0; i < amount; i++)
                {
                    MoveHead(direction);
                    for (var knotIndex = 0; knotIndex < _knots.Count - 1; knotIndex++)
                    {
                        UpdateFollowingKnot(knotIndex, knotIndex + 1);
                    }

                    history.Push(new(_knots.ToArray()));
                }
            }

            AOCConsole.WriteLine($"The answer is: {_visited.Count}");
        }

   

    }
}