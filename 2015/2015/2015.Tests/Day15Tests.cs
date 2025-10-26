namespace AoC2015.Tests;
public class Day15Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";

        //When
        var result = Day15.ParseInput(filename);

        //Then
        Assert.True(2 == result.Count, $"Expected 2 but was {result.Count}" );
        Assert.True("Butterscotch" == result.First().Name, $"Expected Butterscotch but was {result.First().Name}");
        Assert.True(-1 == result.First().Capacity, $"Expected -1 but was {result.First().Capacity}");
        Assert.True(-2 == result.First().Durability, $"Expected -1 but was {result.First().Capacity}");
        Assert.True(6 == result.First().Flavor, $"Expected 6 but was {result.First().Flavor}");
        Assert.True(3 == result.First().Texture, $"Expected 3 but was {result.First().Texture}");
        Assert.True(8 == result.First().Calories, $"Expected 8 but was {result.First().Calories}");
        Assert.True("Cinnamon" == result.Last().Name, $"Expected Cinnamon but was {result.Last().Name}");
        Assert.True(2 == result.Last().Capacity, $"Expected 2 but was {result.Last().Capacity}");
        Assert.True(3 == result.Last().Durability, $"Expected 3 but was {result.Last().Durability}");
        Assert.True(-2 == result.Last().Flavor, $"Expected -2 but was {result.Last().Flavor}");
        Assert.True(-1 == result.Last().Texture, $"Expected -1 but was {result.Last().Texture}");
        Assert.True(3 == result.Last().Calories, $"Expected 3 but was {result.Last().Calories}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";

        //When
        var result = Day15.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("62842880" == result.Result, $"Expected 62842880 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";

        //When
        var result = Day15.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("57600000" == result.Result, $"Expected 57600000 but was {result.Result}");
    }

}