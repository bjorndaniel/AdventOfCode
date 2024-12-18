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

    //[Fact]
    //public void Can_solve_part2_for_test()
    //{
    //    //Given
    //    var filename = $"{Helpers.DirectoryPathTests}Day17-test2.txt";

    //    //When
    //    var result = Day17.Part2(filename, new TestPrinter(output));

    //    //Then
    //    Assert.Equal("117440", result.Result);
    //}

    [Fact]
    public void Can_run_programs()
    {
        var c = new Computer(0, 0, 9);
        var program = new List<int> { 2, 6 };
        var result = Day17.RunProgram(c, program);
        Assert.Equal(1, c.B);

        c = new Computer(10, 0, 0);
        program = new List<int> { 5, 0, 5, 1, 5, 4 };
        result = Day17.RunProgram(c, program);
        Assert.Equal([0, 1, 2], result);

        c = new Computer(2024, 0, 0);
        program = new List<int> { 0, 1, 5, 4, 3, 0 };
        result = Day17.RunProgram(c, program);
        Assert.Equal([4, 2, 5, 6, 7, 7, 7, 7, 3, 1, 0], result);
        Assert.Equal(0, c.A);

        c = new Computer(0, 29, 0);
        program = new List<int> { 1, 7 };
        result = Day17.RunProgram(c, program);
        Assert.Equal(26, c.B);

        var x = Day17.XorWithPadding(2024, 43690);
        Assert.Equal(44354, x);
        c = new Computer(0, 2024, 43690);
        program = new List<int> { 4, 0 };
        result = Day17.RunProgram(c, program);
        Assert.Equal(44354, c.B);

        c = new Computer(41644071, 0, 0);
        program = new List<int> { 2, 4, 1, 2, 7, 5, 1, 7, 4, 4, 0, 3, 5, 5, 3, 0 };
        result = Day17.RunProgram(c, program);
    }
}