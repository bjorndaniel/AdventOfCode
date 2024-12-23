namespace AoC2024.Tests;
public class Day22Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day22-test.txt";

        //When
        var result = Day22.ParseInput(filename);

        //Then
        Assert.Equal(4, result.Count);
        Assert.Equal(1, result.First());
        Assert.Equal(2024, result.Last());
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day22-test.txt";

        //When
        var result = Day22.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("37327623", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day22-test2.txt";

        //When
        var result = Day22.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("23", result.Result);
    }

    [Fact]
    public void Can_generate_next()
    {
        var test = 42L;
        test = Helpers.XorWithPadding(test, 15);
        Assert.Equal(37, test);

        var test1 = 100000000L;
        test1 = test1 % 16777216;
        Assert.Equal(16113920, test1);

        var number = 123L;
        var next = Day22.GenerateNext(number);
        Assert.Equal(15887950, next);

        for (int i = 0; i < 10; i++)
        {
            number = Day22.GenerateNext(number);
        }
        Assert.Equal(5908254, number);
    }

    [Fact]
    public void UtilTests()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day22-test3.txt";

        //When
        var result = Day22.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal(true, false);
    }
}