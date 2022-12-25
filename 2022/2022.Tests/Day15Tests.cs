namespace AoC2022.Tests;
public class Day15Tests
{
    private readonly ITestOutputHelper _output;

    public Day15Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";

        //When
        var result = Day15.ParseInput(filename);

        //Then
        Assert.True(14 == result.Count(), $"Expected 14, got {result.Count()}");
        Assert.True(2 == result.First().Position.X, $"Expected 2, got {result.First().Position.X}");
        Assert.True(18 == result.First().Position.Y, $"Expected 18, got {result.First().Position.Y}");
        Assert.True(-2 == result.First().Beacon.Position.X, $"Expected -2, got {result.First().Beacon.Position.X}");
        Assert.True(15 == result.First().Beacon.Position.Y, $"Expected 15, got {result.First().Beacon.Position.Y}");

        Assert.True(20 == result.Last().Position.X, $"Expected 20, got {result.Last().Position.X}");
        Assert.True(1 == result.Last().Position.Y, $"Expected 1, got {result.Last().Position.Y}");
        Assert.True(15 == result.Last().Beacon.Position.X, $"Expected 15, got {result.Last().Beacon.Position.X}");
        Assert.True(3 == result.Last().Beacon.Position.Y, $"Expected 3, got {result.Last().Beacon.Position.Y}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";

        //When
        var result = Day15.SolvePart1(filename, 10);

        //Then
        Assert.True(26 == result, $"Expected 26, got {result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        //var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";
        var filename = $"C:/OneDrive/Code/AdventOfCodeInputs/2022/Puzzles/Day15.txt";


        //When
        //14513780000019
        //var result = Day15.SolvePart2(filename, 0, 20);
        var result = Day15.SolvePart2(filename, 0, 4000000);

        //Then
        Assert.True(56000011 == result, $"Expected 56000011, got {result}");
    }

    [Fact]
    public void Test_distance_cloud()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";
        var sensors = Day15.ParseInput(filename);

        //When
        var result = sensors.SelectMany(_ => _.GetAllPointsCovered());

        //Then
        Print(result, sensors.First().Position);

    }

    [Theory]
    [InlineData(-2, 8, 8, true, -20, 20)]
    [InlineData(16, 8, 8, true, -20, 20)]
    [InlineData(7, -1, 17, true, -20, 20)]
    [InlineData(6, 0, 16, true, -20, 20)]
    [InlineData(8, 0, 16, true, -20, 20)]
    [InlineData(5, 1, 15, true, -20, 20)]
    [InlineData(9, 1, 15, true, -20, 20)]
    [InlineData(7, 0, 10, true, 0, 10)]
    [InlineData(8, 1, 14, true, 1, 14)]
    [InlineData(8, 0, 0, true, -10, 0)]
    [InlineData(7, -1, 12, true, -10, 12)]
    [InlineData(16, 0, 0, false, -20, -10)]
    [InlineData(5, 0, 0, false, 17, 20)]
    public void Test_ranges(int row, int expectedLow, int expectedHigh, bool expected, int lower, int upper)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";
        //var sensors = Day15.ParseInput(filename);

        //When
        var (success, result) = new Sensor(new PointL(8, 7), new Beacon(new PointL(2, 10))).GetRange(row, lower, upper);

        //Then
        Assert.True(expected == success, $"Expected {expected}, got {success}");
        Assert.True(expectedHigh == result.high, $"Expected {expectedHigh} but got {result.high}");
        Assert.True(expectedLow == result.low, $"Expected {expectedLow} but got {result.low}");

    }

    private void Print(IEnumerable<PointL> points, PointL center)
    {
        for (long row = points.Min(_ => _.Y) - 1; row < points.Max(_ => _.Y) + 2; row++)
        {
            var sb = new StringBuilder();
            for (long col = points.Min(_ => _.X) - 1; col < points.Max(_ => _.X) + 2; col++)
            {
                var p = new PointL(col, row);
                if (points.Contains(p))
                {
                    if (p == center)
                    {
                        sb.Append("X");
                    }
                    else if (p.X == 0 && p.Y == 0)
                    {
                        sb.Append("O");
                    }
                    else if (p.X == 14)
                    {
                        sb.Append("1");
                    }
                    else if (p.Y == 11)
                    {
                        sb.Append("1");
                    }
                    else
                    {
                        sb.Append("#");
                    }
                }
                else
                {
                    sb.Append(".");
                }
            }
            _output.WriteLine(sb.ToString());
        }
    }

}
