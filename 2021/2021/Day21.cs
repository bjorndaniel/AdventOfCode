namespace Advent2021;
public static class Day21
{
    public static Dictionary<(int p1Score, int p1Position, int p2Score, int p2Position), (long p1Wins, long p2Wins)> _universes = new();
    private static int[] _possibleRolls = new int[]
    {
        3, 4, 5, 4, 5, 6, 5, 6, 7, 4, 5, 6, 5, 6, 7, 6, 7, 8, 5, 6, 7, 6, 7, 8, 7, 8, 9
    };

    public static (long losingScore, long dieRolls) Play(long player1, long player2)
    {
        long score1 = 0;
        long score2 = 0;
        int die = 1;
        var player1Turn = true;
        var hasWon = false;
        var dieRolls = 0;
        while (!hasWon)
        {
            dieRolls += 3;
            var (dieScore, nextDie) = GetDieScore(die);
            die = nextDie;
            if (player1Turn)
            {
                var nextPosition = player1 + dieScore;
                nextPosition = nextPosition > 9 ? nextPosition % 10 : nextPosition;
                player1 = nextPosition == 0 ? 10 : nextPosition;
                score1 += player1;
            }
            else
            {
                var nextPosition = player2 + dieScore;
                player2 = nextPosition > 11 ? nextPosition % 10 : nextPosition;
                score2 += player2;
            }
            player1Turn = !player1Turn;
            hasWon = score1 >= 1000 || score2 >= 1000;
        }
        return (score1 > score2 ? score2 : score1, dieRolls);
    }

    public static long Play2(int player1Start, int player2Start)
    {
        var (p1, p2) = PlayQuantum(player1Start - 1, 0, player2Start - 1, 0);
        return Math.Max(p1, p2);
    }

    //https://gist.github.com/thatsumoguy/28cc019af9f3ded13d2e204adb320bd2
    //Had almost the same solution but could not figure out why it didnt work so eventually gave up, will have to revisit my solution at some point
    public static (long p1WIns, long p2Wins) PlayQuantum(int player1Position, int player1Score, int player2Position, int player2Score)
    {
        if (player1Score > 20)
        {
            return (1, 0);
        }
        if (player2Score > 20)
        {
            return (0, 1);
        }
        if (!_universes.ContainsKey((player1Position, player1Score, player2Position, player2Score)))
        {
            var (x, y) = (0L, 0L);

            foreach (var roll in _possibleRolls)
            {
                var newScore = (player1Position + roll) % 10;
                var (i, j) = PlayQuantum(player2Position, player2Score, newScore, player1Score + newScore + 1);
                x += j;
                y += i;

                _universes[(player1Position, player1Score, player2Position, player2Score)] = (x, y);
            }
        }
        return _universes[(player1Position, player1Score, player2Position, player2Score)];

    }

    //Tired, this could be simpler...
    public static (int score, int die) GetDieScore(int currentDie)
    {
        var dieScore = 0;
        if (currentDie < 98)
        {
            for (int i = currentDie; i < (currentDie + 3); i++)
            {
                dieScore += i;
            }
            return (dieScore, currentDie + 3);
        }
        if (currentDie == 98)
        {
            return (98 + 99 + 100, 1);
        }
        if (currentDie == 99)
        {
            return (99 + 100 + 1, 2);
        }
        return (100 + 1 + 2, 3);
    }

}

