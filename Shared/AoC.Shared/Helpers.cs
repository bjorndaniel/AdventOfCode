﻿namespace AoC2023.Shared;
public static class Helpers
{
    public static readonly string DirectoryPath = "C:/OneDrive/Code/AdventOfCodeInputs/";
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

    public static List<(Func<string, IPrinter, SolutionResult> func, string filename, string name)> FindSolveableMethodsInAssembly(Assembly assembly)
    {
        var solveableMethods = new List<(Func<string, IPrinter, SolutionResult> func, string filename, string name)>();

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
                    if(method.IsStatic)
                    {
                        Func<string, IPrinter, SolutionResult> func = (Func<string, IPrinter, SolutionResult>)Delegate.CreateDelegate(typeof(Func<string, IPrinter, SolutionResult>), null, method);
                        solveableMethods.Add((func, fileName, name)); 
                    }

                }
            }
        }

        return solveableMethods;
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
            if (args.Any())
            {
                if(!args.Any(_ => solveable.filename.EndsWith(_?.ToString() ?? "")))
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
}