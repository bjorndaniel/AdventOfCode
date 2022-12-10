namespace Advent2021.Tests;
public class Day16Tests
{

    [Fact]
    public void Can_add_versions()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day16-test.txt";

        //When
        var result = Day16.GetMessageVersionSum(filename);

        //Then
        Assert.Equal(16, result);
    }

    [Fact]
    public void Can_add_versions2()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day16-test2.txt";

        //When
        var result = Day16.GetMessageVersionSum(filename);

        //Then
        Assert.Equal(12, result);
    }

    [Fact]
    public void Can_add_versions3()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day16-test3.txt";

        //When
        var result = Day16.GetMessageVersionSum(filename);

        //Then
        Assert.Equal(23, result);
    }

    [Fact]
    public void Can_add_versions4()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day16-test4.txt";

        //When
        var result = Day16.GetMessageVersionSum(filename);

        //Then
        Assert.Equal(31, result);
    }

    [Fact]
    public void Can_add_versions5()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day16-test5.txt";

        //When
        var result = Day16.GetMessageVersionSum(filename);

        //Then
        Assert.Equal(14, result);
    }

    [Fact]
    public void Can_add_versions6()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day16-test6.txt";

        //When
        var result = Day16.GetMessageVersionSum(filename);

        //Then
        Assert.Equal(9, result);
    }

    [Fact]
    public void Can_get_binary_string_from_hex()
    {
        //Given
        var hexString = "D2FE28";

        //When
        var result = Day16.DecodeHexToBinary(hexString);

        //Then
        Assert.Equal("110100101111111000101000", result);
    }

    [Fact]
    public void Can_get_int_from_binary()
    {
        //Given
        var input = "011111100101";

        //When
        var result = Day16.BinaryStringToInt(input);

        //Then
        Assert.Equal(2021, result);
    }
    [Fact]
    public void Can_get_int_from_binary_3()
    {
        //Given
        var input = "110";

        //When
        var result = Day16.BinaryStringToInt(input);

        //Then
        Assert.Equal(6, result);
    }

    [Fact]
    public void Can_get_version_and_type()
    {
        //Given
        var input = Day16.DecodeHexToBinary("EE00D40C823060");

        //When
        var (ver, type) = Day16.GetVersionAndType(input);

        //Then
        Assert.Equal(7, ver);
        Assert.Equal(TokenType.Maximum, type);
    }

    [Fact]
    public void Can_parse_literal()
    {
        //Given
        var binary = "101111111000101000";

        //When
        var (binaryValue, value, remainder) = Day16.ParseLiteralValue(binary);

        //Then
        Assert.Equal(2021, value);
        Assert.Equal("011111100101", binaryValue);
        Assert.Equal("000", remainder);

    }

    [Fact]
    public void Can_parse_literal2()
    {
        //Given
        var binary = "00101000";

        //When
        var (binaryValue, value, remainder) = Day16.ParseLiteralValue(binary);

        //Then
        Assert.Equal(5, value);
        Assert.Equal("0101", binaryValue);
        Assert.Equal("000", remainder);
        Assert.Equal("000", remainder);
    }

    [Fact]
    public void Can_parse_literal3()
    {
        //Given
        var binary = "11010001010";

        //When
        var result = Day16.Tokenize(binary);

        //Then
        Assert.Equal(10, result.LiteralValue);
        Assert.Equal("1010", result.BinaryValue);
        //Assert.Equal(string.Empty, result.BinaryRemainder);
    }

    [Fact]
    public void Can_parse_tokentype_literal()
    {
        //Given
        var literal = "110100101111111000101000";

        //When
        var result = Day16.Tokenize(literal);

        //Then
        Assert.Equal(TokenType.Literal, result.Type);
    }

    [Fact]
    public void Can_parse_tokentype_operator()
    {
        //Given
        var op = "00111000000000000110111101000101001010010001001000000000";

        //When
        var result = Day16.Tokenize(op);

        //Then
        Assert.Equal(TokenType.LessThan, result.Type);
        Assert.Equal(1, result.Version);
        Assert.Equal(LengthType.BitLength, result.LengthType);
        Assert.Equal(27, result.LengthValue);
        Assert.Equal(2, result.SubTokens.Count);
        Assert.Equal(9, result.GetVersionSum());
        //Assert.Equal("0000000", result.BinaryRemainder);
    }

    [Fact]
    public void Can_parse_tokentype_operator2()
    {
        //Given
        var op = "11101110000000001101010000001100100000100011000001100000";

        //When
        var result = Day16.Tokenize(op);

        //Then
        Assert.Equal(3, result.SubTokens.Count());
        Assert.Equal(1, result.SubTokens.First().LiteralValue);
        Assert.Equal(2, result.SubTokens.Skip(1).First().LiteralValue);
        Assert.Equal(3, result.SubTokens.Skip(2).First().LiteralValue);
        Assert.Equal(TokenType.Maximum, result.Type);
        Assert.Equal(7, result.Version);
        Assert.Equal(14, result.GetVersionSum());
        Assert.Equal(LengthType.TokenNumber, result.LengthType);
        Assert.Equal(3, result.LengthValue);
        //Assert.Equal(string.Empty, result.BinaryRemainder);
    }

    [Fact]
    public void Can_get_token_length()
    {
        //Given
        var input = "11101110000000001101010000001100100000100011000001100000";

        //When
        var result = Day16.Tokenize(input);

        //Then
        Assert.Equal(3, result.SubTokens.Count);
        Assert.Equal(11, result.SubTokens.First().TokenLength);
        Assert.Equal(11, result.SubTokens.Skip(1).First().TokenLength);
        Assert.Equal(11, result.SubTokens.Last().TokenLength);
        Assert.Equal(input.Length -5, result.TokenLength);
    }

    [Fact]
    public void Can_get_token_length1()
    {
        //Given
        var input = "00111000000000000110111101000101001010010001001000000000";

        //When
        var result = Day16.Tokenize(input);

        //Then
        Assert.Equal(2, result.SubTokens.Count);
        Assert.Equal(11, result.SubTokens.First().TokenLength);
        Assert.Equal(16, result.SubTokens.Last().TokenLength);
        Assert.Equal(input.Length - 7, result.TokenLength);
    }

    [Fact]
    public void Can_get_sum()
    {
        //Given
        var input = Day16.DecodeHexToBinary("C200B40A82");

        //When
        var result = Day16.Tokenize(input);

        //Then
        Assert.Equal(TokenType.Sum, result.Type);
        Assert.Equal(3, result.Sum());
    }

    [Fact]
    public void Can_get_product()
    {
        //Given
        var input = Day16.DecodeHexToBinary("04005AC33890");

        //When
        var result = Day16.Tokenize(input);

        //Then
        Assert.Equal(TokenType.Product, result.Type);
        Assert.Equal(54, result.Sum());
    }

    [Fact]
    public void Can_get_minimum()
    {
        //Given
        var input = Day16.DecodeHexToBinary("880086C3E88112");

        //When
        var result = Day16.Tokenize(input);

        //Then
        Assert.Equal(TokenType.Minimum, result.Type);
        Assert.Equal(7, result.Sum());
    }

    [Fact]
    public void Can_get_maximum()
    {
        //Given
        var input = Day16.DecodeHexToBinary("CE00C43D881120");

        //When
        var result = Day16.Tokenize(input);

        //Then
        Assert.Equal(TokenType.Maximum, result.Type);
        Assert.Equal(9, result.Sum());
    }

    [Fact]
    public void Can_get_lessthan()
    {
        //Given
        var input = Day16.DecodeHexToBinary("D8005AC2A8F0");

        //When
        var result = Day16.Tokenize(input);

        //Then
        Assert.Equal(TokenType.LessThan, result.Type);
        Assert.Equal(1, result.Sum());
    }

    [Fact]
    public void Can_get_greaterthan()
    {
        //Given
        var input = Day16.DecodeHexToBinary("F600BC2D8F");

        //When
        var result = Day16.Tokenize(input);

        //Then
        Assert.Equal(TokenType.GreaterThan, result.Type);
        Assert.Equal(0, result.Sum());
    }

    [Fact]
    public void Can_get_equal()
    {
        //Given
        var input = Day16.DecodeHexToBinary("9C005AC2F8F0");

        //When
        var result = Day16.Tokenize(input);

        //Then
        Assert.Equal(TokenType.Equal, result.Type);
        Assert.Equal(0, result.Sum());
    }

    [Fact]
    public void Can_get_compound_product()
    {
        //Given
        var input = Day16.DecodeHexToBinary("9C0141080250320F1802104A08");

        //When
        var result = Day16.Tokenize(input);

        //Then
        Assert.Equal(TokenType.Equal, result.Type);
        Assert.Equal(1, result.Sum());
    }
}

