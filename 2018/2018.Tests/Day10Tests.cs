namespace AoC2018.Tests;
public class Day10Tests
{
    private readonly ITestOutputHelper _output;

    public Day10Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.ParseInput(filename);

        //Then
        Assert.True(31 == result.Points.Count, $"Expected 10 but was {result.Points.Count}");
    }

    [Fact]
    public void Can_print_Sky()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";
        var sky = Day10.ParseInput(filename);

        //When
        sky.Print(new TestPrinter(_output));

        //Then
        Assert.True(31 == sky.Points.Count, $"Expected 10 but was {sky.Points.Count}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test2.txt";

        //When
        var result = Day10.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True(false);
    }

}