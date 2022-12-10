namespace Advent2021.Tests;
public class Day24Tests
{
    [Fact]
    public void Can_read_inputs()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day24-test.txt";

        //When
        var result = Day24.ParseInstructions(filename);

        //Then
        Assert.Equal(11, result.Count());
        Assert.Equal(Instruction.inp, result.First().instruction);
        Assert.Equal("w", result.First().parameters.First());
        Assert.Single(result.First().parameters);
        Assert.Equal(Instruction.mod, result.Last().instruction);
        Assert.Equal("w", result.Last().parameters.First());
        Assert.Equal("2", result.Last().parameters.Last());
    }


    [Fact]
    public void Can_run_program()
    {
        //Given
        var instructions = new List<(Instruction inst, List<string> p)>
        {
            (Instruction.inp, new List<string>{ "z" }),
            (Instruction.inp, new List<string>{ "x" }),
            (Instruction.mul, new List<string>{ "z", "3" }),
            (Instruction.eql, new List<string>{ "z", "x" })

        };

        //When
        var result = Day24.RunProgram(instructions, new List<long> { 1, 3 });

        //Then
        Assert.Equal(1, result.Registers["z"]);
    }

    [Fact]
    public void Can_run_program_2()
    {
        //Given
        var instructions = new List<(Instruction inst, List<string> p)>
    {
        (Instruction.inp, new List<string>{ "x" }),
        (Instruction.inp, new List<string>{ "z" }),
        (Instruction.eql, new List<string>{ "x", "z" })

    };

        //When
        var result = Day24.RunProgram(instructions, new List<long> { 26, 27 });

        //Then
        Assert.Equal(0, result.Registers["x"]);
    }

    [Fact]
    public void Can_run_program_from_file()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day24-test.txt";
        var instructions = Day24.ParseInstructions(filename);

        //When
        var result = Day24.RunProgram(instructions.ToList(), new List<long> { 10 });

        //Then
        Assert.Equal(1, result.Registers["w"]);
        Assert.Equal(0, result.Registers["x"]);
        Assert.Equal(1, result.Registers["y"]);
        Assert.Equal(0, result.Registers["z"]);
    }
}
