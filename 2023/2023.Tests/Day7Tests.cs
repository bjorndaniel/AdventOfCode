namespace AoC2023.Tests;
public class Day7Tests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.ParseInput(filename);

        //Then
        Assert.True(5 == result.Count, $"Expected 5 but was {result.Count}");
        Assert.True('3' == result[0].Cards[0], $"Expected 3 but was {result[0].Cards[0]}");
        Assert.True(HandType.Pair == result[0].Type, $"Expected Pair but was {result[0].Type}");
        Assert.True(HandType.ThreeOfAKind == result[1].Type, $"Expected ThreeOfAKind    but was {result[1].Type}");
        Assert.True(HandType.TwoPair == result[2].Type, $"Expected TwoPair but was {result[2].Type}");
        Assert.True(HandType.TwoPair == result[3].Type, $"Expected TwoPair but was {result[3].Type}");
        Assert.True(HandType.ThreeOfAKind == result[4].Type, $"Expected ThreeOfAKind but was {result[4].Type}");
    }

    [Theory]
    [InlineData("Day7-test.txt", "6440")]
    [InlineData("Day7-test2.txt", "1343")]
    public void Can_solve_part1_for_test(string input, string expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{input}";

        //When
        var result = Day7.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True(expected == result.Result, $"Expected {expected} but was {result.Result}");
    }

    [Theory]
    [InlineData("Day7-test3.txt", "5905")]
    [InlineData("Day7-test4.txt", "3667")]
    public void Can_solve_part2_for_test(string input, string expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{input}";

        //When
        var result = Day7.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True(expected == result.Result, $"Expected {expected} but was {result.Result}");
    }

    [Theory]
    [InlineData("AAAAA", HandType.FiveOfAKind)]
    [InlineData("AA8AA", HandType.FourOfAKind)]
    [InlineData("23332", HandType.FullHouse)]
    [InlineData("TTT98", HandType.ThreeOfAKind)]
    [InlineData("23432", HandType.TwoPair)]
    [InlineData("23456", HandType.HighCard)]
    public void Can_get_hand_type(string cards, HandType expected)
    {
        //Given
        var hand = cards.ToArray();

        //When
        var result = Day7.GetHandType(hand);

        //Then
        Assert.True(expected == result, $"Expected {expected} but was {result}");
    }

    [Fact]
    public void Can_compare_hands()
    {
        var hand1 = new Hand(['2', 'A', 'A', 'A', 'A'], 0, 0, HandType.FourOfAKind);
        var hand2 = new Hand(['A', 'K', 'K', 'K', 'K'], 0, 0, HandType.FourOfAKind);
        var hands = new List<Hand> { hand1, hand2 };
        var result = hands.OrderBy(_ => _).ToList();
        Assert.True(hand2 == result[1]);
        hand1 = new Hand(['5', '2', '3', '4', '6'], 0, 0, HandType.HighCard);
        hand2 = new Hand(['2', '3', '4', '5', '6'], 0, 0, HandType.HighCard);
        var hand3 = new Hand(['5', '4', '3', '2', '1'], 0, 0, HandType.HighCard);
        hands = new List<Hand> { hand1, hand2, hand3 };
        result = hands.OrderBy(_ => _).ToList();
        Assert.True(hand2 == result[0]);
        Assert.True(hand1 == result[1]);
        Assert.True(hand3 == result[2]);
        hand1 = new Hand(['2', '3', '4', '5', 'J'], 0, 0, HandType.FourOfAKind);
        hand2 = new Hand(['J', '3', '4', '5', 'A'], 0, 0, HandType.FourOfAKind);
        hands = new List<Hand> { hand1, hand2 };
        result = hands.OrderBy(_ => _).ToList();
        Assert.True(hand2 == result[1]);
    }

    [Fact]
    public void Can_update_type()
    {
        var hand1 = new Hand(['3', '2', 'T', '3', 'K'], 0, 0, HandType.Pair);
        hand1.UpdateType();
        Assert.True(HandType.Pair == hand1.Type, $"Expected Pair but was {hand1.Type}");
        hand1 = new Hand(['T', '5', '5', 'J', '5'], 0, 0, HandType.ThreeOfAKind);
        hand1.UpdateType();
        Assert.True(HandType.FourOfAKind == hand1.Type, $"Expected FourOfAKind but was {hand1.Type}");
        hand1 = new Hand(['K', 'K', '6', '7', '7'], 0, 0, HandType.TwoPair);
        hand1.UpdateType();
        Assert.True(HandType.TwoPair == hand1.Type, $"Expected TwoPair but was {hand1.Type}");
        hand1 = new Hand(['Q', 'Q', 'Q', 'J', 'A'], 0, 0, HandType.ThreeOfAKind);
        hand1.UpdateType();
        Assert.True(HandType.FourOfAKind == hand1.Type, $"Expected FourOfAKind but was {hand1.Type}");
        hand1 = new Hand(['Q', 'Q', 'Q', 'J', 'J'], 0, 0, HandType.FullHouse);
        hand1.UpdateType();
        Assert.True(HandType.FiveOfAKind == hand1.Type, $"Expected FiveOfAKind but was {hand1.Type}");
        hand1 = new Hand(['J', 'J', 'J', 'J', 'J'], 0, 0, HandType.FiveOfAKind);
        hand1.UpdateType();
        Assert.True(HandType.FiveOfAKind == hand1.Type, $"Expected FiveOfAKind but was {hand1.Type}");

    }
}