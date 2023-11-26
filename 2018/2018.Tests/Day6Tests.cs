namespace AoC2018.Tests
{
    public class Day6Tests
    {
        private readonly ITestOutputHelper _output;

        public Day6Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Can_parse_input()
        {
            //Given
            var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

            //When
            var result = Day6.ParseInput(filename);

            //Then
            Assert.True(6 == result.Count, $"Expected result to be 6 but was {result.Count}");
            Assert.True((1, 1) == result.First(), $"Expected result to be (1,1) but was {result.First()}");
            Assert.True((8, 9) == result.Last(), $"Expected result to be (1,6) but was {result.Last()}");
        }

        [Fact]
        public void Can_solve_part1_for_test()
        {
            //Given
            var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

            //When
            var result = Day6.Part1(filename, new TestPrinter(_output));

            //Then
            Assert.True("17" == result.Result, $"Expected result to be 17 but was {result.Result}");
        }

        [Fact]
        public void Can_solve_part2_for_test()
        {
            //Given
            var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

            //When
            var result = Day6.Part2(filename, new TestPrinter(_output));

            //Then
            Assert.True("16" == result.Result, $"Expected result to be 4 but was {result.Result}");

        }
    }
}
