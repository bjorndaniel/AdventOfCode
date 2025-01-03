namespace AoC2024.Tests;
public class Day21Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day21-test.txt";

        //When
        var result = Day21.ParseInput(filename);

        //Then
        Assert.Equal(5, result.Count); ;
        Assert.Equal("029A", result.First().code);
        Assert.Equal(29, result.First().number);
        Assert.Equal("379A", result.Last().code);
        Assert.Equal(379, result.Last().number);
    }

    [Fact]
    public void UtilTests()
    {
        //Given
        var keypad = Day21.KeyPad();
        var dirPad = Day21.DirectionPad();

        //When
        var result = Day21.GetPadKeys("029A");

        //Then
        Assert.Equal(4, result.Count);
        Assert.Equal(Day21.PadKey.ZERO, result[0]);
        Assert.Equal(Day21.PadKey.TWO, result[1]);
        Assert.Equal(Day21.PadKey.NINE, result[2]);
        Assert.Equal(Day21.PadKey.A, result[3]);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day21-test.txt";

        //When
        var result = Day21.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("126384", result.Result);
    }
}