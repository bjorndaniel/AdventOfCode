namespace AoC2025.Tests;

public class Day10Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.ParseInput(filename);

        //Then
        Assert.True(3 == result.Count, $"Expected 3 but was {result.Count}");
        Assert.True(".##." == new string(result.First().Lights), $"Expected .##. but was {new string(result.First().Lights)}");
        Assert.True(result.First().Buttons.First().SequenceEqual([3]), $"Expected 3 but was {result.First().Buttons.First()}");
        Assert.True(result.First().Joltages.First().SequenceEqual([3,5,4,7]), $"Expected 3,5,4,7 but was {result.First().Joltages.First()}");
        Assert.True(".###.#" == new string(result.Last().Lights), $"Expected .###.# but was {new string(result.Last().Lights)}");
        Assert.True(result.Last().Buttons.First().SequenceEqual([0,1,2,3,4]), $"Expected 3 but was {result.Last().Buttons.First()}");
        Assert.True(result.Last().Joltages.First().SequenceEqual([10,11,11,5,10,5]), $"Expected 3,5,4,7 but was {result.Last().Joltages.First()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("7" == result.Result, $"Expected 7 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("33" == result.Result,  $"Expected 33 but was {result.Result}");
    }

}