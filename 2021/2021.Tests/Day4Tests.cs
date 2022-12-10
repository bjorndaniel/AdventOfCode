namespace Advent2021.Tests;
public class Day4Tests
{

    [Fact]
    public void Can_create_boards_and_numbers()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPath}day4-test.txt";

        //When
        var (numbers, _) = Day4.ReadAllLines(filename);

        //Then
        Assert.Equal(27, numbers.Count());
    }

    [Fact]
    public void Can_play_bingo()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPath}day4-test.txt";

        //When
        var result = Day4.PlayBingo(filename);

        //Then
        Assert.Equal(4512, result);
    }

    [Fact]
    public void Can_play_bingo_last_to_win()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPath}day4-test.txt";

        //When
        var result = Day4.LastBoardToWin(filename);

        //Then
        Assert.Equal(1924, result);
    }
}
