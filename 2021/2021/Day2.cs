namespace Advent2021;
public class Day2
{

    public static int Navigate(string filename)
    {
        var horizontal = 0;
        var depth = 0;
        var directions = GetDirections(filename);
        directions.ToList().ForEach(d =>
        {
            switch (d.direction)
            {
                case "forward":
                    horizontal += d.steps;
                    break;
                case "down":
                    depth += d.steps;
                    break;
                case "up":
                    depth -= d.steps;
                    break;
            }
        });
        return horizontal * depth;
    }

    public static int Aim(string filename)
    {
        var horizontal = 0;
        var depth = 0;
        var aim = 0;
        var directions = GetDirections(filename);
        directions.ToList().ForEach(d =>
        {
            switch (d.direction)
            {
                case "forward":
                    horizontal += d.steps;
                    depth += d.steps * aim;
                    break;
                case "down":
                    aim += d.steps;
                    break;
                case "up":
                    aim -= d.steps;
                    break;
            }
        });
        return horizontal * depth;
    }
    private static IEnumerable<(string direction, int steps)> GetDirections(string filename)
    {
        using var reader = new StreamReader(filename);
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var direction = line!.Split(" ")[0];
            var amount = int.Parse(line.Split(" ")[1]);
            yield return (direction, amount);
        }
    }
}
