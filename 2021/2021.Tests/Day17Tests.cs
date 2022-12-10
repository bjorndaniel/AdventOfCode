namespace Advent2021.Tests;
public class Day17Tests
{

    [Fact]
    public void Can_get_target_area()
    {
        //Given
        var input = "target area: x=20..30, y=-10..-5";

        //When
        var grid = Day17.GetTargetArea(input);

        //Then
        Assert.Equal(20, grid.MinX);
        Assert.Equal(30, grid.MaxX);
        Assert.Equal(-5, grid.MinY);
        Assert.Equal(-10, grid.MaxY);
    }

    [Fact]
    public void Can_find_height()
    {
        //Given
        var input = "target area: x=20..30, y=-10..-5";

        //When
        var result = Day17.GetHighestY(input);

        //Then
        Assert.Equal(45, result);
    }

    [Fact]
    public void Can_find_all_trajectories()
    {
        //Given
        var input = "target area: x=20..30, y=-10..-5";

        //When
        var result = Day17.FindAllTrajectories(input);

        //Then
        Assert.Equal(112, result);

    }
}

