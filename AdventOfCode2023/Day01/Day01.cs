using AdventOfCode;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AOCConsole = System.Console;

namespace AdventOfCode2023.Day;

public partial class Day01
{
    private readonly string[] _input;
    [GeneratedRegex("\\d")]
    private static partial Regex DigitRegex();

    [GeneratedRegex("\\d|one|two|three|four|five|six|seven|eight|nine")]
    private static partial Regex StringDigitRegexLeft();

    [GeneratedRegex("\\d|one|two|three|four|five|six|seven|eight|nine", RegexOptions.RightToLeft)]
    private static partial Regex StringDigitRegexRight();

    public Day01(int part)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day01", $"Day01.txt");
        _input = File.ReadAllLines(path);

        DayOutput.WriteDayPart(part, "01");

        if (part == 1)
        {
            Part1();
        }
        else
        {
            Part2();
        }
    }


    public void Part1_Original()
    {
        List<int> lineValues = new();

        foreach(var line in _input)
        {
            List<string> numbersInLine = new();
            foreach(var character in line)
            {
                if (char.IsNumber(character))
                    numbersInLine.Add(character.ToString());
            }
            
            if (!numbersInLine.Any())
                continue;

            if (numbersInLine.Count == 1)
            {
                var combined = string.Concat(numbersInLine.First(), numbersInLine.First());
                lineValues.Add(int.Parse(combined));
            }
            else
            {
                var combined = string.Concat(numbersInLine.First(), numbersInLine.Last());
                lineValues.Add(int.Parse(combined));
            }
        }

        var answer = lineValues.Sum();
        AOCConsole.WriteLine($"The answer is: {answer}");
    }

    public void Part1()
    {
        var regex = DigitRegex();

        var digitsInString = _input
            .Select(l => regex.Matches(l))
            .Select(m =>
                (int.Parse(m.First().Value) * 10)
                + int.Parse(m.Last().Value))
            .ToList();

        var answer = digitsInString.Sum();
        AOCConsole.WriteLine($"The answer is: {answer}");
    }

    public void Part2_original()
    {
        List<int> lineValues = new();

        foreach (var line in _input)
        {
            Dictionary<int, string> numbersInLine = new();
            foreach (var character in line.Select((value, i) => new { i, value }))
            {
                if (char.IsNumber(character.value))
                    numbersInLine.Add(character.i, character.value.ToString());
            }

            FindStringNumbers(line, numbersInLine);

            var sortedDict = numbersInLine.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            if (!sortedDict.Any())
                continue;

            if (sortedDict.Count == 1)
            {
                var combined = string.Concat(sortedDict.First().Value, sortedDict.First().Value);
                lineValues.Add(int.Parse(combined));
            }
            else
            {
                var combined = string.Concat(sortedDict.First().Value, sortedDict.Last().Value);
                lineValues.Add(int.Parse(combined));
            }
        }
        var answer = lineValues.Sum();


        AOCConsole.WriteLine($"The answer is: {answer}");
    }

    public void Part2()
    {
        var regex1 = StringDigitRegexLeft();
        var regex2 = StringDigitRegexRight();

        var digitsInString = _input
            .Select(l =>
                (regex1.Match(l).Value.ParseToInt() * 10)
                + regex2.Match(l).Value.ParseToInt())
            .ToList();

        var answer = digitsInString.Sum();
        AOCConsole.WriteLine($"The answer is: {answer}");
    }

    private static void FindStringNumbers(string line, Dictionary<int, string> numbersInLine)
    {
        if (line.Contains("one", StringComparison.CurrentCultureIgnoreCase))
        {
            var indexPostion = line.GetAllIndexes("one").ToList();
            indexPostion.Select(x => numbersInLine.TryAdd(x, "1")).ToList();
        }

        if (line.Contains("two", StringComparison.CurrentCultureIgnoreCase))
        {
            var indexPostion = line.GetAllIndexes("two").ToList();
            indexPostion.Select(x => numbersInLine.TryAdd(x, "2")).ToList();
        }

        if (line.Contains("three", StringComparison.CurrentCultureIgnoreCase))
        {
            var indexPostion = line.GetAllIndexes("three").ToList();
            indexPostion.Select(x => numbersInLine.TryAdd(x, "3")).ToList();
        }

        if (line.Contains("four", StringComparison.CurrentCultureIgnoreCase))
        {
            var indexPostion = line.GetAllIndexes("four").ToList();
            indexPostion.Select(x => numbersInLine.TryAdd(x, "4")).ToList();
        }

        if (line.Contains("five", StringComparison.CurrentCultureIgnoreCase))
        {
            var indexPostion = line.GetAllIndexes("five").ToList();
            indexPostion.Select(x => numbersInLine.TryAdd(x, "5")).ToList();
        }

        if (line.Contains("six", StringComparison.CurrentCultureIgnoreCase))
        {
            var indexPostion = line.GetAllIndexes("six").ToList();
            indexPostion.Select(x => numbersInLine.TryAdd(x, "6")).ToList();
        }

        if (line.Contains("seven", StringComparison.CurrentCultureIgnoreCase))
        {
            var indexPostion = line.GetAllIndexes("seven").ToList();
            indexPostion.Select(x => numbersInLine.TryAdd(x, "7")).ToList();
        }

        if (line.Contains("eight", StringComparison.CurrentCultureIgnoreCase))
        {
            var indexPostion = line.GetAllIndexes("eight").ToList();
            indexPostion.Select(x => numbersInLine.TryAdd(x, "8")).ToList();
        }

        if (line.Contains("nine", StringComparison.CurrentCultureIgnoreCase))
        {
            var indexPostion = line.GetAllIndexes("nine").ToList();
            indexPostion.Select(x => numbersInLine.TryAdd(x, "9")).ToList();
        }
    }
   

}

public static class Extension
{
    public static IEnumerable<int> GetAllIndexes(this string source, string matchString)
    {
        matchString = Regex.Escape(matchString);
        return from Match match in Regex.Matches(source, matchString)
               select match.Index;
    }

    public static int ParseToInt(this string s) =>
            char.IsDigit(s[0]) ? s[0] - '0' :
            s switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                _ => throw new UnreachableException(),
            };
}