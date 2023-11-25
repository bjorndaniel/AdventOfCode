namespace AoC2018.Tests;

public class Day3Tests
{
    private readonly ITestOutputHelper _output;

    public Day3Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.ParseInput(filename);

        //Then
        Assert.True(3 == result.Count, $"Expected 3 but was {result.Count}");
        Assert.True(1 == result[0].Id, $"Expected 1 but was {result[0].Id}");
        Assert.True(2 == result[1].Id, $"Expected 2 but was {result[0].Id}");
        Assert.True(3 == result[2].Id, $"Expected 3 but was {result[0].Id}");
        Assert.True(1 == result[0].Left, $"Expected 1 but was {result[0].Left}");
        Assert.True(3 == result[0].Top, $"Expected 3 but was {result[0].Top}");
        Assert.True(4 == result[0].Width, $"Expected 4 but was {result[0].Width}");
        Assert.True(4 == result[0].Height, $"Expected 4 but was {result[0].Height}");
        Assert.True(3 == result[1].Left, $"Expected 3 but was {result[1].Left}");
        Assert.True(1 == result[1].Top, $"Expected 1 but was {result[1].Top}");
        Assert.True(4 == result[1].Width, $"Expected 4 but was {result[1].Width}");
        Assert.True(4 == result[1].Height, $"Expected 4 but was {result[1].Height}");
        Assert.True(5 == result[2].Left, $"Expected 5 but was {result[2].Left}");
        Assert.True(5 == result[2].Top, $"Expected 5 but was {result[2].Top}");
        Assert.True(2 == result[2].Width, $"Expected 2 but was {result[2].Width}");
        Assert.True(2 == result[2].Height, $"Expected 2 but was {result[2].Height}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("4" == result.Result, $"Expected result to be 4 but was {result.Result}");
    }
}
