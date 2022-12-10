namespace Advent2021;
public class Day17
{
    public static int GetHighestY(string input)
    {
        var grid = GetTargetArea(input);
        var probe = new Probe();
        return FindHighest(probe, grid);
    }

    public static int FindAllTrajectories(string input)
    {
        var grid = GetTargetArea(input);
        var probe = new Probe();
        return FindAll(probe, grid);
    }

    public static Grid GetTargetArea(string input)
    {
        var xvals = input[(input.IndexOf("x=") + 2)..input.IndexOf(",")];
        var yvals = input[(input.IndexOf("y=") + 2)..];
        var xmin = int.Parse(xvals.Split("..")[0]);
        var xmax = int.Parse(xvals.Split("..")[1]);
        var ymin = int.Parse(yvals.Split("..")[1]);
        var ymax = int.Parse(yvals.Split("..")[0]);
        return new Grid
        {
            MinX = xmin,
            MaxX = xmax,
            MinY = ymin,
            MaxY = ymax
        };
    }

    //Brute forcing for now...
    private static int FindAll(Probe probe, Grid grid)
    {
        var count = 0;
        for (int i = 1; i <= grid.MaxX; i++)
        {
            for (int j = grid.MaxY; j < grid.MaxX; j++)
            {
                TryTrajectory(probe, grid, i, j);
                if(probe.IsInTargetArea(grid))
                {
                    count++;
                }
            }
        }
        return count;
    }

    //Brute forcing for now...
    private static int FindHighest(Probe probe, Grid grid)
    {
        var highest = 0;
        for(int i = 1; i < grid.MinX; i++)
        {
            for(int j = grid.MinY; j < grid.MaxX; j++)
            {
                TryTrajectory(probe, grid, i, j);
                highest = probe.IsInTargetArea(grid) && probe.MaxY > highest ? probe.MaxY : highest;
            }
        }
        return highest;
    }

    private static void TryTrajectory(Probe probe, Grid grid, int startX, int startY)
    {
        probe.XPos = 0;
        probe.YPos = 0;
        probe.MaxY = 0;
        probe.VelocityX = startX;
        probe.VelocityY = startY;
        while (!probe.PassedTarget(grid) && !probe.IsInTargetArea(grid))
        {
            probe.Step();
        }
    }
}

public class Grid
{
    public int MinX { get; set; }
    public int MaxX { get; set; }
    public int MinY { get; set; }
    public int MaxY { get; set; }
}

public class Probe
{
    public int XPos { get; set; }
    public int YPos { get; set; }
    public int VelocityX { get; set; }
    public int VelocityY { get; set; }
    public int MaxY { get; set; }

    public void Step()
    {
        XPos = XPos + VelocityX;
        YPos = YPos + VelocityY;
        if (VelocityX != 0)
        {
            VelocityX += XPos > 0 ? -1 : 1;
        }
        VelocityY -= 1;
        MaxY = YPos > MaxY ? YPos : MaxY;
    }

    public bool PassedTarget(Grid grid) =>
        XPos > grid.MaxX || YPos < grid.MaxY;

    public bool IsInTargetArea(Grid targetArea) =>
        XPos >= targetArea.MinX && XPos <= targetArea.MaxX && YPos <= targetArea.MinY && YPos >= targetArea.MaxY;
}

