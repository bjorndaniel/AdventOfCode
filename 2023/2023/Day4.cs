namespace AoC2023;
public class Day4
{
    public static List<Card> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Card>();
        foreach (var l in lines)
        {
            var sections = l.Split(':', StringSplitOptions.RemoveEmptyEntries);
            var cardNumber = int.Parse(sections.First().Split(" ").Last());
            var winning = sections.Last().Split("|", StringSplitOptions.RemoveEmptyEntries).First().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(_ => int.Parse(_)).ToList();
            var player = sections.Last().Split("|", StringSplitOptions.RemoveEmptyEntries).Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(_ => int.Parse(_)).ToList();
            result.Add(new Card(cardNumber, winning, player));
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day4.txt", "Day 4 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var cards = ParseInput(filename);
        var sum = 0;
        foreach (var card in cards)
        {
            var playerWin = card.PlayerNumbers.Where(_ => card.WinningNumbers.Contains(_));
            if (playerWin.Any())
            {
                var count = playerWin.Count();
                var cardSum = 1;
                for (int i = 1; i < count; i++)
                {
                    cardSum *= 2;
                }
                sum += cardSum;
            }
        }
        return new SolutionResult(sum.ToString());
    }

    [Solveable("2023/Puzzles/Day4.txt", "Day 4 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var cards = ParseInput(filename);
        var remaining = new Dictionary<int, int>();
        cards.ForEach(_ => remaining.Add(_.Number, 1));
        foreach (var card in cards)
        {
            var winCount = card.PlayerNumbers.Where(_ => card.WinningNumbers.Contains(_)).Count();
            for (int i = 1; i < winCount + 1; i++)
            {
                remaining[card.Number + i] += remaining[card.Number];
            }
        }
        return new SolutionResult(remaining.Sum(_ => _.Value).ToString());
    }

    public record Card(int Number, List<int> WinningNumbers, List<int> PlayerNumbers) { }
}