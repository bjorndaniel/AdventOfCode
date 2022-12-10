namespace Advent2021;
public class Day3
{

    public static int CalculatePowerConsumption(string filename)
    {
        var inputs = File.ReadAllLines(filename).ToList();
        var gamma = "";
        var epsilon = "";
        for (int i = 0; i < inputs.First().Length; i++)
        {
            gamma += GetCommonValue(inputs, i, true, true);
            epsilon += GetCommonValue(inputs, i, false, true);
        }
        var gammaNumber = GetIntFromBinaryString(gamma);
        var epsilonNumber = GetIntFromBinaryString(epsilon);
        return gammaNumber * epsilonNumber;
    }

    public static int CalculateLifeSupportRating(string filename)
    {
        var inputs = File.ReadAllLines(filename);
        var oxygenNumber = GetDiagnosticNumber(inputs, true);
        var co2ScrubberNumber = GetDiagnosticNumber(inputs, false);
        return oxygenNumber * co2ScrubberNumber;
    }

    private static int GetDiagnosticNumber(string[] inputs, bool isOxygen)
    {
        var remaining = inputs.ToList();
        var position = 0;
        var pattern = "";
        while (remaining.Count > 1)
        {
            var positionValue = GetCommonValue(remaining, position, isOxygen, isOxygen);
            pattern += positionValue;
            remaining = inputs.Where(x => x.StartsWith(pattern)).ToList();
            position++;
        }
        return GetIntFromBinaryString(remaining.First());
    }

    private static int GetIntFromBinaryString(string binary) =>
        Convert.ToInt32(binary, 2);

    private static int GetCommonValue(List<string> inputs, int position, bool mostCommon, bool equalReturns1)
    {
        var positions = inputs.Select(x => x[position]);
        var ones = positions.Count(_ => _.Equals('1'));
        var zeros = positions.Count(_ => _.Equals('0'));
        if (ones == zeros)
        {
            return equalReturns1 ? 1 : 0;
        }
        if (mostCommon)
        {
            return ones > zeros ? 1 : 0;
        }
        return ones < zeros ? 1 : 0;
    }
}
