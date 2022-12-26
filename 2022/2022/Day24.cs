namespace AoC2022;
public static class Day24
{
    public static char[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                result[i, j] = lines[i][j];
            }
        }
        return result;

    }
}
