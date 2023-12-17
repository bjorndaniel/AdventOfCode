namespace AoC2023.Tests;
public class Day5Tests(ITestOutputHelper output)
{
    

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.ParseInput(filename);

        //Then
        Assert.True(4 == result.Seeds.Count, $"Expected 4 but was {result.Seeds.Count}");
        Assert.True(result.Seeds.Contains(14), $"Expected to find 14");
        Assert.True(7 == result.Maps.Count, $"Exptected 7 but was {result.Maps.Count}");
        var seedToSoil = result.Maps.First(_ => _.Type == Day5.MapType.SeedSoil);
        Assert.True(seedToSoil.GetDestination(50) == 52, $"Expected 52 but was {seedToSoil.GetDestination(50)}");
        Assert.True(seedToSoil.GetDestination(0) == 0, $"Expected 0 but was {seedToSoil.GetDestination(0)}");
        Assert.True(seedToSoil.GetDestination(99) == 51, $"Expected 51 but was {seedToSoil.GetDestination(0)}");
        Assert.True(seedToSoil.GetDestination(48) == 48, $"Expected 48 but was {seedToSoil.GetDestination(48)}");
        Assert.True(seedToSoil.GetDestination(96) == 98, $"Expected 52 but was {seedToSoil.GetDestination(96)}");
        Assert.True(seedToSoil.GetDestination(79) == 81, $"Expected 52 but was {seedToSoil.GetDestination(79)}");
        Assert.True(result.SeedRanges.Contains((79,92)), $"Exptected range (79,92)");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("35" == result.Result, $"Expected 35 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("46" == result.Result, $"Expected 46 but was {result.Result}");
    }

}