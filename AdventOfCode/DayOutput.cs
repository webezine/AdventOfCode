namespace AdventOfCode;

public static class DayOutput
{
    public static void WriteDayPart(int part, string day)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}-Part{part}.txt");
        var partText = File.ReadAllLines(path).ToList();
        foreach (var line in partText)
        {
            System.Console.WriteLine(line);
        }
    }
}
