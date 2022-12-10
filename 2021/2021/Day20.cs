namespace Advent2021;
public class Day20
{
    public static Dictionary<(int x, int y), int> RunImageTransform(string filename, int passes = 2)
    {
        var (alg, img, sizex, sizey) = ReadImageAndAlgorithm(filename, passes);
        for (int i = 0; i < passes; i++)
        {
            img = RunEnhancement(alg, img, i, sizex, sizey);
        }
        var minRow = img.Keys.Min(_ => _.y);
        var maxRow = img.Keys.Max(_ => _.y);
        var minCol = img.Keys.Min(_ => _.x);
        var maxCol = img.Keys.Max(_ => _.y);
        var maxX = img.Keys.Select(_ => _.x).Max();
        for (int row = minRow; row <= maxRow; row++)
        {
            for (int col = minCol; col <= maxRow; col++)
            {
                if (col < -passes)
                {
                    if (img.ContainsKey((col, row)))
                    {
                        img[(col, row)] = 0;

                    }
                }
                if (col > sizex + passes)
                {
                    img[(col, row)] = 0;

                }
                if (row < -passes)
                {
                    if (img.ContainsKey((col, row)))
                    {
                        img[(col, row)] = 0;

                    }
                }
                if (row > sizey + passes)
                {
                    if (img.ContainsKey((col, row)))
                    {
                        img[(col, row)] = 0;

                    }
                }

            }
        }
        return img;
    }

    public static (string algorithm, Dictionary<(int x, int y), int> img, int sizex, int sizey) ReadImageAndAlgorithm(string filename, int iterations)
    {
        var lines = File.ReadAllLines(filename);
        var algorithm = lines[0];
        var image = lines.Skip(2);
        var matrix = new Dictionary<(int x, int y), int>();
        for (var row = -iterations - 1; row < lines.Count() + iterations + 2; row++)
        {
            for (int col = -iterations - 1; col < image.First().Length + iterations + 2; col++)
            {
                if (row >= 0 && col >= 0 && row < image.Count() && col < image.First().Length)
                {
                    matrix.Add((col, row), image.ElementAt(row)[col] == '#' ? 1 : 0);
                }
                else
                {
                    matrix.Add((col, row), 0);
                }

            }
        }
        return (algorithm, matrix, image.Count(), image.First().Length);
    }

    public static Dictionary<(int x, int y), int> RunEnhancement(string algorithm, Dictionary<(int x, int y), int> image, int iteration, int sizex, int sizey)
    {
        var frameLeft = -iteration;
        var frameRight = sizex + iteration - 1;
        var frameTop = -iteration;
        var frameBottom = sizey + iteration - 1;
        var minRow = image.Keys.Min(_ => _.y);
        var maxRow = image.Keys.Max(_ => _.y);
        var minCol = image.Keys.Min(_ => _.x);
        var maxCol = image.Keys.Max(_ => _.y);
        var maxX = image.Keys.Select(_ => _.x).Max();
        var imgKey = (0, 0);
        for (int row = minRow; row <= maxRow; row++)
        {
            for (int col = minCol; col <= maxRow; col++)
            {
                if (col < -iteration)
                {
                    imgKey = (col, row);
                }
                if (col > sizex + iteration)
                {
                    imgKey = (col, row);
                }
                if (row < -iteration)
                {
                    imgKey = (col, row);
                }
                if (row > sizey + iteration)
                {
                    imgKey = (col, row);
                }
                if (image.ContainsKey(imgKey))
                {
                    image[imgKey] = algorithm[0] == '#' ? iteration % 2 == 1 ? 1 : 0 : 0;
                }
            }
        }

        var updates = new Dictionary<(int x, int y), int>();
        foreach (var key in image.Keys)
        {
            var (k, v, u) = GetValue(image, algorithm, key, iteration);
            if (image[k] != v)
            {
                updates[key] = v;
            }
        }
        foreach (var key in updates)
        {
            image[key.Key] = key.Value;
        }
        return image;
    }

    private static ((int x, int y) key, int val, Dictionary<(int, int), int> updates) GetValue(Dictionary<(int x, int y), int> image, string algorithm, (int x, int y) key, int iteration)
    {
        var updates = new Dictionary<(int, int), int>();
        var window = new int[3, 3];
        for (int row = -1; row < 2; row++)
        {
            for (int col = -1; col < 2; col++)
            {
                if (image.ContainsKey((key.x + col, key.y + row)))
                {
                    window[col + 1, row + 1] = image[(key.x + col, key.y + row)];
                }
            }
        }
        var binary = "";
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                binary += window[col, row];
            }
        }
        var val = algorithm[BinaryStringToInt(binary)] == '#' ? 1 : 0;
        return (key, val, updates);
    }

    public static int BinaryStringToInt(string binary)
    {
        binary = binary.PadLeft(64, '0');
        return Convert.ToInt32(binary, 2);
    }

}

