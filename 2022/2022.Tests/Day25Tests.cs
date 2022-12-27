namespace AoC2022.Tests;
public class Day25Tests
{
    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day25-test.txt";

        //When
        var result = Day25.ParseInput(filename);

        //Then
        Assert.True(13 == result.Count(), $"Expected 13, got {result.Count()}");
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("2", 2)]
    [InlineData("1=", 3)]
    [InlineData("1-", 4)]
    [InlineData("10", 5)]
    [InlineData("11", 6)]
    [InlineData("12", 7)]
    [InlineData("2=", 8)]
    [InlineData("2-", 9)]
    [InlineData("20", 10)]
    [InlineData("1=0", 15)]
    [InlineData("1-0", 20)]
    [InlineData("1=11-2", 2022)]
    [InlineData("1-0---0", 12345)]
    [InlineData("1121-1110-1=0", 314159265)]
    public void Can_parse_SNAFU(string snafuValue, int expected)
    {
        //Given
        var snafu = new SNAFU(snafuValue);

        //When
        var result = snafu.ToDecimal();

        //Then
        Assert.True(expected == result, $"Expected {expected}, got {result}");
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    [InlineData(3, "1=")]
    [InlineData(4, "1-")]
    [InlineData(5, "10")]
    [InlineData(6, "11")]
    [InlineData(7, "12")]
    [InlineData(8, "2=")]
    [InlineData(1747, "1=-0-2")]
    [InlineData(906, "12111")]
    [InlineData(1257, "20012")]
    [InlineData(314159265, "1121-1110-1=0")]
    public void Can_parse_decimal_to_SNAFU(int decimalValue, string expected)
    {
        //Given

        //When
        var result = SNAFU.FromDecimal(decimalValue);

        //Then
        Assert.True(expected == result.Value, $"Expected {expected}, got {result.Value}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day25-test.txt";

        //When
        var result = Day25.SolvePart1(filename);

        //Then
        Assert.True("2=-1=0" == result, $"Expected 2=-1=0, got {result}");
    }
}
