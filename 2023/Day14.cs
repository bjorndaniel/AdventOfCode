using System.Collections.Generic;

namespace AoC2023;
public class Day14
{
    public static List<Platforms> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Platform>();
        return lines.ToList();
    }

    [Solveable("2023/Puzzles/Day14.txt", "Day 14 part 1", 14)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }

    [Solveable("2023/Puzzles/Day14.txt", "Day 14 part 2", 14)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }

    public record Platform(int X, int Y,  RockType Type) { }

    public enum RockType
    {
        Round,
        Cube
    }
}