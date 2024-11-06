namespace AoC2015.Tests;
public class Day6Tests(ITestOutputHelper output)
{
    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.ParseInput(filename);

        //Then
        Assert.Equal(3, result.Count);
        Assert.Equal(LightMethod.TurnOn, result[0].Method);
        Assert.Equal(0, result[0].StartX);
        Assert.Equal(0, result[0].StartY);
        Assert.Equal(999, result[0].EndX);
        Assert.Equal(999, result[0].EndY);

        Assert.Equal(LightMethod.Toggle, result[1].Method);
        Assert.Equal(0, result[1].StartX);
        Assert.Equal(0, result[1].StartY);
        Assert.Equal(999, result[1].EndX);
        Assert.Equal(0, result[1].EndY);

        Assert.Equal(LightMethod.TurnOff, result[2].Method);
        Assert.Equal(499, result[2].StartX);
        Assert.Equal(499, result[2].StartY);
        Assert.Equal(500, result[2].EndX);
        Assert.Equal(500, result[2].EndY);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("998996", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day6-part2-test.txt";

        //When
        var result = Day6.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("2000001", result.Result);
    }
}
