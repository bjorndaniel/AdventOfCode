namespace AoC2018.Tests
{
    public class Day5Tests
    {
        private readonly ITestOutputHelper _output;

        public Day5Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Can_parse_input()
        {
            //Given
            var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

            //When
            var result = Day5.ParseInput(filename);

            //Then
            Assert.True("dabAcCaCBAcCcaDA" == result, $"Expected result to be dabAcCaCBAcCcaDA but was {result}");
            
        }

        [Fact]
        public void Can_solve_part1_for_test()
        {
            //Given
            var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

            //When
            var result = Day5.Part1(filename, new TestPrinter(_output));

            //Then
            Assert.True("10" == result.Result, $"Expected result to be 10 but was {result.Result}");
        }

        [Fact]
        public void Can_solve_part2_for_test()
        {
            //Given
            var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

            //When
            var result = Day5.Part2(filename, new TestPrinter(_output));

            //Then
            Assert.True("4" == result.Result, $"Expected result to be 4 but was {result.Result}");

        }
    }
}
