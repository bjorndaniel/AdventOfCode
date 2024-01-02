namespace AoC2015.Tests;
public class Day4Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.ParseInput(filename);

        //Then
        Assert.True("pqrstuv" == result, $"Expected pqrstuv but was {result}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("1048970" == result.Result, $"Expected 1048970 but was {result.Result}");
    }
}