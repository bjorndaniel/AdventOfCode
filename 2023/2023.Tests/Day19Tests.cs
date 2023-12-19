namespace AoC2023.Tests;
public class Day19Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.ParseInput(filename);

        //Then
        Assert.True(5 == result.ratings.Count, $"Expected 5 but was {result.ratings.Count}");
        Assert.True(11 == result.workflows.Count, $"Expected 5 but was {result.workflows.Count}");
        Assert.True(787 == result.ratings[0].PartRatings['x'], $"Expected 787 but was {result.ratings[0].PartRatings['x']}"); ;
        Assert.True(2655 == result.ratings[0].PartRatings['m'], $"Expected 2655 but was {result.ratings[0].PartRatings['x']}");
        Assert.True(1222 == result.ratings[0].PartRatings['a'], $"Expected 1222 but was {result.ratings[0].PartRatings['a']}");
        Assert.True(2876 == result.ratings[0].PartRatings['s'], $"Expected 2876 but was {result.ratings[0].PartRatings['s']}");
        Assert.True("px" == result.workflows[0].Name, $"Expected px but was {result.workflows[0].Name}");
        Assert.True(2006 == result.workflows[0].Flows[0].Number, $"Expected 2006 but was {result.workflows[0].Flows[0].Number}");
        Assert.True("qkq" == result.workflows[0].Flows[0].Output, $"Expected qkq but was {result.workflows[0].Flows[0].Output}");
        Assert.True(Condition.LessThan == result.workflows[0].Flows[0].Condition, $"Expected LessThan but was {result.workflows[0].Flows[0].Condition}");
        Assert.True(Condition.GreaterThan == result.workflows.Last().Flows[0].Condition, $"Expected GreaterThan but was {result.workflows.Last().Flows[0].Condition}");
        Assert.True(7540 == result.ratings[0].Value(), $"Expected 7540 but was {result.ratings[0].Value()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("19114" == result.Result, $"Expected 19114 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("167409079868000" == result.Result, $"Expected 167409079868000 but was {result.Result}");
    }

}