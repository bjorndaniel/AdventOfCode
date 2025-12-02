using System.Text;

namespace AoC2016;

public class Day4
{
    public static List<Room> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Room>();
        foreach (var line in lines)
        {
            var parts = line.Split('[');
            var checksum = parts[1].TrimEnd(']');
            var nameAndId = parts[0];
            var lastDashIndex = nameAndId.LastIndexOf('-');
            var encryptedName = nameAndId.Substring(0, lastDashIndex);
            var sectorId = int.Parse(nameAndId.Substring(lastDashIndex + 1));
            result.Add(new Room(encryptedName, sectorId, checksum));
        }

        return result;
    }

    [Solveable("2016/Puzzles/Day4.txt", "Day 4 part 1", 4)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var rooms = ParseInput(filename);
        var result = 0;
        foreach (var room in rooms)
        {
            var letterCounts = new Dictionary<char, int>();
            foreach(var c in room.EncryptedName)
            {
                if (c == '-')
                {
                    continue;
                }
                if (!letterCounts.ContainsKey(c))
                {
                    letterCounts[c] = 0;
                }
                letterCounts[c]++;
            }
            var sortedLetters = letterCounts
                .OrderByDescending(kv => kv.Value)
                .ThenBy(kv => kv.Key)
                .Select(kv => kv.Key)
                .Take(5)
                .ToArray();
            var computedChecksum = new string(sortedLetters);
            if (computedChecksum == room.Checksum)
            {
                result += room.SectorId;
            }
        }

        return new SolutionResult(result.ToString());
    }

    [Solveable("2016/Puzzles/Day4.txt", "Day 4 part 2", 4)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var rooms = ParseInput(filename);
        foreach(var room in rooms)
        {
            var sb = new StringBuilder();
            var shift = room.SectorId % 26;
            foreach(var l in room.EncryptedName)
            {
                if(l == '-')
                {
                    sb.Append(' ');
                }
                else
                {
                    var baseChar = 'a';
                    var offset = l - baseChar;
                    var shifted = (char)(baseChar + ((offset + shift) % 26));
                    sb.Append(shifted);
                }
            }

            var decrypted = sb.ToString();
            if (decrypted.Contains("northpole", StringComparison.OrdinalIgnoreCase))
            {
                return new SolutionResult(room.SectorId.ToString());
            }
        }
        return new SolutionResult("");
    }

}
public record Room(string EncryptedName, int SectorId, string Checksum);