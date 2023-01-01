namespace AoC2022;
public static class Helpers
{
    public static readonly string DirectoryPath = "C:/OneDrive/Code/AdventOfCodeInputs/2022/Puzzles/";
    public static readonly string DirectoryPathTests = "./Puzzles/";
    public static T[] GetColumn<T>(T[,] matrix, int columnNumber, int startRow = 0)
    {
        return Enumerable.Range(startRow, matrix.GetLength(0) - startRow)
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    public static T[] GetRow<T>(T[,] matrix, int rowNumber, int startColumn = 0)
    {
        return Enumerable.Range(startColumn, matrix.GetLength(1) - startColumn)
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }
}
