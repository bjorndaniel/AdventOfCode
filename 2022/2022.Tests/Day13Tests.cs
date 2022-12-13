namespace AoC2022.Tests;
public class Day13Tests
{
    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

        //When
        var pairs = Day13.ParseInput(filename);

        //Then
        Assert.True(8 == pairs.Count(), $"Expected 8, got {pairs.Count()}");
        Assert.True("1,1,3,1,1" == pairs.First().Left, $"Expected 1,1,3,1,1 got {pairs.First().Left}");
        Assert.True("1,1,5,1,1" == pairs.First().Right, $"Expected 1,1,5,1,1 got {pairs.First().Right}");
        Assert.True("1,[2,[3,[4,[5,6,7]]]],8,9" == pairs.Last().Left, $"Expected 1,[2,[3,[4,[5,6,7]]]],8,9 got {pairs.Last().Left}");
        Assert.True("1,[2,[3,[4,[5,6,0]]]],8,9" == pairs.Last().Right, $"Expected 1,[2,[3,[4,[5,6,0]]]],8,9 got {pairs.Last().Right}");

    }

    [Theory]
    [InlineData("1,1,3,1,1", "1,1,5,1,1", true)]
    public void Can_compare_pair(string left, string right, bool expected)
    {
        //Given
        var pair = new Pair(left, right);

        //When
        var result = Day13.ComparePair(pair);

        //Then
        Assert.True(expected == result, $"Expected comparison to be {expected} but was {result}");
    }


    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

        //When
        var result = Day13.SolvePart1(filename);

        //Then
        Assert.True(13 == result, $"Expected 13, got {result}");
    }
}
