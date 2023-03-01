using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using AOCConsole = System.Console;

namespace AdventOfCode2022.Day
{
    public class Day12 : DayBase
    {
        private readonly string _heightMap;

        record struct Coord(int lat, int lon);
        record struct Symbol(char value);
        record struct Elevation(char value);
        record struct Poi(Symbol symbol, Elevation elevation, int distanceFromGoal);

        Symbol startSymbol = new Symbol('S');
        Symbol goalSymbol = new Symbol('E');
        Elevation lowestElevation = new Elevation('a');
        Elevation highestElevation = new Elevation('z');

        public Day12(int part, string day)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Day{day}", $"Day{day}.txt");

            _heightMap = File.ReadAllText(path);

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
            var lowestSteps = GetPois(_heightMap)
                .Single(poi => poi.symbol == startSymbol)
                .distanceFromGoal;
            AOCConsole.WriteLine($"The answer is: {lowestSteps}");
        }

        IEnumerable<Poi> GetPois(string input)
        {
            var map = ParseMap(input);
            var goal = map.Keys.Single(point => map[point] == goalSymbol);

            // starting from the goal symbol compute shortest paths for each point of 
            // the map using a breadth-first search.
            var poiByCoord = new Dictionary<Coord, Poi>() {
            {goal, new Poi(goalSymbol, GetElevation(goalSymbol), 0)}
        };

            var q = new Queue<Coord>();
            q.Enqueue(goal);
            while (q.Any())
            {
                var thisCoord = q.Dequeue();
                var thisPoi = poiByCoord[thisCoord];

                foreach (var nextCoord in Neighbours(thisCoord).Where(map.ContainsKey))
                {
                    if (poiByCoord.ContainsKey(nextCoord))
                    {
                        continue;
                    }

                    var nextSymbol = map[nextCoord];
                    var nextElevation = GetElevation(nextSymbol);

                    if (thisPoi.elevation.value - nextElevation.value <= 1)
                    {
                        poiByCoord[nextCoord] = new Poi(
                            symbol: nextSymbol,
                            elevation: nextElevation,
                            distanceFromGoal: thisPoi.distanceFromGoal + 1
                        );
                        q.Enqueue(nextCoord);
                    }
                }

            }
            return poiByCoord.Values;
        }

        Elevation GetElevation(Symbol symbol) =>
            symbol.value switch
            {
                'S' => lowestElevation,
                'E' => highestElevation,
                _ => new Elevation(symbol.value)
            };

        ImmutableDictionary<Coord, Symbol> ParseMap(string input)
        {
            var lines = input.Split(Environment.NewLine);
            return (
                from y in Enumerable.Range(0, lines.Length)
                from x in Enumerable.Range(0, lines[0].Length)
                select new KeyValuePair<Coord, Symbol>(
                    new Coord(x, y), new Symbol(lines[y][x])
                )
            ).ToImmutableDictionary();
        }

        IEnumerable<Coord> Neighbours(Coord coord) =>
            new[] {
           coord with {lat = coord.lat + 1},
           coord with {lat = coord.lat - 1},
           coord with {lon = coord.lon + 1},
           coord with {lon = coord.lon - 1},
            };
    

        public void Part2()
        {
               var loewstSteps = GetPois(_heightMap)
                    .Where(poi => poi.elevation == lowestElevation)
                    .Select(poi => poi.distanceFromGoal)
                    .Min();
            AOCConsole.WriteLine($"The answer is: {loewstSteps}");
        }

       
    }
}