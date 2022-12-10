namespace Advent2021;
public class Day4
{
    public static int PlayBingo(string filename, bool lastBoard = false)
    {
        var result = 0;
        var (numbers, boards) = ReadAllLines(filename);
        foreach (var number in numbers)
        {
            foreach (var board in boards)
            {
                var bingo = board.MarkNumber(number);
                if (bingo && (lastBoard ? boards.All(_ => _.HasBingo) : true))
                {
                    var sum = board.Numbers.Where(_ => !_.IsMarked).Sum(_ => _.Number);
                    return sum * number;
                }
            }
        }
        return result;
    }

    public static int LastBoardToWin(string filename) =>
        PlayBingo(filename, true);


    public static (IEnumerable<int> numbers, List<Board> boards) ReadAllLines(string filename)
    {
        var result = new List<string>();
        var input = File.ReadAllText(filename);
        var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var numbers = lines.First().Split(new[] { ',' }).Select(_ => int.Parse(_));
        var rowNumber = 1;
        var boards = new List<Board>();
        foreach (var line in lines)
        {
            var boardLines = lines.Skip(rowNumber).Take(5).ToArray();
            if (boardLines.Count() == 0)
            {
                break;
            }
            rowNumber += 5;
            var board = new Board();
            boards.Add(board);
            for (int row = 0; row < 5; row++)
            {
                for (int column = 0; column < 5; column++)
                {
                    var rowNumbers = boardLines[row].Split(new[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);
                    board.Numbers.Add(
                        new BoardNumber
                        {
                            Number = int.Parse(rowNumbers[column]),
                            Row = row,
                            Column = column,
                        });
                }
            }
        }
        return (numbers, boards);
    }

    public class Board
    {
        public List<BoardNumber> Numbers { get; set; } = new();
        public bool HasBingo { get; set; }

        public bool MarkNumber(int ballNumber)
        {
            foreach (var number in Numbers)
            {
                if (number.Number == ballNumber)
                {
                    number.IsMarked = true;
                }
            }
            var bingoRow = Numbers.GroupBy(_ => _.Row).Any(r => r.All(n => n.IsMarked));
            var bingoColumn = Numbers.GroupBy(_ => _.Column).Any(r => r.All(n => n.IsMarked));
            HasBingo = bingoRow || bingoColumn;
            return bingoRow || bingoColumn;
        }
    }

    public class BoardNumber
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Number { get; set; }
        public bool IsMarked { get; set; }
    }
}


