namespace AoC2025.Tests;

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
        Assert.True(4 == result.numbers.Length, $"Expected 3 but was {result.numbers.Length}");
        Assert.True(4 == result.operands.Length, $"Expected 3 but was {result.operands.Length}");
        Assert.True("123" == new string(result.numbers[0].First()), $"Expected 123 but was {new string(result.numbers[0].First())}");
        Assert.True("64 " == new string(result.numbers[3].First()), $"Expected 64 but was {new string(result.numbers[3].First())}");
        Assert.True("*" == result.operands.First(), $"Expected * but was {result.operands.First()}");
        Assert.True("+" == result.operands.Last(), $"Expected * but was {result.operands.Last()}");

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("4277556" == result.Result, $"Expected 4277556 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("3263827" == result.Result, $"Expected 3263827 but was {result.Result}");
    }

}