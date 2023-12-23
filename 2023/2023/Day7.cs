namespace AoC2023;
public class Day7
{

    public static List<Hand> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Hand>();
        foreach (var line in lines)
        {
            var cards = line.Split(" ");
            var bid = int.Parse(cards[1]);
            var hand = new Hand([.. cards[0]], bid, 0, GetHandType([.. cards[0]]));
            result.Add(hand);
        }
        return result;

    }

    [Solveable("2023/Puzzles/Day7.txt", "Day 7 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var hands = ParseInput(filename).GroupBy(_ => _.Type).OrderBy(_ => _.Key);
        var currentRank = 1;
        foreach (var h in hands)
        {
            if (h.Count() == 1)
            {
                h.First().Rank = currentRank;
                currentRank++;
                continue;
            }
            var sortedHands = h.OrderBy(h => h);
            foreach (var hand in sortedHands)
            {
                hand.Rank = currentRank;
                currentRank++;
            }
        }
        var ranked = hands.SelectMany(h => h).OrderByDescending(h => h.Rank);
        return new SolutionResult(ranked.Sum(_ => _.Rank * _.Bid).ToString());
    }

    [Solveable("2023/Puzzles/Day7.txt", "Day 7 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var _order = new char[] { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };
        var parsed = ParseInput(filename);
        parsed.ForEach(_ => _.Ordering = _order);
        parsed.ForEach(_ => _.UpdateType());
        var hands = parsed.GroupBy(_ => _.Type).OrderBy(_ => _.Key);
        var currentRank = 1;
        foreach (var h in hands)
        {
            if (h.Count() == 1)
            {
                h.First().Rank = currentRank;
                currentRank++;
                continue;
            }
            var sortedHands = h.OrderBy(h => h);
            foreach (var hand in sortedHands)
            {
                hand.Rank = currentRank;
                currentRank++;
            }
        }
        var ranked = hands.SelectMany(h => h).OrderBy(h => h.Rank);
        return new SolutionResult(ranked.Sum(_ => _.Rank * _.Bid).ToString());

    }

    public static HandType GetHandType(char[] cards)
    {
        var groups = cards.GroupBy(c => c);
        var count = groups.Count();
        return count switch
        {
            5 => HandType.HighCard,
            4 => HandType.Pair,
            3 => groups.Any(g => g.Count() == 3) ? HandType.ThreeOfAKind : HandType.TwoPair,
            2 => groups.Any(g => g.Count() == 4) ? HandType.FourOfAKind : HandType.FullHouse,
            _ => HandType.FiveOfAKind,
        };
    }

    public class Hand(char[] cards, int bid, int rank, HandType type) : IComparable
    {
        private static readonly char[] _order = { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };

        public int Bid { get; set; } = bid;
        public int Rank { get; set; } = rank;
        public char[] Cards { get; set; } = cards;
        public HandType Type { get; set; } = type;
        public char[] Ordering { get; set; } = _order;

        public void UpdateType()
        {
            if (!Cards.Contains('J'))
            {
                return;
            }
            var grouped = Cards.Where(_ => _ != 'J').GroupBy(c => c);
            var highest = Type;
            foreach (var g in grouped)
            {
                var newC = Cards.ToList();
                newC.RemoveAll(c => c == 'J');
                newC.AddRange(Enumerable.Range(0, Cards.Count(c => c == 'J')).Select(_ => g.Key));
                var type = GetHandType([.. newC]);
                highest = type > highest ? type : highest;
            }
            Type = highest;
        }
        int IComparable.CompareTo(object? obj)
        {
            if (obj is Hand hand)
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    if (Array.IndexOf(Ordering, Cards[i]) < Array.IndexOf(Ordering, hand.Cards[i]))
                    {
                        return 1;
                    }
                    else if (Array.IndexOf(Ordering, Cards[i]) > Array.IndexOf(Ordering, hand.Cards[i]))
                    {
                        return -1;
                    }
                }
            }
            return -1;
        }
    }

    public enum HandType
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind,
    }

}