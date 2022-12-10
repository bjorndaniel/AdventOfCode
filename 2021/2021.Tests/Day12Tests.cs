namespace Advent2021.Tests;
public class Day12Tests
{
    [Fact]
    public void Can_find_all_paths()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day12-test.txt";

        //When
        var result = Day12.FindAllPaths(filename);

        //Then
        Assert.Equal(19, result);

    }

    [Fact]
    public void Can_find_all_paths2()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day12-test2.txt";

        //When
        var result = Day12.FindAllPaths(filename);

        //Then
        Assert.Equal(226, result);
    }


    [Fact]
    public void Can_find_all_paths3()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day12-test.txt";

        //When
        var result = Day12.FindAllPaths(filename, true);

        //Then
        Assert.Equal(103, result);

    }

    [Fact]
    public void Can_find_all_paths4()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day12-test2.txt";

        //When
        var result = Day12.FindAllPaths(filename, true);

        //Then
        Assert.Equal(3509, result);

    }
}

