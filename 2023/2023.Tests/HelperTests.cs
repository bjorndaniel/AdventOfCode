namespace AoC2023.Tests;

public class HelperTests
{

    [Fact]
    public void Can_get_solvable_methods()
    {
        //Given
        var assembly = typeof(Helpers).Assembly;

        //When
        var result = Helpers.FindSolveableMethodsInAssembly(assembly);

        //Then
        Assert.NotNull(result);
        Assert.True(result.Any(_ => _.filename == "Day1.txt"), $"Could not find the solveables for day 1");
    }
}
