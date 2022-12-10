
namespace Advent2021.Tests;
public class Day22Tests
{

    [Theory]
    [InlineData("day22-test.txt", 39)]
    [InlineData("day22-test1.txt", 590784)]
    public void Can_initialize(string file, int expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}{file}";

        //When
        var result = Day22.Initialize(filename);

        //Then
        Assert.Equal(expected, result);
    }


    [Theory]
    [InlineData("day22-test.txt", 39)]
    [InlineData("day22-test2.txt", 2758514936282235)]
    public void Can_reboot(string file, long expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}{file}";

        //When
        var result = Day22.Reboot(filename);

        //Then
        Assert.Equal(expected, result);
    }


    [Theory]
    [InlineData(10, 12, 10, 12, 10, 12, 27)]
    [InlineData(-12, -10, -12, -10, -12, -10, 27)]
    public void Can_get_cube_count(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax, long expected)
    {
        //Given
        var swarm = new CubeSwarm
        {
            XMin = xMin,
            YMin = yMin,
            ZMin = zMin,
            XMax = xMax,
            YMax = yMax,
            ZMax = zMax,
        };
        //When
        var result = swarm.GetCubeCount();

        //Then
        Assert.Equal(expected, result);
    }


    [Theory]
    [ClassData(typeof(CubeSwarmTestData))]
    public void Can_check_intersection(List<CubeSwarm> testdata)
    {
        //When
        var resultTrue = Day22.Intersects(testdata.First(), testdata.Skip(1).First());
        var resultFalse = Day22.Intersects(testdata.Skip(2).First(), testdata.Skip(3).First());
        var resultTrue1 = Day22.Intersects(testdata.Skip(4).First(), testdata.Skip(5).First());


        //Then
        Assert.True(resultTrue);
        Assert.False(resultFalse);
        Assert.True(resultTrue1);
    }

}
public class CubeSwarmTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<CubeSwarm>
            {

                new CubeSwarm
                {
                    XMin = 10,
                    XMax= 12,
                    YMin = 10,
                    YMax = 12,
                    ZMin = 10,
                    ZMax = 12,
                },

                new CubeSwarm
                {
                    XMin = 11,
                    XMax = 13,
                    YMin= 11,
                    YMax= 13,
                    ZMin= 11,
                    ZMax= 13,
                },
                new CubeSwarm
                {
                     XMin = 10,
                     XMax= 12,
                     YMin = 10,
                     YMax = 12,
                     ZMin = 10,
                     ZMax = 12,
                },

                new CubeSwarm
                {
                    XMin= 1,
                    XMax= 4,
                    YMin= 1,
                    YMax= 4,
                    ZMin= 1,
                    ZMax= 4,
                },
                new CubeSwarm
                {
                     XMin = 10,
                     XMax= 13,
                     YMin = 10,
                     YMax = 13,
                     ZMin = 10,
                     ZMax = 13,
                },

                new CubeSwarm
                {
                    XMin= 9,
                    XMax= 11,
                    YMin= 9,
                    YMax= 11,
                    ZMin= 9,
                    ZMax= 11,
                }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

