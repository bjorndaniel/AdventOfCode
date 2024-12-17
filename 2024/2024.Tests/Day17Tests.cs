namespace AoC2024.Tests;
public class Day17Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";

        //When
        var (computer, program) = Day17.ParseInput(filename);

        //Then
        Assert.Equal(729, computer.A);
        Assert.Equal(0, computer.B);
        Assert.Equal(0, computer.C);
        Assert.Equal(6, program.Count());
        Assert.Equal(0, program.First());
        Assert.Equal(5, program[2]);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";

        //When
        var result = Day17.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("4,6,3,5,6,3,5,2,1,0", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";

        //When
        var result = Day17.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True(false);
    }

}