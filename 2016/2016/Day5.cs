namespace AoC2016;

public class Day5
{
    public static string ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.First();
    }

    [Solveable("2016/Puzzles/Day5.txt", "Day 5 part 1", 5, true)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var inputBytes = Encoding.UTF8.GetBytes(input);

        // buffer containing input bytes followed by ascii digits for the index
        var buffer = new byte[inputBytes.Length + 20];
        inputBytes.CopyTo(buffer, 0);

        var password = new char[8];
        var filled = 0;
        var index = 0;

        using var md5 = MD5.Create();
        Span<byte> hash = stackalloc byte[16];

        while (filled < 8)
        {
            var digitsLen = WriteIntAscii(index, buffer.AsSpan(inputBytes.Length));

            if (!md5.TryComputeHash(buffer.AsSpan(0, inputBytes.Length + digitsLen), hash, out _))
            {
                index++;
                continue;
            }

            // five leading hex zeroes => first 20 bits zero
            if (hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0)
            {
                var lowNibble = hash[2] & 0x0F;
                password[filled++] = (char)(lowNibble < 10 ? ('0' + lowNibble) : ('a' + (lowNibble - 10)));
            }

            index++;
        }

        return new SolutionResult(new string(password));
    }

    [Solveable("2016/Puzzles/Day5.txt", "Day 5 part 2", 5, true)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var buffer = new byte[inputBytes.Length + 20];
        inputBytes.CopyTo(buffer, 0);

        var password = new char[8];
        for (int i = 0; i < 8; i++) password[i] = '\0';
        var filled = 0;
        var index = 0;

        using var md5 = MD5.Create();
        Span<byte> hash = stackalloc byte[16];

        while (filled < 8)
        {
            var digitsLen = WriteIntAscii(index, buffer.AsSpan(inputBytes.Length));

            if (!md5.TryComputeHash(buffer.AsSpan(0, inputBytes.Length + digitsLen), hash, out _))
            {
                index++;
                continue;
            }

            if (hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0)
            {
                var position = hash[2] & 0x0F; // hex[5]
                if (position < 8)
                {
                    // character is hex[6] -> high nibble of hash[3]
                    var charNibble = (hash[3] & 0xF0) >> 4;
                    var ch = (char)(charNibble < 10 ? ('0' + charNibble) : ('a' + (charNibble - 10)));

                    if (password[position] == '\0')
                    {
                        password[position] = ch;
                        filled++;
                    }
                }
            }

            index++;
        }

        return new SolutionResult(new string(password));
    }

    // write integer as ascii decimal into dest span; returns number of bytes written
    private static int WriteIntAscii(int value, Span<byte> dest)
    {
        if (value == 0)
        {
            dest[0] = (byte)'0';
            return 1;
        }

        var pos = 0;
        Span<byte> tmp = stackalloc byte[20];
        var v = value;
        while (v > 0)
        {
            tmp[pos++] = (byte)('0' + (v % 10));
            v /= 10;
        }

        for (int i = 0; i < pos; i++)
        {
            dest[i] = tmp[pos - 1 - i];
        }
        return pos;
    }

}