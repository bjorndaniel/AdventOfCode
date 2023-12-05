using System;
using System.Collections.Concurrent;
using static AoC2023.Day5;

namespace AoC2023;
public class Day5
{
    public static Almanac ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var seeds = lines.First().Split(":").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(_ => long.Parse(_)).ToList();
        var result = new Almanac(seeds, new List<Map>(), new List<(long start, long end)>());
        for(int i = 0; i< seeds.Count; i += 2)
        {
            result.SeedRanges.Add((seeds[i], seeds[i] + seeds[i + 1] - 1));
        }


        Map map = null!;
        foreach (var line in lines.Skip(2))
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            if (char.IsNumber(line[0]))
            {
                var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(_ => long.Parse(_)).ToList();
                map.Ranges.Add((numbers[0], numbers[1], numbers[2]));
            }
            else
            {
                if (map != null)
                {
                    result.Maps.Add(map);
                }
                map = new Map(new List<(long destination, long source, long length)>(), GetMapType(line));
            }

        }
        if(map != null)
        {
            result.Maps.Add(map);
        }
        return result;
        MapType GetMapType(string line) =>
            line[..2] switch
            {
                "se" => MapType.SeedSoil,
                "so" => MapType.SoilFertilizer,
                "fe" => MapType.FertilizerWater,
                "wa" => MapType.WaterLight,
                "li" => MapType.LightTemperature,
                "te" => MapType.TemperatureHumidity,
                "hu" => MapType.HumidityLocation,
                _ => throw new ArgumentException($"Missing enum {line[..2]}")
            };
    }

    [Solveable("2023/Puzzles/Day5.txt", "Day 5 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var almanac = ParseInput(filename);
        var sum = long.MaxValue;
        foreach(var seed in almanac.Seeds)
        {
            var soil = almanac.Maps.First(_ => _.Type == MapType.SeedSoil).GetDestination(seed);
            var fert = almanac.Maps.First(_ => _.Type == MapType.SoilFertilizer).GetDestination(soil);
            var water = almanac.Maps.First(_ => _.Type == MapType.FertilizerWater).GetDestination(fert);
            var light = almanac.Maps.First(_ => _.Type == MapType.WaterLight).GetDestination(water);
            var temp = almanac.Maps.First(_ => _.Type == MapType.LightTemperature).GetDestination(light);
            var hum = almanac.Maps.First(_ => _.Type == MapType.TemperatureHumidity).GetDestination(temp);
            var loc = almanac.Maps.First(_ => _.Type == MapType.HumidityLocation).GetDestination(hum);
            if(loc < sum)
            {
                sum = loc;
            }

        }
        return new SolutionResult(sum.ToString());
    }

    [Solveable("2023/Puzzles/Day5.txt", "Day 5 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var almanac = ParseInput(filename);
        var sums = new BlockingCollection<long>();
        printer.Flush();
        Parallel.ForEach(almanac.SeedRanges, new ParallelOptions { MaxDegreeOfParallelism = 100 }, (range) =>
        {
            for (long i = range.start; i <= range.end; i++)
            {
                var soil = almanac.Maps.First(_ => _.Type == MapType.SeedSoil).GetMapping(i);
                var fert = almanac.Maps.First(_ => _.Type == MapType.SoilFertilizer).GetMapping(soil);
                var water = almanac.Maps.First(_ => _.Type == MapType.FertilizerWater).GetMapping(fert);
                var light = almanac.Maps.First(_ => _.Type == MapType.WaterLight).GetMapping(water);
                var temp = almanac.Maps.First(_ => _.Type == MapType.LightTemperature).GetMapping(light);
                var hum = almanac.Maps.First(_ => _.Type == MapType.TemperatureHumidity).GetMapping(temp);
                var loc = almanac.Maps.First(_ => _.Type == MapType.HumidityLocation).GetMapping(hum);
                sums.Add(loc);
            }
        });
        return new SolutionResult(sums.Min().ToString());
    }

    public record Almanac(List<long> Seeds, List<Map> Maps, List<(long start, long end)> SeedRanges) { }

    public class Map
    {

        public Map(List<(long destination, long source, long length)> ranges, MapType type)
        {
            Ranges = ranges;
            Type = type;
        }

        public List<(long destination, long source, long length)> Ranges { get; private set; }

        public MapType Type { get; private set; }

        public ConcurrentDictionary<long, long> Mappings { get; private set; } = new();

        public void CreateMappings()
        {
            Parallel.ForEach(Ranges, range =>
            {
                for (long i = range.source; i < (range.source + range.length); i++)
                {
                    if (!Mappings.ContainsKey(i))
                    {
                        Mappings.TryAdd(i, GetDestination(i));
                    }
                }
            });
        }

        public long GetMapping(long source)
        {
            if (Mappings.ContainsKey(source))
            {
                return Mappings[source];
            }
            var res = (source, GetDestination(source));
            Mappings.TryAdd(res.source, res.Item2);
            return res.Item2;
        }

        public long GetDestination(long source)
        {
            var range = Ranges.FirstOrDefault(_ => source >= _.source && source <= (_.source + _.length));
            if (range == default)
            {
                return source;
            }
            
            return range.destination + (source - range.source) ;
        }
    }

    public enum MapType
    {
        SeedSoil,
        SoilFertilizer,
        FertilizerWater,
        WaterLight,
        LightTemperature,
        TemperatureHumidity,
        HumidityLocation
    }
}