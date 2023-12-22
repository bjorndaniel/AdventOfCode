using static AoC2023.Day22;

namespace AoC2023.Tests;
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
        Assert.True(7 == result.Count, $"Expected 7 but was {result.Count}");
        Assert.True(1 == result.First().SizeX, $"Expected 1 but was {result.First().SizeX}");
        Assert.True(3 == result.First().SizeY, $"Expected 3 but was {result.First().SizeY}");
        Assert.True(1 == result.First().SizeZ, $"Expected 1 but was {result.First().SizeZ}");
        var g = result.Single(b => b.Id == "G");
        Assert.True(1 == g.SizeX, $"Expected 1 but was {g.SizeX}");
        Assert.True(g.Z1 == 8 && g.Z2 == 9, $"Expected 8,9 but was {g.Z1},{g.Z2} for G");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day22-test.txt";

        //When
        var result = Day22.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("5" == result.Result, $"Expected 5 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day22-test.txt";

        //When
        var result = Day22.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True(false);
    }

    [Fact]
    public void Can_settle_bricks()
    {
        //Given
        var bricks = Day22.ParseInput($"{Helpers.DirectoryPathTests}Day22-test.txt");

        //When
        Day22.SettleBricks(bricks);

        //Then
        var a = bricks.Single(b => b.Id == "A");
        Assert.True(a.X1 == 1 && a.X2 == 1, $"Expected 1,1 but was {a.X1},{a.X2} for A");
        Assert.True(a.Y1 == 0 && a.Y2 == 2, $"Expected 0,2 but was {a.Y1},{a.Y2} for A");
        Assert.True(a.Z1 == 1 && a.Z2 == 1, $"Expected 1,1 but was {a.Z1},{a.Z2} for A");
        var g = bricks.Single(b => b.Id == "G");
        Assert.True(g.X1 == 1 && g.X2 == 1, $"Expected 1,1 but was {g.X1},{g.X2} for G");
        Assert.True(g.Y1 == 1 && g.Y2 == 1, $"Expected 1,1 but was {g.Y1},{g.Y2} for G");
        Assert.True(g.Z1 == 5 && g.Z2 == 6, $"Expected 5,6 but was {g.Z1},{g.Z2} for G");
    }

    [Fact]
    public void Can_correctly_identify_to_disintegrate()
    {
        //Given
        var a = new Brick("a", 1, 0, 0, 1, 2, 1);
        var b = new Brick("b", 0, 0, 2, 2, 0, 2);
        var c = new Brick("c", 0, 2, 3, 2, 2, 3);
        var d = new Brick("d", 0, 0, 4, 0, 2, 4);
        var e = new Brick("e", 2, 0, 5, 2, 2, 5);
        var f = new Brick("f", 0, 1, 6, 2, 1, 6);
        var g = new Brick("g", 1, 1, 8, 1, 1, 9);

        //When
        //Then
        Assert.True(a.IsSupporting(b));
        Assert.False(g.IsSupporting(b));
        Assert.False(g.IsSupporting(a));
        Assert.False(g.IsSupporting(b));
        Assert.False(g.IsSupporting(c));
        Assert.False(g.IsSupporting(d));
        Assert.False(g.IsSupporting(e));
        Assert.False(g.IsSupporting(f));

    }
}