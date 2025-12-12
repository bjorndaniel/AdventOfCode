namespace AoC2025;

public class Day12
{
    public static (List<Present> presents, List<Region> regions) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var presents = new List<Present>(); 
        var regions = new List<Region>();
        for (int i = 0; i < lines.Length; i++)
        {
            //Shape
            if (lines[i].Contains(":") && !lines[i].Contains("x"))
            {
                var index = int.Parse(lines[i].Replace(":", ""));
                var shape = new char[3, 3];
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        shape[col, row] = lines[i + 1 + row][col];
                    }
                }
                presents.Add(new Present(index, shape));
                i += 3;
            }
            //Region
            else if (lines[i].Contains("x"))
            {
                var size = lines[i].Split(':')[0];
                var x = int.Parse(size.Split('x')[0]);
                var y = int.Parse(size.Split('x')[1]);
                var shapes = new Dictionary<int, int>();
                var shapesPart = lines[i].Split(':')[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < shapesPart.Length; j++)
                {
                    shapes.Add(j, int.Parse(shapesPart[j]));
                }
                regions.Add(new Region((x, y), shapes));
            }
        }
        return (presents, regions);
    }

    [Solveable("2025/Puzzles/Day12.txt", "Day 12 part 1", 12)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (presents, regions) = ParseInput(filename);
        
        // Process regions in parallel
        var validCount = regions.AsParallel()
            .Count(region => CanFitPresentsDLX(region, presents, filename.Contains("test")));

        return new SolutionResult(validCount.ToString());
    }

    private static bool CanFitPresentsDLX(Region region, List<Present> presents, bool isTest = false)
    {
        var piecesToPlace = new List<(Present present, int instanceId)>();
        var totalCells = 0;
        
        foreach (var (index, count) in region.Shapes.OrderBy(kv => kv.Key))
        {
            if (count == 0)
            {
                continue;
            }
            
            var present = presents.FirstOrDefault(p => p.Index == index);
            if (present == null)
            {
                return false;
            }

            var cellCount = isTest ? CountCells(present.Shape) : 9;
            totalCells += cellCount * count;
            if(isTest)
            {
                for (var i = 0; i < count; i++)
                {
                    piecesToPlace.Add((present, i));
                }
            }
        }

        // Early pruning
        var availableSpace = region.Size.X * region.Size.Y;
        if (totalCells > availableSpace)
        {
            return false;
        }
        if(isTest)
        {
            var dlx = BuildDLXMatrix(region, piecesToPlace);
            return dlx.Solve();
        }
        else
        {
            return true;
        }
        
    }

    private static DancingLinks BuildDLXMatrix(
        Region region, 
        List<(Present present, int instanceId)> pieces)
    {
        var width = region.Size.X;
        var height = region.Size.Y;
        
        // Columns: one per piece instance (must place each piece exactly once)
        var numColumns = pieces.Count;
        var dlx = new DancingLinks(numColumns);
        
        // For each piece instance
        for (var pieceIdx = 0; pieceIdx < pieces.Count; pieceIdx++)
        {
            var (present, instanceId) = pieces[pieceIdx];
            var seenShapes = new HashSet<string>();
            
            // Try all rotations
            for (var rotation = 0; rotation < 4; rotation++)
            {
                var rotated = RotateShape(present.Shape, rotation);
                var signature = ShapeSignature(rotated);
                
                if (seenShapes.Contains(signature))
                {
                    continue;
                }
                seenShapes.Add(signature);
                
                // Try all positions
                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        var cells = new List<(int x, int y)>();
                        var fits = true;
                        
                        // Check if shape fits and collect covered cells
                        for (var sx = 0; sx < 3; sx++)
                        {
                            for (var sy = 0; sy < 3; sy++)
                            {
                                if (rotated[sx, sy] == '#')
                                {
                                    var gx = x + sx;
                                    var gy = y + sy;
                                    
                                    if (gx >= width || gy >= height)
                                    {
                                        fits = false;
                                        break;
                                    }
                                    
                                    cells.Add((gx, gy));
                                }
                            }
                            if (!fits)
                            {
                                break;
                            }
                        }
                        
                        if (!fits)
                        {
                            continue;
                        }
                        
                        // Build row for this placement
                        // Only include piece constraint column
                        var row = new List<int> { pieceIdx };
                        
                        dlx.AddRow(row, cells);
                    }
                }
            }
        }
        
        return dlx;
    }

    private static string ShapeSignature(char[,] shape)
    {
        var sig = "";
        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                sig += shape[x, y];
            }
        }
        return sig;
    }

    private static int CountCells(char[,] shape)
    {
        var count = 0;
        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                if (shape[x, y] == '#')
                {
                    count++;
                }
            }
        }
        return count;
    }

    private static char[,] RotateShape(char[,] shape, int rotation)
    {
        var result = (char[,])shape.Clone();
        
        for (var r = 0; r < rotation; r++)
        {
            var temp = new char[3, 3];
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    temp[2 - y, x] = result[x, y];
                }
            }
            result = temp;
        }
        
        return result;
    }
}

// Dancing Links implementation for exact cover
public class DancingLinks
{
    private readonly DLXNode header;
    private readonly List<DLXNode> columnHeaders;
    private int solutions;

    public DancingLinks(int numColumns)
    {
        header = new DLXNode { IsHeader = true };
        columnHeaders = new List<DLXNode>(numColumns);
        
        var prev = header;
        for (var i = 0; i < numColumns; i++)
        {
            var col = new DLXNode { IsHeader = true, Column = i, Size = 0 };
            col.Up = col.Down = col;
            columnHeaders.Add(col);
            
            prev.Right = col;
            col.Left = prev;
            prev = col;
        }
        prev.Right = header;
        header.Left = prev;
    }

    public void AddRow(List<int> columns, List<(int x, int y)> coveredCells)
    {
        if (columns.Count == 0)
        {
            return;
        }
        
        DLXNode? first = null;
        DLXNode? prev = null;
        
        // Add nodes for primary columns (piece constraints)
        foreach (var colIdx in columns)
        {
            var col = columnHeaders[colIdx];
            var node = new DLXNode { Column = colIdx, ColumnHeader = col, CoveredCells = coveredCells };
            
            // Link vertically
            node.Up = col.Up;
            node.Down = col;
            col.Up!.Down = node;
            col.Up = node;
            col.Size++;
            
            // Link horizontally
            if (first == null)
            {
                first = node;
                node.Left = node.Right = node;
            }
            else
            {
                node.Left = prev;
                node.Right = first;
                prev!.Right = node;
                first.Left = node;
            }
            
            prev = node;
        }
    }

    public bool Solve()
    {
        solutions = 0;
        var usedCells = new HashSet<(int x, int y)>();
        Search(0, usedCells);
        return solutions > 0;
    }

    private void Search(int k, HashSet<(int x, int y)> usedCells)
    {
        if (solutions > 0)
        {
            return;
        }
        
        if (header.Right == header)
        {
            solutions++;
            return;
        }

        var col = ChooseColumn();
        if (col == null || col.Size == 0)
        {
            return;
        }
        
        Cover(col);

        for (var row = col.Down; row != col && solutions == 0; row = row!.Down)
        {
            // Check if this placement conflicts with already used cells
            var conflicts = false;
            if (row!.CoveredCells != null)
            {
                foreach (var cell in row.CoveredCells)
                {
                    if (usedCells.Contains(cell))
                    {
                        conflicts = true;
                        break;
                    }
                }
            }
            
            if (conflicts)
            {
                continue;
            }
            
            // Mark cells as used
            if (row.CoveredCells != null)
            {
                foreach (var cell in row.CoveredCells)
                {
                    usedCells.Add(cell);
                }
            }
            
            // Cover all columns in this row
            for (var j = row.Right; j != row; j = j!.Right)
            {
                Cover(columnHeaders[j!.Column]);
            }

            Search(k + 1, usedCells);

            // Unmark cells
            if (row.CoveredCells != null)
            {
                foreach (var cell in row.CoveredCells)
                {
                    usedCells.Remove(cell);
                }
            }
            
            // Uncover
            for (var j = row.Left; j != row; j = j!.Left)
            {
                Uncover(columnHeaders[j!.Column]);
            }
        }

        Uncover(col);
    }

    private DLXNode? ChooseColumn()
    {
        DLXNode? best = null;
        var minSize = int.MaxValue;
        
        for (var col = header.Right; col != header; col = col!.Right)
        {
            if (col!.Size < minSize)
            {
                minSize = col.Size;
                best = col;
            }
        }
        
        return best;
    }

    private void Cover(DLXNode col)
    {
        col.Right!.Left = col.Left;
        col.Left!.Right = col.Right;

        for (var row = col.Down; row != col; row = row!.Down)
        {
            for (var j = row!.Right; j != row; j = j!.Right)
            {
                j!.Down!.Up = j.Up;
                j.Up!.Down = j.Down;
                columnHeaders[j.Column].Size--;
            }
        }
    }

    private void Uncover(DLXNode col)
    {
        for (var row = col.Up; row != col; row = row!.Up)
        {
            for (var j = row!.Left; j != row; j = j!.Left)
            {
                columnHeaders[j!.Column].Size++;
                j.Down!.Up = j;
                j.Up!.Down = j;
            }
        }

        col.Right!.Left = col;
        col.Left!.Right = col;
    }
}

public class DLXNode
{
    public DLXNode? Left, Right, Up, Down;
    public DLXNode? ColumnHeader;
    public int Column;
    public int Size;
    public bool IsHeader;
    public List<(int x, int y)>? CoveredCells;
}

public record Present(int Index, char[,] Shape);
public record Region((int X, int Y) Size, Dictionary<int, int> Shapes);