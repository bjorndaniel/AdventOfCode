namespace AoC2024;
public class Day8
{
    public static char[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new char[lines.Length, lines.First().Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines.First().Length; j++)
            {
                result[i, j] = lines[i][j];
            }
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day8.txt", "Day 8 part 1", 8)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var grid = ParseInput(filename);
        var antinodes = GetAntinodes(grid);
        return new SolutionResult(antinodes.Count.ToString());
    }

    [Solveable("2024/Puzzles/Day8.txt", "Day 8 part 2", 8)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var grid = ParseInput(filename);
        var antinodes = GetAntinodes(grid, true);
        return new SolutionResult(antinodes.Count.ToString());
    }

    private static HashSet<(int x, int y)> GetAntinodes(char[,] grid, bool part2 = false)
    {
        var antennas = new List<(int x, int y, char freq)>();
        var antinodes = new HashSet<(int x, int y)>();

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (grid[row, col] != '.')
                {
                    antennas.Add((row, col, grid[row, col]));
                }
            }
        }

        for (int row = 0; row < antennas.Count; row++)
        {
            for (int col = row + 1; col < antennas.Count; col++)
            {
                var (y1, x1, freq1) = antennas[row];
                var (y2, x2, freq2) = antennas[col];
                if (freq1 == freq2)
                {
                    var dx = x2 - x1;
                    var dy = y2 - y1;
                    if (part2)
                    {
                        for (int dir = 0; ; dir++)
                        {
                            var antinode1 = (x2 + dir * dx, y2 + dir * dy);
                            var antinode2 = (x1 - dir * dx, y1 - dir * dy);
                            if (Helpers.IsInsideGrid(grid, antinode1) is false && Helpers.IsInsideGrid(grid, antinode2) is false)
                            {
                                break;
                            }

                            if (Helpers.IsInsideGrid(grid, antinode1))
                            {
                                antinodes.Add(antinode1);
                            }

                            if (Helpers.IsInsideGrid(grid, antinode2))
                            {
                                antinodes.Add(antinode2);
                            }
                        }
                    }
                    else
                    {
                        var antinode1 = (x2 + dx, y2 + dy);
                        var antinode2 = (x1 - dx, y1 - dy);
                        if (Helpers.IsInsideGrid(grid, antinode1))
                        {
                            antinodes.Add(antinode1);
                        }
                        if (Helpers.IsInsideGrid(grid, antinode2))
                        {
                            antinodes.Add(antinode2);
                        }
                    }
                }
            }
        }
        return antinodes;
    }
}