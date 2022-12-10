namespace AoC2022.Tests;
public class Day3Tests
{
    [Fact]
    public void Can_get_char_number()
    {
        Assert.Equal(1, Day3.GetValue('a'));
        Assert.Equal(8, Day3.GetValue('h'));
        Assert.Equal(16, Day3.GetValue('p'));
        Assert.Equal(27, Day3.GetValue('A'));
        Assert.Equal(34, Day3.GetValue('H'));
        Assert.Equal(26, Day3.GetValue('z'));
        Assert.Equal(52, Day3.GetValue('Z'));
        Assert.Equal(38, Day3.GetValue('L'));
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day3-test.txt";

        //When
        var result = Day3.ParseInput(filename).ToList();

        //Then
        Assert.Equal("vJrwpWtwJgWr", result.First().Compartments().comp1);
        Assert.Equal("hcsFMMfFFhFp", result.First().Compartments().comp2);
        Assert.Equal("jqHRNqRjqzjGDLGL", result[1].Compartments().comp1);
        Assert.Equal("rsFMfFZSrLrFZsSL", result[1].Compartments().comp2);
        Assert.Equal("PmmdzqPrV", result[2].Compartments().comp1);
        Assert.Equal("vPwwTWBwg", result[2].Compartments().comp2);

    }

    [Fact]
    public void Can_get_badge_from_chunc()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day3-test.txt";
        var backpacks = Day3.ParseInput(filename).ToList();
        var chunks = backpacks.Chunk(3);

        //When
        var result = Day3.GetBadge(chunks.First());

        //Then
        Assert.Equal('r', result);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day3-test.txt";

        //When 
        var result = Day3.SolvePart1(filename);

        //Then
        Assert.Equal(157, result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day3-test.txt";

        //When 
        var result = Day3.SolvePart2(filename);

        //Then
        Assert.Equal(70, result);
    }
}
