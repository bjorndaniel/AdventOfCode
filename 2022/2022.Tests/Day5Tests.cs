namespace AoC2022.Tests;
public class Day5Tests
{
    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day5-test.txt";

        //When
        var (stacks, movements) = Day5.ParseInput(filename);

        //Then
        Assert.Equal(3, stacks.Count);
        Assert.Equal(4, movements.Count());
        Assert.Contains(stacks, _ => _.StackNumber == 2 && _.StackHeight == 3);
        Assert.Contains(stacks, _ => _.StackNumber == 1 && _.Crates.Count == 2 && _.Crates.Any(_ => _.Value == 'Z'));
        Assert.Contains(stacks, _ => _.StackNumber == 2 && _.StackHeight == 3 && _.Crates.All(_ => _.StackNumber == 2));
        Assert.Contains(movements, _ => _.From == 2 && _.To == 1 && _.NrOfCrates == 1);
        Assert.Contains(movements, _ => _.From == 2 && _.To == 1 && _.NrOfCrates == 2);
        Assert.True(stacks.First(_ => _.StackNumber == 1).Crates.First(_ => _.Value == 'N').StackIndex == 0);
        Assert.True(stacks.First(_ => _.StackNumber == 1).Crates.First(_ => _.Value == 'Z').StackIndex == 1);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day5-test.txt";

        //When
        var result = Day5.SolvePart1(filename);

        //Then
        Assert.Equal("CMZ", result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day5-test.txt";

        //When
        var result = Day5.SolvePart2(filename);

        //Then
        Assert.Equal("MCD", result);
    }
}
