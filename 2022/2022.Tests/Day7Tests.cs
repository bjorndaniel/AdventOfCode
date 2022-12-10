namespace AoC2022.Tests;
public class Day7Tests
{
    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day7-test.txt";

        //When
        var result = Day7.ParseInput(filename);

        //Then
        Assert.Equal("/", result.Name);
        Assert.Null(result.Parent);
        Assert.Equal(2, result.SubDirectories.Count);
        Assert.Equal(2, result.Files.Count());
        Assert.Single(result.SubDirectories.First(_ => _.Name == "a").SubDirectories);
        Assert.Single(result.SubDirectories.First(_ => _.Name == "a").SubDirectories.First().Files);
        Assert.Equal(584, result.SubDirectories.First(_ => _.Name == "a").SubDirectories.First().TotalSize);
        Assert.Equal(94853, result.SubDirectories.First(_ => _.Name == "a").TotalSize);
        Assert.Equal(24933642, result.SubDirectories.First(_ => _.Name == "d").TotalSize);
        Assert.Equal(48381165, result.TotalSize);
        Assert.Empty(result.SubDirectories.First(_ => _.Name == "d").SubDirectories);
        Assert.Equal(4, result.SubDirectories.First(_ => _.Name == "d").Files.Count);

    }

    [Fact]
    public void Can_solve_part_1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day7-test.txt";

        //When
        var result = Day7.SolvePart1(filename);

        //Then
        Assert.Equal(95437, result);
    }

    [Fact]
    public void Can_solve_part_2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day7-test.txt";

        //When
        var result = Day7.SolvePart2(filename);

        //Then
        Assert.Equal(24933642, result);
    }
}
