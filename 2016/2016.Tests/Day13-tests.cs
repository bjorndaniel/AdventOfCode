namespace AoC2016.Tests;

public class Day13Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

        //When
        var result = Day13.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("11" == result.Result, $"Expected 11 but was {result.Result}");
    }
}