using AdventOfCode;
using System.Text.RegularExpressions;
using AOCConsole = System.Console;

namespace AdventOfCode2023.Day;

public partial class Day02
{
    private readonly string[] _input;

    [GeneratedRegex(@"^Game (?<gameid>\d+): ((?<sets>[^;]*)(; )?)*$", RegexOptions.ExplicitCapture)]
    private static partial Regex GameRegex();

    [GeneratedRegex(@"(\d+) (\w+)")]
    private static partial Regex SetRegex();

    public Day02(int part)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day02", $"Day02-input.txt");
        _input = File.ReadAllLines(path);

        DayOutput.WriteDayPart(part, "02");

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
        var games = BuildGames();
        var red = 12;
        var green = 13;
        var blue = 14;
        var answer = games
            .Where(g => g.DiceVariants.All(s =>
                s.GetValueOrDefault("red") <= red
                && s.GetValueOrDefault("green") <= green
                && s.GetValueOrDefault("blue") <= blue))
            .Sum(g => g.Id);
        AOCConsole.WriteLine($"The answer is: {answer}");
    }

    public void Part2()
    {
        var games = BuildGames();

        var answer = games
                .Select(g => g.DiceVariants.Max(s => s.GetValueOrDefault("red", 1)) *
                    g.DiceVariants.Max(s => s.GetValueOrDefault("green", 1)) *
                    g.DiceVariants.Max(s => s.GetValueOrDefault("blue", 1)))
                .Sum();
        AOCConsole.WriteLine($"The answer is: {answer}");
    }   

    private List<GameSummary> BuildGames()
    {
        var games = _input
            .Select(l => GameRegex().Match(l))
            .Select(m => new GameSummary
            {
                Id = int.Parse(m.Groups["gameid"].Value),
                DiceVariants = m.Groups["sets"]
                    .Captures
                    .Select(c => SetRegex().Matches(c.Value)
                    .OfType<Match>()
                    .Select(m => (num: int.Parse(m.Groups[1].Value), Color: m.Groups[2].Value))
                    .GroupBy(
                        x => x.Color,
                        (k, g) => (color: k, num: g.Sum(x => x.num)))
                        .ToDictionary(x => x.color, x => x.num))
                    .ToList(),
            }).ToList();
        return games;
    }

    private class GameSummary
    {
        public int Id { get; set; }
        public List<Dictionary<string, int>> DiceVariants { get; set; }
    }
}
