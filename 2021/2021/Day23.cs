//Finally gave up and copied a solution from 
//https://github.com/premun/advent-of-code/tree/main/src/2021/23
//I'm not sure I understand the logic, but it works.
namespace Advent2021;
public class Day23
{
    public static int Solve(AmphipodWorld startWorld, AmphipodWorld finishedWorld)
    {
        Console.WriteLine("Searching from");
        Console.WriteLine(startWorld);

        var queue = new PriorityQueue<AmphipodWorld, int>();
        queue.Enqueue(startWorld, 0);

        var lowestEnergies = new Dictionary<int, int>
        {
            [startWorld.GetHashCode()] = 0
        };

        var previousWorld = new Dictionary<int, (AmphipodWorld, int)>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var currentEnergy = lowestEnergies[current.GetHashCode()];

            foreach (var move in current.GetPossibleMoves())
            {
                var hash = move.World.GetHashCode();
                if (!lowestEnergies.TryGetValue(hash, out var neighbourEnergy))
                {
                    neighbourEnergy = int.MaxValue;
                }

                var newEnergy = currentEnergy + move.EnergyCost;
                if (newEnergy < neighbourEnergy)
                {
                    lowestEnergies[hash] = newEnergy;
                    queue.Enqueue(move.World, newEnergy);
                    previousWorld[hash] = (current, currentEnergy);
                }
            }
        }

        var currentHash = finishedWorld.GetHashCode();
        if (!lowestEnergies.TryGetValue(currentHash, out var result))
        {
            Console.WriteLine("Failed to find the moves to reach the desired state!");
            return -1;
        }

        var start = startWorld.GetHashCode();
        var moves = new List<(AmphipodWorld, int)>();

        while (currentHash != start)
        {
            moves.Add(previousWorld[currentHash]);
            currentHash = previousWorld[currentHash].Item1.GetHashCode();
        }

        // Print the winning sequence
        int i = 1;
        foreach (var w in moves.Reverse<(AmphipodWorld, int)>())
        {
            Console.WriteLine($"{i++}# - {w.Item2}:");
            Console.WriteLine(w.Item1);
        }

        Console.WriteLine(result);
        Console.WriteLine(finishedWorld);

        return result;

    }

}
public class AmphipodWorld
{
    private readonly Field[][] _map;
    private readonly int _sizeY;
    private readonly int _sizeX;

    private readonly Coor[] _rooms;

    public AmphipodWorld(string[] world) : this(world.Select(line => line.Select(field => field).ToArray()).ToArray())
    {
    }

    public AmphipodWorld(char[][] world)
    {
        _sizeY = world.Length;
        _sizeX = world.Select(line => line.Length).Max();

        _map = new Field[_sizeY][];

        for (int y = 0; y < _sizeY; y++)
        {
            _map[y] = new Field[_sizeX];

            for (int x = 0; x < _sizeX; x++)
            {
                char c = x >= world[y].Length ? ' ' : world[y][x];

                Field field;

                switch (c)
                {
                    case (char)0:
                    case ' ':
                        field = new EmptyField();
                        break;

                    case '#':
                        field = new WallField();
                        break;

                    case '.':
                        field = Neighbourhs(y, x).Any(n => world[n.Y][n.X] >= 'A' && world[n.Y][n.X] <= 'D')
                            ? new RoomDoor()
                            : new HallwayField();
                        break;

                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                        var roomsToLeft = Enumerable.Range(0, x - 1).Count(i => _map[y][i] is RoomField f);

                        field = roomsToLeft switch
                        {
                            0 => new RoomField('A', c),
                            1 => new RoomField('B', c),
                            2 => new RoomField('C', c),
                            3 => new RoomField('D', c),
                            _ => throw new Exception("Too many rooms detected"),
                        };

                        break;

                    default:
                        throw new Exception($"Cannot recognize field {c}");
                }

                _map[y][x] = field;
            }
        }

        _rooms = AllFields.Where(f => f.Field is RoomField).Select(f => f.Coor).ToArray();
    }

    public AmphipodWorld(Field[][] map, Coor[] rooms)
    {
        _map = map;
        _sizeY = map.Length;
        _sizeX = map[0].Length;
        _rooms = rooms;
    }

    public AmphipodWorld MoveAmphipod(Coor from, Coor to)
    {
        var newMap = new Field[_sizeY][];

        for (int y = 0; y < _sizeY; y++)
        {
            newMap[y] = new Field[_sizeX];

            for (int x = 0; x < _sizeX; x++)
            {
                if (y == from.Y && x == from.X)
                {
                    var fromField = (OccupyableField)_map[y][x];
                    newMap[y][x] = fromField with
                    {
                        Occupant = null
                    };
                }
                else if (y == to.Y && x == to.X)
                {
                    var toField = (OccupyableField)_map[y][x];
                    newMap[y][x] = toField with
                    {
                        Occupant = ((OccupyableField)_map[from.Y][from.X]).Occupant
                    };
                }
                else
                {
                    newMap[y][x] = _map[y][x];
                }
            }
        }

        return new AmphipodWorld(newMap, _rooms);
    }

    public bool IsFinished() => _rooms.All(coor => ((RoomField)Get(coor)).OccupantIsHome);

    public IEnumerable<(AmphipodWorld World, int EnergyCost)> GetPossibleMoves()
    {
        var amphipods = AllFields
            .Where(field => field.Field is OccupyableField f && f.IsOccupied)
            .Select(x => (Field: (OccupyableField)x.Field, x.Coor))
            /*.OrderBy(x => 'D' - x.Field.Occupant!.Value)*/;

        var result = new List<(AmphipodWorld World, int EnergyCost)>();

        foreach (var amphipod in amphipods)
        {
            var amphipodName = amphipod.Field.Occupant;

            // Starting in my room - does it make sense to move out?
            if (amphipod.Field is RoomField sourceRoom && sourceRoom.OccupantIsHome)
            {
                bool blockingSomeone = Enumerable.Range(amphipod.Coor.Y, _sizeY - amphipod.Coor.Y - 1)
                    .Select(y => Get(new Coor(Y: y, X: amphipod.Coor.X)))
                    .Any(f => f is RoomField roomField && !roomField.OccupantIsHome);

                if (!blockingSomeone)
                {
                    // All amphipods below (including) me are home
                    continue;
                }
            }

            foreach (var destination in GetReachableFields(amphipod.Coor))
            {
                var targetField = Get(destination.Coor);

                if (targetField is RoomDoor)
                {
                    // Cannot move right outside of a room
                    continue;
                }

                if (amphipod.Field is HallwayField)
                {
                    if (targetField is HallwayField)
                    {
                        // Can't move from hallway to hallway
                        continue;
                    }

                    if (targetField is RoomField rf && rf.Name != amphipodName)
                    {
                        // Can't go to other's room
                        continue;
                    }
                }

                if (targetField is RoomField targetRoom)
                {
                    if (targetRoom.Name != amphipodName)
                    {
                        // Can only move to his own room
                        continue;
                    }

                    if (amphipod.Field is RoomField sourcRoom && targetRoom.Name == sourcRoom.Name)
                    {
                        // No sense in moving around my room
                        continue;
                    }

                    if (_map[destination.Coor.Y + 1][destination.Coor.X] is RoomField fieldBelow && !fieldBelow.IsOccupied)
                    {
                        // No sense to block the room
                        continue;
                    }

                    if (_rooms.Select(r => (RoomField)Get(r)).Any(r => r.Name == targetRoom.Name && r.IsOccupied && !r.OccupantIsHome))
                    {
                        // Amphipods will never move into a room which contains no amphipods does not contain foreigners
                        continue;
                    }
                }

                var cost = destination.Distance * amphipodName switch
                {
                    'A' => 1,
                    'B' => 10,
                    'C' => 100,
                    'D' => 1000,
                    _ => throw new Exception($"Unrecognized amphipod '{amphipodName}'"),
                };

                result.Add((MoveAmphipod(amphipod.Coor, destination.Coor), cost));
            }
        }

        return result;
    }

    public Field Get(int Y, int X) => _map[Y][X];

    public Field Get(Coor coordinate) => _map[coordinate.Y][coordinate.X];

    public override string ToString() => new(
        Enumerable.Range(0, _sizeY).SelectMany(y =>
        Enumerable.Range(0, _sizeX).Select(x => _map[y][x].ToChar()).Append('\r').Append('\n'))
        .ToArray());

    public override int GetHashCode() => ToString().GetHashCode();

    public override bool Equals(object? obj)
    {
        if (base.Equals(obj))
        {
            return true;
        }

        if (obj is AmphipodWorld other)
        {
            return GetHashCode() == other.GetHashCode();
        }
        return false;
    }
    private IEnumerable<Coor> Neighbourhs(int y, int x) => Neighbourhs(new Coor(y, x));

    private IEnumerable<Coor> Neighbourhs(Coor coor) => new[]
    {
        coor + new Coor(0, 1),
        coor + new Coor(1, 0),
        coor + new Coor(-1, 0),
        coor + new Coor(0, -1),
    }.Where(n => n.Y < _sizeY && n.X < _sizeX && n.Y >= 0 && n.X >= 0);

    private IEnumerable<(Field Field, Coor Coor)> AllFields
        => _map.SelectMany((row, y) => row.Select((field, x) => (field, new Coor(y, x))));

    // Technically this is wrong - we don't calculate the distance correctly
    // We instead rely on the corridor being 1 char wide and so that DFS == BFS in this case
    private List<(Coor Coor, int Distance)> GetReachableFields(Coor from)
    {
        var visited = new List<(Coor, int)>();

        void GetReachableFieldsHelper(Coor f, int distance)
        {
            var newFreeFieldsAround = Neighbourhs(f)
                .Where(n => Get(n) is OccupyableField field && !field.IsOccupied && !visited.Any(m => m.Item1 == n));

            foreach (var field in newFreeFieldsAround)
            {
                visited.Add((field, distance));
                GetReachableFieldsHelper(field, distance + 1);
            }
        }

        GetReachableFieldsHelper(from, 1);

        return visited;
    }
}

public abstract record Field
{
    public abstract char ToChar();
}

public record WallField() : Field
{
    public override char ToChar() => '#';
}

public record EmptyField() : Field
{
    public override char ToChar() => ' ';
}

public record OccupyableField(char? Occupant) : Field
{
    [MemberNotNullWhen(true, "Occupant")]
    public bool IsOccupied => Occupant.HasValue;

    public override char ToChar() => IsOccupied ? Occupant.Value : '.';
}

public record HallwayField : OccupyableField
{
    public HallwayField(char? occupant = null) : base(occupant)
    {
    }
}

public record RoomDoor : OccupyableField
{
    public RoomDoor(char? occupant = null) : base(occupant)
    {
    }
}

public record RoomField : OccupyableField
{
    public RoomField(char name, char? occupant = null) : base(occupant)
    {
        Name = name;
    }

    public char Name { get; }

    public bool OccupantIsHome => Name == Occupant;
}
public record Coor(int Y, int X)
{
    public static Coor operator -(Coor me, Coor other) => new(Y: me.Y - other.Y, X: me.X - other.X);
    public static Coor operator +(Coor me, Coor other) => new(Y: me.Y + other.Y, X: me.X + other.X);
}