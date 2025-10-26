namespace AoC2015.Tests;
public class Day14Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day14-test.txt";

        //When
        var result = Day14.ParseInput(filename);

        //Then
        Assert.True(2 == result.Count, $"Expected 2 but was {result.Count}");
        Assert.Contains(result, r => r.Name == "Comet");
        Assert.Contains(result, r => r.Name == "Dancer");
        Assert.True(result.First(r => r.Name == "Comet").Speed == 14, "Comet should have speed 14");
        Assert.True(result.First(r => r.Name == "Comet").FlyTime == 10, "Comet should have fly time 10");
        Assert.True(result.First(r => r.Name == "Comet").RestTime == 127, "Comet should have rest time 127");
        Assert.True(result.First(r => r.Name == "Dancer").Speed == 16, "Dancer should have speed 16");
        Assert.True(result.First(r => r.Name == "Dancer").FlyTime == 11, "Dancer should have fly time 11");
        Assert.True(result.First(r => r.Name == "Dancer").RestTime == 162, "Dancer should have rest time 162");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day14-test.txt";

        //When
        var result = Day14.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("1120" == result.Result, $"Expected 1120 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day14-test.txt";

        //When
        var result = Day14.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("689" == result.Result, $"Expected 689 but was {result.Result}");
    }

}