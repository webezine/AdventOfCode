using System.IO;
using System.Linq;

namespace AdventOfCode2021.Day
{
    public abstract class DayBase
    {
        public static void WriteDayOnePart(int part, int day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}-Part{part}.txt");
            var partText = File.ReadAllLines(path).ToList();
            foreach (var line in partText)
            {
                System.Console.WriteLine(line);
            }
        }
    }
}
