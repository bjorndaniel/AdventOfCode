using System.Reflection;

namespace AoC2023;
public static class Helpers
{
    public static readonly string DirectoryPath = "C:/OneDrive/Code/AdventOfCodeInputs/2023/Puzzles/";
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
        foreach (var type in types.Where(_ => _.Namespace == "AoC2023"))
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
}