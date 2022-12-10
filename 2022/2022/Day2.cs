namespace AoC2022;
public static class Day2
{
    public static IEnumerable<Round> ParseInput(string filename, bool round2 = false)
    {
        foreach (var line in File.ReadAllLines(filename))
        {
            var parts = line.Split(' ');
            yield return new Round(FromString(parts[0], round2), FromString(parts[1], round2));
        }

        static RPS FromString(string input, bool round2)
        {
            return input switch
            {
                "A" => RPS.Rock,
                "X" => round2 ? RPS.PlayerLose : RPS.Rock,
                "B" => RPS.Paper,
                "Y" => round2 ? RPS.PlayerDraw : RPS.Paper,
                "C" => RPS.Scissors,
                "Z" => round2 ? RPS.PlayerWin : RPS.Scissors,
                _ => throw new Exception($"Unknown input {input}")
            };
        }
    }

    public static long SolvePart1(string filename) =>
        ParseInput(filename).Sum(_ => _.GetMatchResult());

    public static long SolvePart2(string filename) =>
        ParseInput(filename, true).Sum(_ => _.GetMatchResult());

}

public enum RPS
{
    Rock = 1,
    Paper = 2,
    Scissors = 3,
    PlayerDraw,
    PlayerLose,
    PlayerWin
}

public enum RoundResult
{
    Loss = 0,
    Draw = 3,
    Win = 6
}

public record Round(RPS Opponent, RPS Player)
{
    public int GetMatchResult()
    {
        return Opponent switch
        {
            RPS.Rock =>
                Player switch
                {
                    RPS.Rock => (int)RoundResult.Draw + (int)RPS.Rock,
                    RPS.Paper => (int)RoundResult.Win + (int)RPS.Paper,
                    RPS.Scissors => (int)RPS.Scissors,
                    RPS.PlayerLose => (int)RPS.Scissors,
                    RPS.PlayerDraw => (int)RoundResult.Draw + (int)RPS.Rock,
                    RPS.PlayerWin => (int)RoundResult.Win + (int)RPS.Paper,
                    _ => throw new NotImplementedException()
                },
            RPS.Paper =>
                Player switch
                {
                    RPS.Rock => (int)RPS.Rock,
                    RPS.Paper => ((int)RoundResult.Draw + (int)RPS.Paper),
                    RPS.Scissors => ((int)RoundResult.Win + (int)RPS.Scissors),
                    RPS.PlayerLose => (int)RPS.Rock,
                    RPS.PlayerDraw => (int)RoundResult.Draw + (int)RPS.Paper,
                    RPS.PlayerWin => (int)RoundResult.Win + (int)RPS.Scissors,
                    _ => throw new NotImplementedException()
                },
            RPS.Scissors =>
                    Player switch
                    {
                        RPS.Rock => ((int)RoundResult.Win + (int)RPS.Rock),
                        RPS.Paper => (int)RPS.Paper,
                        RPS.Scissors => ((int)RoundResult.Draw + (int)RPS.Scissors),
                        RPS.PlayerLose => (int)RPS.Paper,
                        RPS.PlayerDraw => (int)RoundResult.Draw + (int)RPS.Scissors,
                        RPS.PlayerWin => (int)RoundResult.Win + (int)RPS.Rock,
                        _ => throw new NotImplementedException(),
                    },
            _ => throw new NotImplementedException()
        };
        throw new ArgumentException("Impossible outcome");
    }
}


