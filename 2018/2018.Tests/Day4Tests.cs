namespace AoC2018.Tests;

public  class Day4Tests
{
    private readonly ITestOutputHelper _output;

    public Day4Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.ParseInput(filename);

        //Then
        Assert.True(2 == result.Count, $"Expected result to be 2 but was {result.Count}");
        var guard10 = result.First(_ => _.Id == 10);
        var guard99 = result.First(_ => _.Id == 99);
        Assert.True(3 == guard10.Shifts.Count, $"Expected result to be 3 but was {guard10.Shifts.Count}");
        Assert.True(50 == guard10.Shifts.Sum(_ => _.MinutesAsleep.Count), $"Expected result to be 50 but was {guard10.Shifts.Sum(_ => _.MinutesAsleep.Count)}");
        Assert.True(30 == guard99.Shifts.Sum(_ => _.MinutesAsleep.Count), $"Expected result to be 30 but was {guard99.Shifts.Sum(_ => _.MinutesAsleep.Count)}");
        Assert.True(24 == guard10.GetMostAsleepMinute().minute, $"Expected result to be 24 but was {guard10.GetMostAsleepMinute()}");
        Assert.True(2 == guard10.GetMostAsleepMinute().count, $"Expected result to be 2 but was {guard10.GetMostAsleepMinute().count}");
        Assert.True(50 == result.Max(_ =>_.GetAsleepCount()), $"Expected result to be 50 but was {result.Max(_ => _.GetAsleepCount())}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("240" == result.Result, $"Expected result to be 240 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("4455" == result.Result, $"Expected result to be 4455 but was {result.Result}");

    }
}
