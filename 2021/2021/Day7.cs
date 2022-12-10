namespace Advent2021;
public class Day7
{
    public static int CalculateFuelConsumption(string filename)
    {
        var positions = GetPositions(filename);
        foreach(var position in positions)
        {
            var other = positions.Where(_ => _.Id != position.Id);
            position.TotalDistance = other.Sum(_ => Math.Abs(_.Position - position.Position));
        }
        return positions.Min(_ => _.TotalDistance);
    }

    private static List<Crab> GetPositions(string filename)
    {
        var input = File.ReadAllText(filename);
        var crabs = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(_ => new Crab(int.Parse(_))).ToList();
        return crabs;
    }

    public static int CalculateFuelConsumption2(string filename)
    {
        var positions = GetPositions(filename);
        Dictionary<int, int> total = new Dictionary<int, int>();
        for(int i = 0; i < positions.Max(_ => _.Position); i++)
        {
            positions.ForEach(position =>
            {
                var distance = position.CalculateFuel(i);
                if (total.ContainsKey(i))
                {
                    total[i] += distance;
                }
                else
                {
                    total.Add(i, distance);
                }
            });
        }
        return total.Min(_ => _.Value);
    }

    public class Crab
    {
        public Crab(int position)
        {
            Position = position;
        }
        public Guid Id { get; } = Guid.NewGuid();
        public int Position { get; set; }   
        public int TotalDistance { get; set; }
        public Dictionary<int,int> Distances { get; set; }  = new Dictionary<int,int>();
        public int CalculateFuel(int target)
        {
            var distance = Math.Abs(Position - target);
            if(distance == 0)
            {
                return 0;
            }
            var fuel = 0;
            for(int i = 1; i < distance + 1; i++)
            {
                fuel += i;
            }
            return fuel;
        }
    }
}

