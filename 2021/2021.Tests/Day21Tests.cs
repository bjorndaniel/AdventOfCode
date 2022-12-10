namespace Advent2021.Tests;
public class Day21Tests
{

    [Fact]
    public void Can_play_the_game()
    {
        //When
        var (losingScore, dieRoles) = Day21.Play(4, 8);


        //Then
        Assert.Equal(993, dieRoles);
        Assert.Equal(739785, losingScore * dieRoles);
        Assert.Equal(745, losingScore);
    }

    [Fact]
    public void Can_play_the_game_part_2()
    {
        //When
        var result = Day21.Play2(4, 8);

        //Then
        Assert.Equal(444356092776315, result);
    }

    [Theory]
    [InlineData(1, 6, 4)]
    [InlineData(22, 69, 25)]
    [InlineData(97, 294, 100)]
    [InlineData(98, 297, 1)]
    [InlineData(99, 200, 2)]
    [InlineData(100, 103, 3)]
    public void Can_get_die_score(int input, int expectedScore, int expectedDie)
    {
        //When
        var (dieScore, die) = Day21.GetDieScore(input);

        //Then
        Assert.Equal(expectedScore, dieScore);
        Assert.Equal(die, expectedDie);
    }

}

