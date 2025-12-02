namespace AoC2016.Tests;

public class Day4Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.ParseInput(filename);

        //Then
        Assert.True(4 == result.Count, $"Expected 4 but was {result.Count}");
        Assert.True("aaaaa-bbb-z-y-x" == result.First().EncryptedName, $"Expected aaaaa-bbb-z-y-x but was {result.First().EncryptedName}");
        Assert.True(123 == result.First().SectorId, $"Expected 123 but was {result.First().SectorId}");
        Assert.True("abxyz" == result.First().Checksum, $"Expected abxyz but was {result.First().SectorId}");

        Assert.True("totally-real-room" == result.Last().EncryptedName, $"Expected totally-real-room but was {result.Last().EncryptedName}");
        Assert.True(200 == result.Last().SectorId, $"Expected 200 but was {result.Last().SectorId}");
        Assert.True("decoy" == result.Last().Checksum, $"Expected decoy but was {result.Last().SectorId}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("1514" == result.Result, $"Expected 1514 but was {result.Result}");
    }

}