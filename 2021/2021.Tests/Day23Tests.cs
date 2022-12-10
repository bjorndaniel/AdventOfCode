namespace Advent2021.Tests;

public class Day23Tests
{
    private readonly ITestOutputHelper _output;

    public Day23Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_run_testprogram()
    {

        var startWorld = new AmphipodWorld(new[]
         {
            "#############",
            "#...........#",
            "###B#C#B#D###",
            "  #A#D#C#A#",
            "  #########",
        });

        var endWorld = new AmphipodWorld(new[]
          {
            "#############",
            "#...........#",
            "###A#B#C#D###",
            "  #A#B#C#D#",
            "  #########"
        });

        var result = Day23.Solve(startWorld, endWorld);
        Assert.Equal(12521, result);
    }

}
