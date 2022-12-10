namespace AoC2022.Tests;
public class Day10Tests
{
    private readonly ITestOutputHelper _output;

    public Day10Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.ParseInput(filename);

        //Then
        Assert.Equal(146, result.Count());
        Assert.Equal(Instruction.NOOP, result.ElementAt(9).Instr);
        Assert.Equal(Instruction.ADDX, result.ElementAt(22).Instr);
        Assert.Equal(-19, result.ElementAt(22).Value);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.SolvePart1(filename, 220);

        //Then
        Assert.Equal(13140, result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.SolvePart2(filename, int.MaxValue);

        //Then
        var chars = Print(result);
        Assert.Equal(124, chars);

        int Print(char?[,] chars)
        {
            var litCount = 0;
            for (int row = 0; row < chars.GetLength(0); row++)
            {
                var sb = new StringBuilder();
                for (int col = 0; col < chars.GetLength(1); col++)
                {
                    if (chars[row, col] == '#')
                    {
                        litCount++;
                    }
                    sb.Append(chars[row, col] ?? '.');
                }
                _output.WriteLine(sb.ToString());
            }
            _output.WriteLine("");
            return litCount;
        }
    }
}
