namespace Advent2021.Tests;
public class Day20Tests
{
    private readonly ITestOutputHelper _output;

    public Day20Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_convert_binary()
    {
        //Given
        var input = "000100010";

        //When
        var result = Day20.BinaryStringToInt(input);

        //Then
        Assert.Equal(34, result);
    }

    [Fact]
    public void Can_read_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day20-test.txt";

        //When
        var (algorithm, image, x, y) = Day20.ReadImageAndAlgorithm(filename, 0);

        //Then
        Assert.Equal(5, x);
        Assert.Equal(5, y);
        Assert.Equal(512, algorithm.Length);
        Assert.Equal('#', algorithm[34]);
        Assert.Equal('.', algorithm[18]);
        Assert.Equal('.', algorithm[48]);
        Assert.Equal(1, image[(0, 0)]);
        Assert.Equal(0, image[(1, 0)]);
        Assert.Equal(0, image[(2, 0)]);
        Assert.Equal(1, image[(3, 0)]);
        Assert.Equal(0, image[(4, 0)]);
        Assert.Equal(1, image[(0, 1)]);
        Assert.Equal(0, image[(1, 1)]);
        Assert.Equal(0, image[(2, 1)]);
        Assert.Equal(0, image[(3, 1)]);
        Assert.Equal(0, image[(4, 1)]);
        Assert.Equal(0, image[(0, 4)]);
        Assert.Equal(0, image[(1, 4)]);
        Assert.Equal(1, image[(2, 4)]);
        Assert.Equal(1, image[(3, 4)]);
        Assert.Equal(1, image[(4, 4)]);
    }

    [Fact]
    public void Can_enhance()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day20-test.txt";
        var (algorithm, image, _, _) = Day20.ReadImageAndAlgorithm(filename, 1);

        //When
        Print(image);
        var result = Day20.RunEnhancement(algorithm, image, 0, 5, 5);
        Print(result);

        //Then
        Assert.Equal(0, result[(0, 0)]);
        Assert.Equal(0, result[(1, 0)]);
        Assert.Equal(1, result[(2, 0)]);
    }

    [Fact]
    public void Can_run_transform_twice()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day20-test.txt";
        var (algorithm, image, _, _) = Day20.ReadImageAndAlgorithm(filename, 2);

        //When
        var result = Day20.RunImageTransform(filename, 2);
        Print(result);

        //Then
        Assert.Equal(35, result.Count(_ => _.Value == 1));
    }

    [Fact]
    public void Can_run_transform()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day20-test.txt";
        var (algorithm, image, _, _) = Day20.ReadImageAndAlgorithm(filename, 50);

        //When
        var result = Day20.RunImageTransform(filename, 50);
        Print(result);

        //Then
        Assert.Equal(3351, result.Count(_ => _.Value == 1));
    }

    private void Print(Dictionary<(int x, int y), int> image)
    {
        var minRow = image.Keys.Min(_ => _.y);
        var maxRow = image.Keys.Max(_ => _.y);
        var minCol = image.Keys.Min(_ => _.x);
        var maxCol = image.Keys.Max(_ => _.y);
        for (int row = minRow; row <= maxRow; row++)
        {
            var sb = new StringBuilder();
            for (int col = minCol; col <= maxRow; col++)
            {
                if (image.ContainsKey((col, row)))
                {
                    var value = image[(col, row)];
                    sb.Append(value == 0 ? "." : (value == 1 ? "#" : "_"));
                }
            }
            _output.WriteLine(sb.ToString());
        }
        _output.WriteLine("");
    }
}

