namespace Advent2021;
public class Day11
{
    private static int _rows;
    private static int _cols;

    public static int CalculateFlashes(string filename, bool checkSynchronized = false)
    {
        var result = 0;
        var octopi = GetMatrix(filename);
        var rounds = 100;
        if (checkSynchronized)
        {
            rounds = int.MaxValue;
        }
        for (int i = 0; i < rounds; i++)
        {
            octopi.ForEach(_ =>
            {
                _.EnergyLevel++;
                _.HasFlashed = _.EnergyLevel == 10;
            });
            while (octopi.Count(_ => _.EnergyLevel == 10) > 0)
            {
                foreach (var octopus in octopi)
                {
                    if (octopus.HasFlashed)
                    {
                        octopus.EnergyLevel++;
                        if (octopus.EnergyLevel > 11)
                        {
                            continue;
                        }
                        var adjacent = GetAdjacent(octopi, octopus);
                        foreach (var o in adjacent)
                        {
                            if (!o.HasFlashed)
                            {
                                o.EnergyLevel++;
                                if (o.EnergyLevel == 10)
                                {
                                    o.HasFlashed = true;
                                }
                            }
                        }
                    }
                }
            }
            result += octopi.Count(_ => _.HasFlashed);
            octopi.ForEach(_ =>
            {
                _.EnergyLevel = _.HasFlashed ? 0 : _.EnergyLevel;
                _.HasFlashed = false;
            });
            if (octopi.All(_ => _.EnergyLevel == 0))
            {
                return i + 1;
            }
        }
        return result;
    }

    public static int CalculateSynchronized(string filename) =>
        CalculateFlashes(filename, true);

    private static List<Octopus> GetAdjacent(List<Octopus> octopi, Octopus octopus)
    {
        var result = new List<Octopus>();
        //Above
        if (octopus.Row > 0)
        {
            result.Add(octopi.First(_ => _.Row == octopus.Row - 1 && _.Col == octopus.Col));
            if (octopus.Col > 0)
            {
                try
                {
                    result.Add(octopi.First(_ => _.Row == octopus.Row - 1 && _.Col == octopus.Col - 1));
                }
                catch (Exception)
                {

                    throw;
                }
            }
            if (octopus.Col < _rows)
            {
                try
                {
                    result.Add(octopi.First(_ => _.Row == octopus.Row - 1 && _.Col == octopus.Col + 1));
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        //Left
        if (octopus.Col > 0)
        {
            try
            {
                result.Add(octopi.First(_ => _.Row == octopus.Row && _.Col == octopus.Col - 1));
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Right
        if (octopus.Col < _cols)
        {
            try
            {
                result.Add(octopi.First(_ => _.Row == octopus.Row && _.Col == octopus.Col + 1));
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Below
        if (octopus.Row < _rows)
        {
            result.Add(octopi.First(_ => _.Row == octopus.Row + 1 && _.Col == octopus.Col));
            if (octopus.Col > 0)
            {
                try
                {
                    result.Add(octopi.First(_ => _.Row == octopus.Row + 1 && _.Col == octopus.Col - 1));
                }
                catch (Exception)
                {

                    throw;
                }
            }
            if (octopus.Col < _cols)
            {
                try
                {
                    result.Add(octopi.First(_ => _.Row == octopus.Row + 1 && _.Col == octopus.Col + 1));
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        return result;
    }

    private static List<Octopus> GetMatrix(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Octopus>();
        _rows = lines.Length - 1;
        _cols = lines.Max(_ => _.Length) - 1;
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                result.Add(new Octopus(int.Parse((lines[y][x]).ToString()), y, x));
            }
        }
        return result;
    }

    public class Octopus
    {
        public Octopus(int energyLevel, int row, int col)
        {
            EnergyLevel = energyLevel;
            Row = row;
            Col = col;
        }

        public int EnergyLevel { get; set; }
        public int Row { get; }
        public int Col { get; }
        public bool HasFlashed { get; set; }
    }
}

