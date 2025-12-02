namespace AoC2016;

public class Day2
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.ToList();
    }

    [Solveable("2016/Puzzles/Day2.txt", "Day 2 part 1", 2)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var moves = ParseInput(filename);
        var keypad = new char[,]
        {
            { '1','2','3' },
            { '4','5','6' },
            { '7','8','9' }
        };

        var x = 1;
        var y = 1;
        var code = new List<char>();
        foreach (var line in moves)
        {
            foreach (var move in line.Trim())
            {
                var nx = x;
                var ny = y;
                switch (move)
                {
                    case 'U': ny = Math.Max(0, y - 1); break;
                    case 'D': ny = Math.Min(2, y + 1); break;
                    case 'L': nx = Math.Max(0, x - 1); break;
                    case 'R': nx = Math.Min(2, x + 1); break;
                }
                x = nx;
                y = ny;
            }

            code.Add(keypad[y, x]);
        }

        return new SolutionResult(new string(code.ToArray()));
    }

    [Solveable("2016/Puzzles/Day2.txt", "Day 2 part 2", 2)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var lines = File.ReadAllLines(filename);
        // Diamond keypad for part 2 (5x5 grid, spaces represent invalid positions)
        var keypad = new char[,]
        {
            { ' ', ' ', '1', ' ', ' ' },
            { ' ', '2', '3', '4', ' ' },
            { '5', '6', '7', '8', '9' },
            { ' ', 'A', 'B', 'C', ' ' },
            { ' ', ' ', 'D', ' ', ' ' }
        };
        var x = 0;
        var y = 2;
        var code = new List<char>();

        foreach (var line in lines)
        {
            foreach (var move in line.Trim())
            {
                var nx = x;
                var ny = y;
                switch (move)
                {
                    case 'U':
                        ny = Math.Max(0, y - 1); 
                        break;
                    case 'D':
                        ny = Math.Min(4, y + 1); 
                        break;
                    case 'L':
                        nx = Math.Max(0, x - 1);
                        break;
                    case 'R':
                        nx = Math.Min(4, x + 1); 
                        break;
                }
                if (keypad[ny, nx] != ' ')
                {
                    x = nx;
                    y = ny;
                }
            }
            code.Add(keypad[y, x]);
        }

        return new SolutionResult(new string(code.ToArray()));
    }


}