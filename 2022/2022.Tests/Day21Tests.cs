namespace AoC2022.Tests;
public class Day21Tests
{
    private readonly ITestOutputHelper _output;

    public Day21Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day21-test.txt";

        //When
        var result = Day21.ParseInput(filename);

        //Then
        Assert.True(15 == result.Count, $"Expected 15 , got {result.Count}");
        Assert.True("root" == result["root"].Name, $"Expected root, got {result["root"].Name}");
        Assert.True("pppw" == result["root"].Left, $"Expected pppw, got {result["root"].Left}");
        Assert.True("sjmn" == result["root"].Right, $"Expected sjmn, got {result["root"].Right}");
        Assert.True(Operator.Add == result["root"].Operator, $"Expected Add, got {result["root"].Operator}");
        Assert.True("dbpl" == result["dbpl"].Name, $"Expected dbpl, got {result["dbpl"].Name}");
        Assert.True("" == result["dbpl"].Left, $"Expected pppw, got {result["dbpl"].Left}");
        Assert.True("" == result["dbpl"].Right, $"Expected sjmn, got {result["dbpl"].Right}");
        Assert.True(5 == result["dbpl"].Value, $"Expected 5, got {result["dbpl"].Value}");
        Assert.True(Operator.None == result["dbpl"].Operator, $"Expected Add, got {result["dbpl"].Operator}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day21-test.txt";

        //When
        var result = Day21.SolvePart1(filename);

        //Then
        Assert.True(152 == result, $"Expected 152, got {result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day21-test.txt";


        //When
        var result = Day21.SolvePart2(filename, new TestPrinter(_output));

        //Then
        Assert.True(301 == result, $"Expected 301, got {result}");
    }
}
