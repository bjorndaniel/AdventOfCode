namespace AoC2023;
public class Day2
{
    public static List<Game> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Game>();
        foreach (var line in lines)
        {
            var game = ParseGame(line);
            result.Add(game);
        }
        return result;
        
        Game ParseGame(string line)
        {
            var parts = line.Split(":");
            var id = int.Parse(parts[0].Split(" ").Last());
            var gameGrabs = new List<Grab>();
            var grabs = parts[1].Split(";");
            foreach(var g in grabs)
            {
                gameGrabs.Add(GetGrab(g));
            }

            return new Game(id, gameGrabs);

            Grab GetGrab(string grab)
            {
                var cubes = new List<Cube>();
                var cubeParts = grab.Split(",");
                foreach (var c in cubeParts)
                {
                    var cb = c.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    cubes.AddRange(Enumerable.Range(0, int.Parse(cb[0])).Select(_ => new Cube(GetEnum(cb[1]))).ToList());
                }
                return new Grab(cubes);
            }

            CubeColor GetEnum(string color) => color switch
            {
                "blue" => CubeColor.Blue,
                "green" => CubeColor.Green,
                "red" => CubeColor.Red,
                _ => throw new Exception($"Unknown color {color}")
            };
        }   
    }

    [Solveable("2023/Puzzles/Day2.txt", "Day 2 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var games = ParseInput(filename);
        var possible = games.Where(_ => _.Grabs.All(_ => _.RedCount <=12 && _.GreenCount <= 13 && _.BlueCount <= 14 )).ToList();
        return new SolutionResult(possible.Sum(_ => _.Id).ToString());
    }

    [Solveable("2023/Puzzles/Day2.txt", "Day 2 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var games = ParseInput(filename);
        var result = games.Sum(_ => _.Power());
        return new SolutionResult(result.ToString());
    }

    public record Game(int Id, List<Grab> Grabs) 
    {
        public int Power()
        {
            var minRed = Grabs.Max(_ => _.RedCount);
            var minGreen = Grabs.Max(_ => _.GreenCount);
            var minBlue = Grabs.Max(_ => _.BlueCount);
            return minRed * minGreen * minBlue;
        }
    }

    public record Grab(List<Cube> Cubes) 
    {
        public int RedCount => Cubes.Count(_ => _.CubeColor == CubeColor.Red);
        public int BlueCount => Cubes.Count(_ => _.CubeColor == CubeColor.Blue);
        public int GreenCount => Cubes.Count(_ => _.CubeColor == CubeColor.Green);
    }

    public record Cube(CubeColor CubeColor) { }

    public enum CubeColor
    {
       Blue,
       Green,
       Red
    }
}