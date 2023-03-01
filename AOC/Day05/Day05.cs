using System.Text.RegularExpressions;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    internal partial class Day05 : DayBase
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
            var cleanedStacks = _stacks.Split($"{Environment.NewLine}{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries);

            var stackInputRows = cleanedStacks.First()
             .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
             .Select(x => x.Chunk(4).Select((x, index) => (Value: x.Skip(1).First(), StackIndex: index)))
             .SkipLast(1);

         //   var part1Stacks = new Stack<char>[stackInputRows.First().Count()].Fill(();
       //     var part2Stacks = new Stack<char>[part1Stacks.Length].Fill(() => new Stack<char>());


          //  AOCConsole.WriteLine($"The answer is: {result}");
        }

   


        public void Part2()
        {

         
           // AOCConsole.WriteLine($"The answer is: {matches}");

        }

    }
}