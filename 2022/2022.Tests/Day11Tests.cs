namespace AoC2022.Tests;
public class Day11Tests
{
    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.ParseInput(filename);

        //Then
        Assert.True(4 == result.Count(), $"Expected 4 monkeys, got {result.Count()}");
        Assert.True(2 == result.First().Value.Items.Count(), $"Expected 2 items, got {result.First().Value.Items.Count()}");
        Assert.Contains(79, result.First().Value.Items);
        Assert.Contains(98, result.First().Value.Items);
        Assert.True(WorryOp.OldMultiply == result.First().Value.Operation, $"Expected operation to be multiply, got {result.First().Value.Operation}");
        Assert.True(19 == result.First().Value.WorryOpValue, $"Expected worry op value to be 19, got {result.First().Value.WorryOpValue}");
        Assert.True(2 == result.First().Value.PassMonkey, $"Expected pass monkey to be 2, got {result.First().Value.PassMonkey}");
        Assert.True(3 == result.First().Value.FailMonkey, $"Expected fail monkey to be 3, got {result.First().Value.FailMonkey}");
        Assert.True(23 == result.First().Value.TestDivider, $"Expected test divider to be 23, got {result.First().Value.TestDivider}");

        Assert.True(1 == result.Last().Value.Items.Count(), $"Expected 1 items, got {result.Last().Value.Items.Count()}");
        Assert.Contains(74, result.Last().Value.Items);
        Assert.True(WorryOp.OldAdd == result.Last().Value.Operation, $"Expected operation to be add, got {result.Last().Value.Operation}");
        Assert.True(3 == result.Last().Value.WorryOpValue, $"Expected worry op value to be 3, got {result.Last().Value.WorryOpValue}");
        Assert.True(0 == result.Last().Value.PassMonkey, $"Expected pass monkey to be 0, got {result.Last().Value.PassMonkey}");
        Assert.True(1 == result.Last().Value.FailMonkey, $"Expected fail monkey to be 1, got {result.Last().Value.FailMonkey}");
        Assert.True(17 == result.Last().Value.TestDivider, $"Expected test divider to be 17, got {result.Last().Value.TestDivider}");
    }

    [Fact]
    public void Can_do_1_round_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var (res, monkeys) = Day11.SolvePart1(filename, 1);

        //Then
        Assert.True(monkeys[0].Items.Count() == 4, $"Expected 4 items, got {monkeys[0].Items.Count()}");
        Assert.True(monkeys[1].Items.Count() == 6, $"Expected 6 items, got {monkeys[0].Items.Count()}");
        Assert.True(monkeys[2].Items.Count() == 0, $"Expected 0 items, got {monkeys[0].Items.Count()}");
        Assert.True(monkeys[3].Items.Count() == 0, $"Expected 0 items, got {monkeys[0].Items.Count()}");
        Assert.True(2 == monkeys.First().Value.NrOfInspections, $"Expected 2 inspections, got {monkeys.First().Value.NrOfInspections}");
        Assert.True(4 == monkeys[1].NrOfInspections, $"Expected 2 inspections, got {monkeys[1].NrOfInspections}");
        Assert.True(3 == monkeys[2].NrOfInspections, $"Expected 2 inspections, got {monkeys[2].NrOfInspections}");
        Assert.True(5 == monkeys[3].NrOfInspections, $"Expected 2 inspections, got {monkeys[3].NrOfInspections}");

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var (result, _) = Day11.SolvePart1(filename, 20);

        //Then
        Assert.True(10605 == result, $"Expected 10605, got {result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var (result, monkeys) = Day11.SolvePart2(filename, 10000);

        //Then
        Assert.True(52013 == monkeys[3].NrOfInspections, $"Expected 52013, got {monkeys[3].NrOfInspections}");
        Assert.True(52166 == monkeys[0].NrOfInspections, $"Expected 52166, got {monkeys[0].NrOfInspections}");
        Assert.True(2713310158 == result, $"Expected 2713310158, got {result}");
    }
}
