Console.WriteLine($"Running AoC 2023");
Console.WriteLine($"");
var watch = new Stopwatch();
var totalTime = new Stopwatch();
totalTime.Start();
var onlyRun = args.ToList();
var solveables = Helpers.FindSolveableMethodsInAssembly(typeof(Helpers).Assembly);
var printer = new Printer();
foreach (var solveable in solveables)
{
    watch.Restart();
    Console.WriteLine($"Running {solveable.name}");
    var result = solveable.func($"{Helpers.DirectoryPath}{solveable.filename}", printer);
    watch.Stop();
    Console.WriteLine($"Got {result.Result} in {watch.ElapsedMilliseconds} ms");
    Console.WriteLine("");
}

totalTime.Stop();
Console.WriteLine($"AoC 2023 total running time: {totalTime.Elapsed.TotalMilliseconds}ms");

