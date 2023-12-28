namespace AoC2022.Tests;
public class Day1Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.ParseInput(filename);

        //Then
        Assert.Equal(5, result.Count());

    }

    [Fact]
    public void Can_solve_test_elves_part1()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("24000", result.Result);
    }

    [Fact]
    public void Can_solve_test_elves_part2()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("45000", result.Result);
    }
}
