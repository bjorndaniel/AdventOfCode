namespace AoC2024.Tests;
public class Day13Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

        //When
        var result = Day13.ParseInput(filename);

        //Then
        Assert.Equal(4, result.Count);
        Assert.Equal(94, result.First().A.XMovement);
        Assert.Equal(34, result.First().A.YMovement);
        Assert.Equal(22, result.First().B.XMovement);
        Assert.Equal(67, result.First().B.YMovement);
        Assert.Equal(8400, result.First().Price.X);
        Assert.Equal(5400, result.First().Price.Y);
        Assert.Equal(3, result.First().A.Cost);
        Assert.Equal(1, result.First().B.Cost);


        Assert.Equal(69, result.Last().A.XMovement);
        Assert.Equal(23, result.Last().A.YMovement);
        Assert.Equal(27, result.Last().B.XMovement);
        Assert.Equal(71, result.Last().B.YMovement);
        Assert.Equal(18641, result.Last().Price.X);
        Assert.Equal(10279, result.Last().Price.Y);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

        //When
        var result = Day13.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("480", result.Result);
    }

    //[Fact]
    //public void Can_solve_part2_for_test()
    //{
    //    //Given
    //    var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

    //    //When
    //    var result = Day13.Part2(filename, new TestPrinter(output));

    //    //Then
    //    Assert.NotEqual("", result.Result);
    //}

}