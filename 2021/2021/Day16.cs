namespace Advent2021;
public class Day16
{
    public static long GetMessageVersionSum(string filename)
    {
        var hex = File.ReadAllText(filename);
        var binary = DecodeHexToBinary(hex);
        var token = Tokenize(binary);
        return token.GetVersionSum();
    }

    public static object EvaluteExpression(string filename)
    {
        var hex = File.ReadAllText(filename);
        var binary = DecodeHexToBinary(hex);
        var token = Tokenize(binary);
        return token.Sum();
    }

    public static string DecodeHexToBinary(string hexinput)
    {
        var binary = new StringBuilder();
        foreach (var hex in hexinput)
        {
            binary.Append(HexToBinary(hex));
        }
        return binary.ToString();
    }

    public static string HexToBinary(char hex) =>
        hex switch
        {
            '0' => "0000",
            '1' => "0001",
            '2' => "0010",
            '3' => "0011",
            '4' => "0100",
            '5' => "0101",
            '6' => "0110",
            '7' => "0111",
            '8' => "1000",
            '9' => "1001",
            'A' => "1010",
            'B' => "1011",
            'C' => "1100",
            'D' => "1101",
            'E' => "1110",
            'F' => "1111",
            _ => throw new ArgumentException("Invalid hexadecimal character"),
        };

    public static (int ver, TokenType type) GetVersionAndType(string input) =>
        (BinaryStringToInt(input[..3]), BinaryStringToToken(input[3..6]));

    private static TokenType BinaryStringToToken(string v) =>
        v switch
        {
            "000" => TokenType.Sum,
            "001" => TokenType.Product,
            "010" => TokenType.Minimum,
            "011" => TokenType.Maximum,
            "100" => TokenType.Literal,
            "101" => TokenType.GreaterThan,
            "110" => TokenType.LessThan,
            "111" => TokenType.Equal,
            _ => throw new ArgumentException("Invalid token"),
        };

    public static long BinaryStringToLong(string binary)
    {
        binary = binary.PadLeft(64, '0');
        return Convert.ToInt64(binary, 2);
    }

    public static int BinaryStringToInt(string binary)
    {
        binary = binary.PadLeft(4, '0');
        return Convert.ToInt32(binary, 2);
    }

    public static (string binaryValue, long value, string remainder) ParseLiteralValue(string binary)
    {
        var isLast = binary[0] == '0';
        if (isLast)
        {
            return (binary[1..5], BinaryStringToLong(binary[1..5]), binary.Length > 5 ? binary[5..] : string.Empty);
        }
        else
        {
            var binaryValue = binary[1..5];
            var current = binary[5..];
            while (current[0] != '0' && current.Length >= 5)
            {
                binaryValue += current[1..5];
                current = current[5..];
            }
            if (current.Length >= 5)
            {
                binaryValue += current[1..5];
            }
            return (binaryValue, BinaryStringToLong(binaryValue), current.Length > 5 ? current[5..] : string.Empty);
        }
    }

    public static Token Tokenize(string binary)
    {
        var (version, type) = GetVersionAndType(binary);
        if (type == TokenType.Literal)
        {
            var (b, i, r) = ParseLiteralValue(binary[6..]);
            var literal = new Token
            {
                Version = version,
                Type = type,
                BinaryValue = b,
                LiteralValue = i,
                TokenLength = binary.Length - r.Length
            };
            return literal;
        }
        var token = new Token
        {
            Type = type,
            Version = version,
            LengthType = binary[6..7] == "0" ? LengthType.BitLength : LengthType.TokenNumber,
        };
        if (token.LengthType == LengthType.TokenNumber)
        {
            token.LengthValue = BinaryStringToInt(binary[7..18]);
            var remainder = binary[18..];
            for (int i = 0; i < token.LengthValue; i++)
            {
                var subToken = Tokenize(remainder);
                subToken.TokenOrder = i;
                token.SubTokens.Add(subToken);
                remainder = remainder[subToken.TokenLength..];
            }
            token.TokenLength = token.SubTokens.Sum(_ => _.TokenLength) + 18;
            return token;
        }
        else
        {
            token.LengthValue = BinaryStringToInt(binary[7..22]);
            var tokenInfo = binary[22..(22 + token.LengthValue)];
            var counter = 0;
            while (tokenInfo.Length > 7)
            {
                var subToken = Tokenize(tokenInfo);
                subToken.TokenOrder = counter;
                counter++;
                token.SubTokens.Add(subToken);
                tokenInfo = tokenInfo[subToken.TokenLength..];
            }
            token.TokenLength = token.SubTokens.Sum(_ => _.TokenLength) + 22;
            return token;
        }
    }

}

public class Token
{
    public TokenType Type { get; set; }
    public int Version { get; set; }
    public LengthType LengthType { get; set; }
    public int LengthValue { get; set; }
    public string BinaryValue { get; set; } = string.Empty;
    public long LiteralValue { get; set; }
    public List<Token> SubTokens = new();
    public long GetVersionSum() => Version + (SubTokens.Any() ? SubTokens.Sum(_ => _.GetVersionSum()) : 0);
    public int TokenLength { get; set; }
    public int TokenOrder { get; set; }
    public long Sum()
    {
        if (Type == TokenType.Literal)
        {
            return LiteralValue;
        }
        return Type switch
        {
            TokenType.Sum => SubTokens.Sum(_ => _.Sum()),
            TokenType.Product => SubTokens.Select(_ => _.Sum()).Aggregate((x, y) => x * y),
            TokenType.Minimum => SubTokens.Min(_ => _.Sum()),
            TokenType.Maximum => SubTokens.Max(_ => _.Sum()),
            TokenType.GreaterThan => SubTokens.First().Sum() > SubTokens.Last().Sum() ? 1 : 0,
            TokenType.LessThan => SubTokens.First().Sum() < SubTokens.Last().Sum() ? 1 : 0,
            TokenType.Equal => SubTokens.First().Sum() == SubTokens.Last().Sum() ? 1 : 0,
            _ => 0
        };
    }
}

public enum TokenType
{
    Sum = 0,
    Product = 1,
    Minimum = 2,
    Maximum = 3,
    Literal = 4,
    GreaterThan = 5,
    LessThan = 6,
    Equal = 7,
}

public enum LengthType
{
    TokenNumber = 11,
    BitLength = 15,
}