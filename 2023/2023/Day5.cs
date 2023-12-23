namespace AoC2023;
public class Day5
{
    public static Almanac ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var seeds = lines.First().Split(":").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(_ => long.Parse(_)).ToList();
        var result = new Almanac(seeds, [], []);
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
                map = new Map([], GetMapType(line));
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

    //This was finished with the help of this walk through: https://www.youtube.com/watch?v=NmxHw_bHhGM
    [Solveable("2023/Puzzles/Day5.txt", "Day 5 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var almanac = ParseInput(filename);
        var sums = new BlockingCollection<long>();
        printer.Flush();
        var seedQueue = new Queue<(long start, long end)>();
        almanac.SeedRanges.ForEach(_ => seedQueue.Enqueue(_));

        // Iterate through each map in the almanac, ordered by type
        foreach (var map in almanac.Maps.OrderBy(_ => _.Type))
        {
            var newSeedQueue = new Queue<(long start, long end)>();
            while (seedQueue.Any())
            {
                var seedRange = seedQueue.Dequeue();
                var hits = false;

                // Iterate through each range in the current map
                foreach (var range in map.Ranges)
                {
                    var start = long.Max(seedRange.start, range.source);
                    var end = long.Min(seedRange.end, range.source + range.length);

                    // Check if the range overlaps with the seed range
                    if (start < end)
                    {
                        // Calculate the new seed range based on the mapping
                        newSeedQueue.Enqueue((start - range.source + range.destination, end - range.source + range.destination));

                        // Check if there are additional seed ranges to enqueue
                        if (start > seedRange.start)
                        {
                            seedQueue.Enqueue((seedRange.start, start));
                        }
                        if (seedRange.end > end)
                        {
                            seedQueue.Enqueue((end, seedRange.end));
                        }
                        hits = true;
                    }
                }

                // If no overlaps were found, enqueue the seed range as is
                if (!hits)
                {
                    newSeedQueue.Enqueue(seedRange);
                }
            }

            // Update the seed queue with the new seed ranges
            seedQueue = newSeedQueue;
        }

        // Return the smallest starting value from the seed queue as the solution
        return new SolutionResult(seedQueue.Min(_ => _.start).ToString());
    }

    public record Almanac(List<long> Seeds, List<Map> Maps, List<(long start, long end)> SeedRanges) { }

    public class Map(List<(long destination, long source, long length)> ranges, MapType type)
    {
        public List<(long destination, long source, long length)> Ranges { get; private set; } = ranges;

        public MapType Type { get; private set; } = type;

        public ConcurrentDictionary<long, long> Mappings { get; private set; } = new();

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