namespace AoC2015.Tests;
public class Day7Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.ParseInput(filename);

        //Then
        Assert.Equal(8, result.Count);
        Assert.Equal(Operation.SET, result[0].Operation);
        Assert.Equal("x", result[0].Target);
        Assert.Equal("123", result[0].Source1);
        Assert.Null(result[0].Source2);

        Assert.Equal(8, result.Count);
        Assert.Equal(Operation.AND, result[2].Operation);
        Assert.Equal("d", result[2].Target);
        Assert.Equal("x", result[2].Source1);
        Assert.Equal("y", result[2].Source2);

        Assert.Equal(Operation.NOT, result[7].Operation);
        Assert.Equal("a", result[7].Target);
        Assert.Equal("y", result[7].Source1);
        Assert.Null(result[7].Source2);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("65079", result.Result);
    }

}