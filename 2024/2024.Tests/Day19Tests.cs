namespace AoC2024.Tests;
public class Day19Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.ParseInput(filename);

        //Then
        Assert.True(false);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True(false);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True(false);
    }

}