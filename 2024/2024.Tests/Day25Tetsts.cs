namespace AoC2024.Tests;
public class Day25Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day25-test.txt";

        //When
        var (locks, keys) = Day25.ParseInput(filename);

        //Then
        Assert.Equal(2, locks.Count);
        Assert.Equal(3, keys.Count);
        Assert.Equal([0, 5, 3, 4, 3], locks.First());
        Assert.Equal([5, 0, 2, 1, 3], keys.First());
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day25-test.txt";

        //When
        var result = Day25.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("3", result.Result);
    }
}