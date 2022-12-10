namespace Advent2021;
public class Day13
{
    public static (int dots, char?[,] paper) CountDots(string filename, bool foldAll = false)
    {
        var result = 0;
        var (paper, instructions) = GetPaperMatrix(filename);
        foreach (var instruction in instructions)
        {
            if (instruction.direction == 'x')
            {
                var leftColumns = instruction.foldLine;
                var xMax = leftColumns * 2;
                var paperSize = paper.GetLength(0) - 1;
                var diff = xMax > paperSize ? xMax - paperSize : 0;
                var x1 = 0 + diff;
                xMax = xMax - diff;
                while (xMax > instruction.foldLine)
                {
                    for (int y = 0; y < paper.GetLength(1); y++)
                    {
                        var dot = paper[xMax, y];
                        if (dot == null)
                        {
                            if (paper[x1, y] != null)
                            {
                                result++;
                            }
                        }
                        else
                        {
                            result++;
                            paper[x1, y] = '#';
                        }
                    }
                    xMax--;
                    x1++;
                }

                var tempPaper = new char?[instruction.foldLine, paper.GetLength(1)];
                for (int row = 0; row < tempPaper.GetLength(1); row++)
                {
                    for (int col = 0; col < tempPaper.GetLength(0); col++)
                    {
                        tempPaper[col, row] = paper[col, row];
                    }
                }
                paper = tempPaper;
            }
            else
            {
                var topColumns = instruction.foldLine;
                var yMax = topColumns * 2;
                var paperSize = paper.GetLength(1) - 1;
                var diff = yMax > paperSize ? yMax - paperSize : 0;
                var y1 = 0 + diff;
                yMax = yMax - diff;
                while (yMax > instruction.foldLine)
                {
                    for (int x = 0; x < paper.GetLength(0); x++)
                    {
                        var dot = paper[x, yMax];
                        if (dot == null)
                        {
                            if (paper[x, y1] != null)
                            {
                                result++;
                            }
                        }
                        else
                        {
                            paper[x, y1] = '#';
                            result++;
                        }

                    }
                    yMax--;
                    y1++;
                }
                var tempPaper = new char?[paper.GetLength(0), instruction.foldLine];
                for (int row = 0; row < tempPaper.GetLength(1); row++)
                {
                    for (int col = 0; col < tempPaper.GetLength(0); col++)
                    {
                        tempPaper[col, row] = paper[col, row];
                    }
                }
                paper = tempPaper;
            }
            if (!foldAll)
            {
                break;
            }
        }
        return (result, paper);
    }

    public static void GetCode(string filename)
    {
        var (_, paper) = CountDots(filename, true);
        PrintPaper(paper);
    }

    private static void PrintPaper(char?[,] paper)
    {
        int rowLength = paper.GetLength(1);
        int colLength = paper.GetLength(0);
        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                if (paper[j, i] == null)
                {
                    Console.Write("  ");
                }
                else
                {
                    Console.Write("# ");
                }
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }
    }

    public static (char?[,] paper, List<(int foldLine, char direction)>) GetPaperMatrix(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var coOrdLines = lines.Where(_ => !_.StartsWith("fold") && !string.IsNullOrWhiteSpace(_));
        var coOrds = coOrdLines.Select(line =>
        {
            var x = int.Parse(line.Split(',')[0]);
            var y = int.Parse(line.Split(',')[1]);
            return (x, y);
        });
        var paper = new char?[coOrds.Max(_ => _.x) + 1, coOrds.Max(_ => _.y) + 1];
        foreach (var c in coOrds)
        {
            paper[c.x, c.y] = '#';
        }
        var instructionList = new List<(int foldLine, char direction)>();
        var instructions = lines.Where(_ => _.StartsWith("fold"));
        foreach (var i in instructions)
        {
            var instruction = i.Split(' ').Last();
            var direction = instruction.Split('=').First();
            var coord = int.Parse(instruction.Split('=').Last());
            instructionList.Add((coord, direction[0]));
        }
        return (paper, instructionList);
    }
}

