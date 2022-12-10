namespace Advent2021;
public class Day10
{
    public static int CalculateErrors(string filename)
    {
        var result = GetErrorAndLinesWithout(filename);
        return result.result;
    }

    public static long CalculateCompletions(string filename)
    {
        long result = 0;
        var (errors, lines) = GetErrorAndLinesWithout(filename);
        var scores = new List<long>();
        foreach (var line in lines)
        {
            //Adapted from https://stackoverflow.com/questions/15153533/how-to-find-the-matching-pair-of-braces-in-a-string
            var stack = new Stack<char>();
            for (int i = 0; i < line.Length; i++)
            {
                var bracket = '?';
                var ch = line[i];
                switch (ch)
                {
                    case '(':
                        stack.Push(ch);
                        break;
                    case '[':
                        stack.Push(ch);
                        break;
                    case '{':
                        stack.Push(ch);
                        break;
                    case '<':
                        stack.Push(ch);
                        break;
                    case ')':
                        bracket = stack.Any() ? stack.Pop() : '?';
                        break;
                    case '>':
                        bracket = stack.Any() ? stack.Pop() : '?';
                        break;
                    case '}':
                        bracket = stack.Any() ? stack.Pop() : '?';
                        break;
                    case ']':
                        bracket = stack.Any() ? stack.Pop() : '?';
                        break;
                    default:
                        break;
                }
            }
            if (stack.Any())
            {
                var chars = new List<char>();
                while (stack.Any())
                {
                    chars.Add(stack.Pop());
                }
                long score = 0;
                foreach (var ch in chars)
                {
                    score *= 5;
                    var x = GetCompletionPoints(ch);
                    score += x;
                }
                scores.Add(score);
            }
        }
        scores = scores.OrderByDescending(_ => _).ToList();
        result = scores.Skip((scores.Count() / 2)).First();
        return result;
    }

    private static (int result, List<string> linesWithoutErrors) GetErrorAndLinesWithout(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        var linesWithoutErrors = new List<string>();

        foreach (var line in lines)
        {
            var correct = true;
            //Adapted from https://stackoverflow.com/questions/15153533/how-to-find-the-matching-pair-of-braces-in-a-string
            var stack = new Stack<char>();
            for (int i = 0; i < line.Length; i++)
            {
                var bracket = '?';
                var ch = line[i];
                switch (ch)
                {
                    case '(':
                        stack.Push(ch);
                        break;
                    case '[':
                        stack.Push(ch);
                        break;
                    case '{':
                        stack.Push(ch);
                        break;
                    case '<':
                        stack.Push(ch);
                        break;
                    case ')':
                        bracket = stack.Any() ? stack.Pop() : '?';
                        if (bracket != '(')
                        {
                            result += GetPoints(')');
                            i = line.Length + 10;
                            correct = false;
                        }
                        break;
                    case '>':
                        bracket = stack.Any() ? stack.Pop() : '?';
                        if (bracket != '<')
                        {
                            result += GetPoints('>');
                            i = line.Length + 10;
                            correct = false;
                        }
                        break;
                    case '}':
                        bracket = stack.Any() ? stack.Pop() : '?';
                        if (bracket != '{')
                        {
                            result += GetPoints('}');
                            i = line.Length + 10;
                            correct = false;
                        }
                        break;
                    case ']':
                        bracket = stack.Any() ? stack.Pop() : '?';
                        if (bracket != '[')
                        {
                            result += GetPoints(']');
                            i = line.Length + 10;
                            correct = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            if (correct)
            {
                linesWithoutErrors.Add(line);
            }
        }
        return (result, linesWithoutErrors);
    }

    private static int GetPoints(char error) =>
        error switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
            _ => 0
        };

    private static int GetCompletionPoints(char error) =>
    error switch
    {
        '(' => 1,
        '[' => 2,
        '{' => 3,
        '<' => 4,
        _ => 0
    };
}
