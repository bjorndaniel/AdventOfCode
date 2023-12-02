namespace AoC2018;
public class Day9
{
    public static Game ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var info = lines.First().Split(" ");
        var (players, score) = (int.Parse(info[0]), int.Parse(info[6]));
        return new Game(players, score);
    }

    [Solveable("2018/Puzzles/Day9.txt", "Day 9 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var game = ParseInput(filename);
        var scores = RunGame(game);
        return new SolutionResult(scores.Max().ToString());
    }


    [Solveable("2018/Puzzles/Day9.txt", "Day 9 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var game = ParseInput(filename);
        var scores = RunGame(game, true);
        return new SolutionResult(scores.Max().ToString());
    }

    private static long[] RunGame(Game game, bool part2 = false)
    {
        var scores = new long[game.Players];
        var circle = new LinkedList<int>();
        var current = circle.AddFirst(0);

        for (int marble = 1; marble <= (part2 ? game.MarbleScore * 100 : game.MarbleScore); marble++)
        {
            if (marble % 23 == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    current = current?.Previous ?? circle.Last;
                }
                scores[marble % game.Players] += marble + current?.Value ?? 0;
                var remove = current;
                current = remove?.Next ?? circle.First;
                circle.Remove(remove!);
            }
            else
            {
                current = current?.Next ?? circle.First;
                current = circle.AddAfter(current!, marble);
            }
        }
        return scores;
    }

    public record Game(int Players, int MarbleScore) { }
}