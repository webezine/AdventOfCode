using AdventOfCode;
using System.IO;
using AOCConsole = System.Console;

namespace AdventOfCode2023.Day;

public class Day03
{
    private readonly string[] _input;
    private int width;
    private int height;

    public Day03(int part)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day03", $"Day03-input.txt");
        _input = File.ReadAllLines(path);
        
        width = _input[0].Length;
        height = _input.Length;
        DayOutput.WriteDayPart(part, "03");

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
        var partsList = BuildPartsList();
        var total = 0;
        foreach(var part in partsList)
            if (part.Symbols)
                total += part.Value;

        AOCConsole.WriteLine($"The answer is: {total} ");
    }

    private List<Part> BuildPartsList()
    {
        Dictionary<(int, int), Gear> gears = new();
        List<Part> parts = new();

        for (int i = 0; i < _input.Length; i++)
        {
            string line = _input[i];
            for (int j = 0; j < line.Length; j++)
            {
                char c = line[j];
                if (char.IsDigit(c))
                {
                    int endIndex = j + 1;
                    while (endIndex < line.Length)
                    {
                        char n = line[endIndex];
                        if (!char.IsDigit(n))
                            break;

                        endIndex++;
                    }

                    Part part = new();
                    part.Value = int.Parse(line.Substring(j, endIndex - j));
                    if (HasSymbol(j - 1, endIndex, i - 1) || HasSymbol(j - 1, endIndex, i) || HasSymbol(j - 1, endIndex, i + 1))
                        part.Symbols = true;

                    UpdateGear(j - 1, endIndex, i - 1, part, gears);
                    UpdateGear(j - 1, endIndex, i, part, gears);
                    UpdateGear(j - 1, endIndex, i + 1, part, gears);
                    parts.Add(part);

                    j = endIndex;
                }
            }
        }
        return parts;
    }
    
    private void UpdateGear(int startX, int endX, int y, Part part, Dictionary<(int, int), Gear> gears)
    {
        if (y < 0 || y >= height) 
            return;

        string line = _input[y];
        for (int i = startX >= 0 ? startX : 0; i < width && i <= endX; i++)
        {
            if (line[i] == '*')
            {
                if (gears.TryGetValue((i, y), out Gear value))
                {
                    value.Parts.Add(part);
                    part.Gears.Add(value);
                }
                else
                {
                    Gear gear = new() { X = i, Y = y };
                    gear.Parts.Add(part);
                    part.Gears.Add(gear);
                    gears.Add((i, y), gear);
                }
            }
        }
    }

    private bool HasSymbol(int startX, int endX, int y)
    {
        if (y < 0 || y >= height)
            return false;

        string line = _input[y];
        for (int i = startX >= 0 ? startX : 0; i < width && i <= endX; i++)
            if (line[i] != '.' && !char.IsDigit(line[i]))
                return true;

        return false;
    }

    public void Part2()
    {
        var partsList = BuildPartsList();
        int total = 0;
        HashSet<(int, int)> gearsSeen = [];
        foreach(var part in partsList)
            if (part.Gears.Count > 0)
                foreach(var gear in part.Gears)
                    if (gear.Parts.Count == 2 && gearsSeen.Add((gear.X, gear.Y)))
                        total += gear.Parts[0].Value * gear.Parts[1].Value;

        AOCConsole.WriteLine($"The answer is: {total} ");
    }

    private class Part
    {
        public int Value;
        public List<Gear> Gears = new();
        public bool Symbols = false;
        public override string ToString()
            => string.Concat(Value, "=", Symbols, ",", Gears.Count);
    }

    private class Gear
    {
        public int X, Y;
        public List<Part> Parts = new();
        public override string ToString()
            => string.Concat(X, ",", Y);
    }
}
