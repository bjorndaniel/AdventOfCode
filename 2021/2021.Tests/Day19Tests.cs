namespace Advent2021.Tests;
public class Day19Tests
{
    [Fact]
    public void Can_read_scanners()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day19-test.txt";

        //When
        var result = Day19.ReadScanners(filename);

        //Then
        var origin = result.First(_ => _.Name == "scanner 0");
        Assert.Equal(5, result.Count);
        Assert.Equal(26, result.First(_ => _.Name == "scanner 4").Beacons.Count);
    }

    [Fact]
    public void Can_get_overlapping_beacons()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day19-test.txt";
        var scanners = Day19.ReadScanners(filename);
        var origin = scanners.First(_ => _.Name == "scanner 0");
        var scanner1 = scanners.First(_ => _.Name == "scanner 1");

        //When
        var result = Day19.GetOverlappingBeacons(origin, scanner1);

        //Then
        Assert.Equal(12, result.Count());
    }

    [Fact]
    public void Can_rotate_beacons()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day19-test.txt";
        var scanners = Day19.ReadScanners(filename);
        var scanner0 = scanners.First(_ => _.Name == "scanner 0");
        var scanner1 = scanners.First(_ => _.Name == "scanner 1");
        var scanner2 = scanners.First(_ => _.Name == "scanner 2");
        var scanner3 = scanners.First(_ => _.Name == "scanner 3");
        var scanner4 = scanners.First(_ => _.Name == "scanner 4");
        scanner0.X = 0;
        scanner0.Y = 0;
        scanner0.Z = 0;
        //var expectedCount = scanner0.Beacons.Count() + scanner1.Beacons.Count() - 12;

        //When
        var x = Day19.MoveBeacons(scanner0, scanner1, Day19.GetOverlappingBeacons(scanner0, scanner1));
        scanner0.Beacons.AddRange(x);
        var y = Day19.MoveBeacons(scanner0, scanner4, Day19.GetOverlappingBeacons(scanner0, scanner4));
        scanner0.Beacons.AddRange(y);
        var z = Day19.MoveBeacons(scanner0, scanner2, Day19.GetOverlappingBeacons(scanner0, scanner2));
        scanner0.Beacons.AddRange(z);
        var w = Day19.MoveBeacons(scanner0, scanner3, Day19.GetOverlappingBeacons(scanner0, scanner3));
        scanner0.Beacons.AddRange(w);

        Assert.Equal(68, scanner1.X);
        Assert.Equal(-1246, scanner1.Y);
        Assert.Equal(-43, scanner1.Z);
        Assert.Equal(-20, scanner4.X);
        Assert.Equal(-1133, scanner4.Y);
        Assert.Equal(1061, scanner4.Z);
        Assert.Equal(1105, scanner2.X);
        Assert.Equal(-1205, scanner2.Y);
        Assert.Equal(1229, scanner2.Z);
        Assert.Equal(-92, scanner3.X);
        Assert.Equal(-2380, scanner3.Y);
        Assert.Equal(-20, scanner3.Z);
    }

    [Fact]
    public void Can_count_beacons()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day19-test.txt";

        //When
        var result = Day19.CountBeacons(filename);

        //Then
        Assert.Equal(79, result.origin.Beacons.Count());
    }

    [Fact]
    public void Can_get_manhattan_distance()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day19-test.txt";

        //When
        var result = Day19.CountBeacons(filename);

        //Then
        Assert.Equal(3621, result.manhattan);
    }
}

