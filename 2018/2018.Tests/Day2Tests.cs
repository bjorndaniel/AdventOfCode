namespace AoC2018.Tests;

public class Day2Tests
{
        private readonly ITestOutputHelper _output;

        public Day2Tests(ITestOutputHelper output)
        {
            _output = output;
        }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.ParseInput(filename);

        //Then
        Assert.True(7 == result.Count, $"Expected 7 but was {result.Count}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("12" == result.Result, $"Expected result to be 12 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test2.txt";

        //When
        var result = Day2.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("fgij" == result.Result, $"Expected result to be fgij but was {result.Result}");
    }
}