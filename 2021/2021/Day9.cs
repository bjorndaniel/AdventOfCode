using System.Collections;

namespace Advent2021;
public class Day9
{
    private static readonly int[] _rowDirections = { 0, 1, 0, -1 };
    private static readonly int[] _columnDirections = { -1, 0, 1, 0 };
    private static int _rows;
    private static int _cols;

    public static int CalculateLowpoint(string filename)
    {
        var heightMap = GetHeightMap(filename);
        var basins = GetBasins(heightMap);
        return basins.Sum(_ => _.Value + 1);
    }

    private static HeightMap GetHeightMap(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var heightMap = new HeightMap
        {
            Rows = lines.Length - 1
        };
        for (int y = 0; y < lines.Length; y++)
        {
            heightMap.Columns = lines[y].Length - 1 > heightMap.Columns ? lines[y].Length - 1 : heightMap.Columns;
            for (int x = 0; x < lines[y].Length; x++)
            {
                heightMap.Locations.Add((x, y), int.Parse((lines[y][x]).ToString()));
            }
        }
        return heightMap;
    }

    private static int[,] GetMatrix(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new int[lines.Length, lines.Max(_ => _.Length)];
        _rows = lines.Length;
        _cols = lines.Max(_ => _.Length);
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                result[y, x] = int.Parse((lines[y][x]).ToString());
            }
        }
        return result;
    }

    private static List<Basin> GetBasins(HeightMap map)
    {
        var result = new List<Basin>();
        foreach (var height in map.Locations)
        {
            var isLowest = map.IsLowest(height.Key, height.Value);
            if (isLowest)
            {
                result.Add(new Basin
                {
                    X = height.Key.x,
                    Y = height.Key.y,
                    Value = height.Value,
                });

            }
        }
        return result;
    }

    public static int CalculateLargestBasins(string filename)
    {
        var heightMap = GetHeightMap(filename);
        var basins = GetBasins(heightMap).OrderByDescending(_ => _.Size);
        var matrix = GetMatrix(filename);
        foreach (var basin in basins)
        {
            basin.Size = CalculateSize(matrix, basin.X, basin.Y);
        }
        var largest = basins.OrderByDescending(_ => _.Size).Take(3);
        return largest.Select(_ => _.Size).Aggregate((x, y) => x * y);
    }

    private static int CalculateSize(int[,] matrix, int x, int y)
    {
        bool[,] visited = new bool[_rows, _cols];
        var result = DFS(y, x, matrix, visited);
        return result;
    }

    //From: https://www.geeksforgeeks.org/depth-first-traversal-dfs-on-a-2d-array/
    private static int DFS(int row, int col, int[,] grid, bool[,] visited)
    {
        var count = 0;

        // Initialize a stack of pairs and
        // push the starting cell into it
        var st = new Stack();
        st.Push(new Tuple<int, int>(row, col));

        // Iterate until the
        // stack is not empty
        while (st.Count > 0)
        {

            // Pop the top pair
            var curr = (Tuple<int, int>)st.Peek()!;
            st.Pop();

            row = curr.Item1;
            col = curr.Item2;
            var val = 9;
            if (!(row < 0 || col < 0 || row >= _rows || col >= _cols))
            {
                val = grid[row, col];
            }

            // Check if the current popped
            // cell is a valid cell or not
            if (!IsValid(visited, grid, row, col, val))
            {
                continue;
            }

            // Mark the current
            // cell as visited
            visited[row, col] = true;

            //Add the number of cells to total count
            count++;

            // Push all the adjacent cells
            for (int i = 0; i < 4; i++)
            {
                var adjx = row + _rowDirections[i];
                var adjy = col + _columnDirections[i];
                st.Push(new Tuple<int, int>(adjx, adjy));
            }
        }
        return count;
    }

    static bool IsValid(bool[,] vis, int[,] grid, int row, int col, int value)
    {
        // If cell is out of bounds
        if (row < 0 || col < 0 || row >= _rows || col >= _cols)
        {
            return false;
        }

        // If the cell is already visited
        if (vis[row, col])
        {
            return false;
        }

        var cellValue = grid[row, col];
        return cellValue != 9 && (cellValue == value || cellValue == (value - 1));
    }

    public class HeightMap
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Dictionary<(int x, int y), int> Locations { get; set; } = new();
        public bool IsLowest((int x, int y) key, int value)
        {
            var isLowest = true;
            if (key.x > 0)
            {
                var left = Locations[(key.x - 1, key.y)];
                isLowest &= left > value;
            }
            if (key.y > 0)
            {
                var top = Locations[(key.x, key.y - 1)];
                isLowest &= top > value;

            }
            if (key.x < Columns)
            {
                var right = Locations[(key.x + 1, key.y)];
                isLowest &= right > value;

            }
            if (key.y < Rows)
            {
                var bottom = Locations[(key.x, key.y + 1)];
                isLowest &= bottom > value;
            }
            return isLowest;
        }
    }

    public class Basin
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public int Size { get; set; }
    }

}

