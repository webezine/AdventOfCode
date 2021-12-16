using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2021.Day {

    public class Day4 : DayBase
    {
        readonly List<string> input;
        readonly List<int> numbers;
        readonly List<List<List<int>>> boards = new();
        readonly int n;

        public Day4(int part, int day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");
            input = File.ReadAllLines(path).ToList();

            numbers = input[0].Split(",").Select(int.Parse).ToList();
            n = (input.Count - 1) / 6;

            for (int i = 0; i < n; i++)
            {
                var board = new List<List<int>>();

                for (int j = 0; j < 5; j++)
                {
                    board.Add(input[2 + 6 * i + j].Split().Where(x => x.Trim() != "").Select(x => int.Parse(x)).ToList());
                }

                boards.Add(board);
            }
            WriteDayOnePart(part, day);

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
            AOCConsole.WriteLine($"The answer is: {BingoCalcs1()}");
        }


        public void Part2()
        {
            AOCConsole.WriteLine($"The answer is: {BingoCalcs2()}");
        }

        private int BingoCalcs1()
        {
            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    for (var i = 0; i < 5; i++)
                    {
                        for (var j = 0; j < 5; j++)
                        {   
                            if (board[i][j] == number)
                            {
                                board[i][j] = -1;
                            }
                        }
                    }
                }

                foreach (var board in boards)
                {
                    for (var i = 0; i < 5; i++)
                    {
                        if (board[i].Sum() == -5 || board.Select(x => x[i]).Sum() == -5)
                        {
                            return board.SelectMany(x => x).Where(x => x != -1).Sum() * number;
                        }
                    }
                }
            }

            return 0;
        }

        private int BingoCalcs2()
        {
            var result = 0;

            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    for (var i = 0; i < 5; i++)
                    {
                        for (var j = 0; j < 5; j++)
                        {
                            if (board[i][j] == number)
                            {
                                board[i][j] = -1;
                            }
                        }
                    }
                }

                var winningBoards = new List<List<List<int>>>();

                foreach (var board in boards)
                {
                    for (var i = 0; i < 5; i++)
                    {
                        if (board[i].Sum() == -5 || board.Select(x => x[i]).Sum() == -5)
                        {
                            result = board.SelectMany(x => x).Where(x => x != -1).Sum() * number;
                            winningBoards.Add(board);
                        }
                    }
                }

                foreach (var board in winningBoards)
                {
                    boards.Remove(board);
                }
            }

            return result;
        }
    }
}