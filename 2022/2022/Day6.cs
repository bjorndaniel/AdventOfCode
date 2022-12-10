namespace AoC2022;
public static class Day6
{
    public static int SolvePart1(string input) =>
        Solve(input, 4);
    
    public static int SolvePart2(string input) =>
        Solve(input, 14);

    private static int Solve(string input, int length)
    {
        for (int i = 0; i < input.Length - 1; i++)
        {
            var nextFour = input[i..(i + length)];
            var g = nextFour.GroupBy(_ => _);
            if (g.All(_ => _.Count() == 1))
            {
                return i + length;
            }
        }
        return 0;
    }
}
