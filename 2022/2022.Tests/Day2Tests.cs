namespace AoC2022.Tests;
public class Day2Tests
{

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day2-test.txt";

        //When
        var result = Day2.ParseInput(filename).ToList();

        //Then
        Assert.Equal(3, result.Count);
        Assert.Equal(RPS.Rock, result[0].Opponent);
        Assert.Equal(RPS.Paper, result[0].Player);
        Assert.Equal(RPS.Paper, result[1].Opponent);
        Assert.Equal(RPS.Rock, result[1].Player);
        Assert.Equal(RPS.Scissors, result[2].Opponent);
        Assert.Equal(RPS.Scissors, result[2].Player);
    }

    [Fact]
    public void Can_play_test_round_part1()
    {
        {
            //Given
            var filename = $"{Helpers.DirectoryPath}Day2-test.txt";

            //When
            var result = Day2.SolvePart1(filename);

            //Then
            Assert.Equal(15, result);
        }
    }

    [Fact]
    public void Can_play_test_round_par2()
    {
        {
            //Given
            var filename = $"{Helpers.DirectoryPath}Day2-test.txt";

            //When
            var result = Day2.SolvePart2(filename);

            //Then
            Assert.Equal(12, result);
        }
    }
}
