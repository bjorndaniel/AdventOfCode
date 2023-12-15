namespace AoC.Shared;
public static class Helpers
{
    public static readonly string DirectoryPath = "C:/OneDrive/Code/AdventOfCodeInputs/";

    public static readonly string DirectoryPathTests = "./Puzzles/";

    public static T[] GetColumn<T>(T[,] matrix, int columnNumber, int startRow = 0) =>
        Enumerable.Range(startRow, matrix.GetLength(0) - startRow)
                .Select(x => matrix[x, columnNumber])
                .ToArray();

    public static T[] GetRow<T>(T[,] matrix, int rowNumber, int startColumn = 0) =>
        Enumerable.Range(startColumn, matrix.GetLength(1) - startColumn)
                .Select(x => matrix[rowNumber, x])
                .ToArray();

    public static List<(Func<string, IPrinter, SolutionResult> func, string filename, string name, int day, bool skip)> FindSolveableMethodsInAssembly(Assembly assembly)
    {
        var solveableMethods = new List<(Func<string, IPrinter, SolutionResult> func, string filename, string name, int day, bool skip)>();

        var types = assembly.GetTypes();
        foreach (var type in types.Where(_ => _.Namespace?.Contains("AoC") ?? false))
        {
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(Solveable), false);
                if (attributes.Length > 0)
                {
                    var solveableAttribute = (Solveable)attributes[0];
                    var fileName = solveableAttribute.Filename;
                    var name = solveableAttribute.Name;
                    if (method.IsStatic)
                    {
                        var func = (Func<string, IPrinter, SolutionResult>)Delegate.CreateDelegate(typeof(Func<string, IPrinter, SolutionResult>), null, method);
                        solveableMethods.Add((func, fileName, name, solveableAttribute.Day, solveableAttribute.Skip));
                    }

                }
            }
        }

        return solveableMethods.OrderBy(_ => _.day).ToList();
    }

    public static void Runner(string message, params object[] args)
    {
        Console.WriteLine(message);
        Console.WriteLine($"");
        var watch = new Stopwatch();
        var totalTime = new Stopwatch();
        totalTime.Start();
        var onlyRun = args.ToList();
        var solveables = FindSolveableMethodsInAssembly(Assembly.GetCallingAssembly());
        var printer = new Printer();
        foreach (var solveable in solveables)
        {
            if (solveable.skip)
            {
                continue;
            }

            if (args.Any())
            {
                if (!args.Any(_ => solveable.filename.EndsWith(_?.ToString() ?? "")))
                {
                    continue;
                }
            }

            watch.Restart();
            Console.WriteLine($"Running {solveable.name}");
            var result = solveable.func($"{DirectoryPath}{solveable.filename}", printer);
            watch.Stop();
            Console.WriteLine($"Got {result.Result} in {watch.ElapsedMilliseconds} ms");
            Console.WriteLine("");
        }

        totalTime.Stop();
        Console.WriteLine($"AoC 2023 total running time: {totalTime.Elapsed.TotalMilliseconds}ms");

    }

    public static int ManhattanDistance((int x, int y) a, (int x, int y) b) =>
       Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);

    public static long ManhattanDistance((long x, long y) a, (long x, long y) b)
    {
        checked { return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y); }
    }

    public static long CalculateLCM(List<long> numbers)
    {
        var lcm = numbers[0];
        for (var i = 1; i < numbers.Count; i++)
        {
            lcm = CalculateLCM(lcm, numbers[i]);
        }
        return lcm;
    }

    public static long CalculateLCM(long a, long b)
    {
        return (a * b) / CalculateGCD(a, b);
    }

    public static long CalculateGCD(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static char[,] CreateMatrix(List<(int x, int y)> coords)
    {
        var maxX = coords.Max(_ => _.x);
        var maxY = coords.Max(_ => _.y);
        var matrix = new char[maxX + 1, maxY + 1];
        for (int i = 0; i < maxX + 1; i++)
        {
            for (int j = 0; j < maxY + 1; j++)
            {
                matrix[i, j] = '.';
            }
        }
        foreach (var coord in coords)
        {
            matrix[coord.x, coord.y] = '#';
        }
        return matrix;
    }

    public static string GetRow(char[,] matrix, int row)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            sb.Append(matrix[i, row]);
        }
        return sb.ToString();
    }

    public static string GetColumn(char[,] matrix, int column)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            sb.Append(matrix[column, i]);
        }
        return sb.ToString();
    }
}