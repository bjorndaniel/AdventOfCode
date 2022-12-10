//namespace Advent2021;
//public class Day23OLD
//{
//    public static PlayingField GetPlayingField(string filename)
//    {
//        var lines = File.ReadAllLines(filename);
//        var doorway = lines.Skip(2).First();
//        var rooms = lines.Skip(3).First();
//        var playingField = new PlayingField();
//        playingField.Field[2, 1] = doorway[3];
//        playingField.Field[4, 1] = doorway[5];
//        playingField.Field[6, 1] = doorway[7];
//        playingField.Field[8, 1] = doorway[9];
//        playingField.Field[2, 2] = rooms[3];
//        playingField.Field[4, 2] = rooms[5];
//        playingField.Field[6, 2] = rooms[7];
//        playingField.Field[8, 2] = rooms[9];

//        return playingField;
//    }

//    public static (long score, PlayingField field) Play(string filename)
//    {
//        var results = new List<(int score, PlayingField field)>();

//        var playingField = GetPlayingField(filename);


//        return (0, playingField);
//        //playingField.MakeMove((6, 1, 3, 0));
//        //playingField.MakeMove((6, 1, 3, 0));
//        //playingField.MakeMove((4, 1, 6, 1));
//        //playingField.MakeMove((4, 2, 5, 0));
//        //playingField.MakeMove((3, 0, 4, 2));
//        //playingField.MakeMove((2, 1, 4, 1));
//        //playingField.MakeMove((8, 1, 7, 0));
//        //playingField.MakeMove((8, 2, 9, 0));
//        //playingField.MakeMove((7, 0, 8, 2));
//        //playingField.MakeMove((5, 0, 8, 1));
//        //playingField.MakeMove((9, 0, 2, 1));

//        //var (score, success) = playingField.TrySolve();
//        //if (success)
//        //{
//        //    results.Add((score, playingField));
//        //}
//        //if (results.Any())
//        //{
//        //    return results.OrderBy(_ => _.score).First();
//        //}
//        //return (0, new PlayingField());
//    }

//}


//private class PlayingField
//{
//    public PlayingField()
//    {
//        CreateField();
//    }

//    public char[,] Field { get; set; } = new char[11, 3];

//    public bool IsFinished =>
//        Field[2, 1] == 'A' &&
//        Field[2, 2] == 'A' &&
//        Field[4, 1] == 'B' &&
//        Field[4, 2] == 'B' &&
//        Field[6, 1] == 'C' &&
//        Field[6, 2] == 'C' &&
//        Field[8, 1] == 'D' &&
//        Field[8, 2] == 'D';

//    public int Score { get; set; }

//    public bool MakeMove((int colS, int rowS, int colE, int rowE) move)
//    {
//        try
//        {
//            var pod = Field[move.colS, move.rowS];
//            if (IsPod(pod) && !IsOccupied(Field[move.colE, move.rowE]))
//            {
//                var distance = Math.Abs(move.colS - move.colE) + Math.Abs(move.rowS - move.rowE);
//                if (move.rowS == move.rowE)
//                {
//                    distance += (2 * move.rowS);
//                }
//                Score += distance * (int)Enum.Parse<AmiphodType>(pod.ToString());
//                Field[move.colS, move.rowS] = '.';
//                Field[move.colE, move.rowE] = pod;
//                return true;
//            }
//        }
//        catch (Exception) { }
//        return false;
//    }

//    private void CreateField()
//    {
//        for (int row = 0; row < Field.GetLength(1); row++)
//        {
//            for (int col = 0; col < Field.GetLength(0); col++)
//            {
//                Field[col, row] = '.';
//            }
//        }

//        //Add blocked grids
//        Field[0, 1] = '#';
//        Field[0, 2] = '#';
//        Field[1, 1] = '#';
//        Field[1, 2] = '#';
//        Field[3, 1] = '#';
//        Field[3, 2] = '#';
//        Field[5, 1] = '#';
//        Field[5, 2] = '#';
//        Field[7, 1] = '#';
//        Field[7, 2] = '#';
//        Field[9, 1] = '#';
//        Field[9, 2] = '#';
//        Field[10, 1] = '#';
//        Field[10, 2] = '#';
//    }

//    private bool IsPod(char pod) =>
//        pod == 'A' || pod == 'B' || pod == 'C' || pod == 'D';

//    private bool IsOccupied(char square) =>
//        square != '.';

//}

//public enum AmiphodType
//{
//    A = 1,
//    B = 10,
//    C = 100,
//    D = 1000
//}
