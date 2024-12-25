namespace AoC2024.Tests;
public class Day24Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";

        //When
        var (wires, gates) = Day24.ParseInput(filename);

        //Then
        Assert.Equal(10, wires.Count);
        Assert.Equal(36, gates.Count);
        Assert.Equal("x00", wires.First().Name);
        Assert.Equal(1, wires.First().Value);
        Assert.Equal("y04", wires.Last().Name);
        Assert.Equal(1, wires.Last().Value);
        Assert.Equal("x04", wires[4].Name);
        Assert.Equal(0, wires[4].Value);

        Assert.Equal("ntg", gates.First().First.Name);
        Assert.Null(gates.First().First.Value);
        Assert.Equal("fgs", gates.First().Second.Name);
        Assert.Null(gates.First().Second.Value);
        Assert.Equal(Op.XOR, gates.First().Instruction);
        Assert.Equal("mjb", gates.First().Output.Name );
        Assert.Equal("tnw", gates.Last().First.Name);
        Assert.Equal("pbm", gates.Last().Second.Name);
        Assert.Equal(Op.OR, gates.Last().Instruction);
        Assert.Equal("gnj", gates.Last().Output.Name);

        Assert.Equal("y02", gates[1].First.Name);
        Assert.Equal(1, gates[1].First.Value);
        Assert.Equal("x01", gates[1].Second.Name);
        Assert.Equal(0, gates[1].Second.Value);
        Assert.Equal(Op.OR, gates[1].Instruction);
        Assert.Equal("tnw", gates[1].Output.Name);
        Assert.Equal("hwm", gates[33].First.Name);
        Assert.Equal("bqk", gates[33].Second.Name);
        Assert.Equal(Op.AND, gates[33].Instruction);
        Assert.Equal("z03", gates[33].Output.Name);

        var sorted = wires.Order().ToList();
        Assert.Equal("x00", sorted.First().Name);

        sorted = wires.OrderDescending().ToList();
        Assert.Equal("x00", sorted.Last().Name);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";

        //When
        var result = Day24.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("2024", result.Result);
    }

    [Fact]
    public void Can_solve_part1_for_test_simple()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day24-test2.txt";

        //When
        var result = Day24.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("4", result.Result);
    }

    //[Fact]
    //public void Can_solve_part2_for_test()
    //{
    //    //Given
    //    var filename = $"{Helpers.DirectoryPathTests}Day24-test3.txt";

    //    //When
    //    var result = Day24.Part2(filename, new TestPrinter(output));

    //    //Then
    //    Assert.Equal("z00,z01,z02,z05", result.Result);
    //}

}