using AdventOfCode;
using System;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day14 
    {
        private readonly string _packets;

        public Day14(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _packets = File.ReadAllText(path);

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
            var parsedPackets = _packets.Split(Environment.NewLine)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => JsonNode.Parse(x))
                .ToList();
        

            var correctPackets = parsedPackets.Chunk(2)
            .Select((pair, index) => ComparePackets(pair[0], pair[1]) < 0 ? index + 1 : 0)
            .Sum();

            AOCConsole.WriteLine($"The answer is: {correctPackets}");
        }

        private int ComparePackets(JsonNode nodeA, JsonNode nodeB)
        {
            if (nodeA is JsonValue && nodeB is JsonValue)
            {
                return (int)nodeA - (int)nodeB;
            }
            else
            {
                var arrayA = nodeA as JsonArray ?? new JsonArray((int)nodeA);
                var arrayB = nodeB as JsonArray ?? new JsonArray((int)nodeB);
                return Enumerable.Zip(arrayA, arrayB)
                    .Select(p => ComparePackets(p.First, p.Second))
                    .FirstOrDefault(c => c != 0, arrayA.Count - arrayB.Count);
            }
        }

        public void Part2()
        {
            var divider = $"[[2]]{Environment.NewLine}[[6]]".Split(Environment.NewLine)
                .Select(x => JsonNode.Parse(x))
                .ToList();

            var packets = _packets.Split(Environment.NewLine)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => JsonNode.Parse(x))
                .Concat(divider)
                .ToList();

            packets.Sort(ComparePackets);
            var result = (packets.IndexOf(divider[0]) + 1) * (packets.IndexOf(divider[1]) + 1);
            AOCConsole.WriteLine($"The answer is: {result}");
        }

       
    }
}