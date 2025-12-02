namespace AoC2016;
public class Day11
{
    public static State ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var elements = new Dictionary<string, (int GenFloor, int ChipFloor)>(StringComparer.OrdinalIgnoreCase);

        for (int i = 0; i < lines.Length; i++)
        {
            int floor = i + 1;
            var line = lines[i].ToLowerInvariant();

            foreach (Match m in Regex.Matches(line, @"\b([a-z]+)\s+generator\b", RegexOptions.IgnoreCase))
            {
                var name = m.Groups[1].Value;
                if (!elements.ContainsKey(name))
                {
                    elements[name] = (0, 0);
                }

                var current = elements[name];
                current.GenFloor = floor;
                elements[name] = current;
            }

            foreach (Match m in Regex.Matches(line, @"\b([a-z]+)-compatible microchip\b", RegexOptions.IgnoreCase))
            {
                var name = m.Groups[1].Value;
                if (!elements.ContainsKey(name))
                {
                    elements[name] = (0, 0);
                }

                var current = elements[name];
                current.ChipFloor = floor;
                elements[name] = current;
            }
        }

        // Order by key so parsing is deterministic, then create pairs
        var pairs = elements
            .OrderBy(kv => kv.Key, StringComparer.OrdinalIgnoreCase)
            .Select(kv => new ItemPair(kv.Value.GenFloor, kv.Value.ChipFloor))
            .ToArray();

        var initial = new State(1, pairs);
        return initial;
    }

    [Solveable("2016/Puzzles/Day11.txt", "Day 11 part 1", 11)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var initial = ParseInput(filename);
        var steps = SolveBfs(initial);
        return new SolutionResult(steps.ToString());
    }

    [Solveable("2016/Puzzles/Day11.txt", "Day 11 part 2", 11)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var initial = ParseInput(filename);

        var list = initial.Pairs.ToList();
        list.Add(new ItemPair(1, 1));
        list.Add(new ItemPair(1, 1));
        initial = new State(initial.Elevator, list.ToArray());

        var steps = SolveBfs(initial);
        return new SolutionResult(steps.ToString());
    }

    private static int SolveBfs(State initial)
    {
        var seen = new HashSet<string>();
        var queue = new Queue<(State state, int steps)>();
        var startKey = CanonicalKey(initial);
        seen.Add(startKey);
        queue.Enqueue((initial, 0));

        while (queue.Count > 0)
        {
            var (state, steps) = queue.Dequeue();

            if (AllOnTop(state))
            {
                return steps;
            }

            var itemsOnFloor = GetItemsOnFloor(state, state.Elevator);

            // If no items to move, skip
            if (itemsOnFloor.Count == 0)
            {
                continue;
            }

            // Consider directions: up and down
            var directions = new List<int>();
            if (state.Elevator < 4)
            {
                directions.Add(state.Elevator + 1);
            }

            if (state.Elevator > 1)
            {
                // Allow moving down as well (pruning removed for correctness)
                directions.Add(state.Elevator - 1);
            }

            // Generate combinations of 1 and 2 items
            var combinations = new List<List<ItemRef>>();

            // single items
            foreach (var item in itemsOnFloor)
            {
                combinations.Add(new List<ItemRef> { item });
            }

            // pairs
            for (int i = 0; i < itemsOnFloor.Count; i++)
            {
                for (int j = i + 1; j < itemsOnFloor.Count; j++)
                {
                    combinations.Add(new List<ItemRef> { itemsOnFloor[i], itemsOnFloor[j] });
                }
            }

            // Try each direction and combination
            foreach (var targetFloor in directions)
            {
                foreach (var combo in combinations)
                {
                    var next = MoveItems(state, combo, targetFloor);

                    if (!IsValid(next))
                    {
                        continue;
                    }

                    var key = CanonicalKey(next);
                    if (!seen.Contains(key))
                    {
                        seen.Add(key);
                        queue.Enqueue((next, steps + 1));
                    }
                }
            }
        }

        // No solution found
        return -1;
    }

    private static State MoveItems(State state, List<ItemRef> items, int targetFloor)
    {
        var newPairs = state.Pairs.Select(p => new ItemPair(p.GenFloor, p.ChipFloor)).ToArray();
        int elevator = targetFloor;

        foreach (var item in items)
        {
            if (item.IsGenerator)
            {
                var p = newPairs[item.Index];
                newPairs[item.Index] = new ItemPair(targetFloor, p.ChipFloor);
            }
            else
            {
                var p = newPairs[item.Index];
                newPairs[item.Index] = new ItemPair(p.GenFloor, targetFloor);
            }
        }

        return new State(elevator, newPairs);
    }

    private static bool AllOnTop(State state)
    {
        for (int i = 0; i < state.Pairs.Length; i++)
        {
            if (state.Pairs[i].GenFloor != 4 || state.Pairs[i].ChipFloor != 4)
            {
                return false;
            }
        }

        return true;
    }

    private static List<ItemRef> GetItemsOnFloor(State state, int floor)
    {
        var list = new List<ItemRef>();
        for (int i = 0; i < state.Pairs.Length; i++)
        {
            if (state.Pairs[i].GenFloor == floor)
            {
                list.Add(new ItemRef(i, true));
            }

            if (state.Pairs[i].ChipFloor == floor)
            {
                list.Add(new ItemRef(i, false));
            }
        }

        return list;
    }

    private static bool IsValid(State state)
    {
        // For each microchip: if it is not with its generator and there is any generator on the same floor => invalid.
        for (int i = 0; i < state.Pairs.Length; i++)
        {
            var chipFloor = state.Pairs[i].ChipFloor;
            var genFloor = state.Pairs[i].GenFloor;

            if (chipFloor != genFloor)
            {
                // if there is any generator on chipFloor, it's unsafe
                bool anyGeneratorThere = state.Pairs.Any(p => p.GenFloor == chipFloor);
                if (anyGeneratorThere)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static string CanonicalKey(State state)
    {
        // Normalize by sorting pairs so that label permutations are collapsed.
        var sorted = state.Pairs
            .Select(p => (p.GenFloor, p.ChipFloor))
            .OrderBy(t => t.GenFloor)
            .ThenBy(t => t.ChipFloor)
            .ToArray();

        var sb = new StringBuilder();
        sb.Append(state.Elevator);
        sb.Append('|');

        for (int i = 0; i < sorted.Length; i++)
        {
            sb.Append(sorted[i].GenFloor);
            sb.Append(',');
            sb.Append(sorted[i].ChipFloor);
            if (i < sorted.Length - 1)
            {
                sb.Append(';');
            }
        }

        return sb.ToString();
    }

    // small helper to identify items
    private readonly record struct ItemRef(int Index, bool IsGenerator);
}

// Public types used by the solver
public readonly record struct ItemPair(int GenFloor, int ChipFloor);

public readonly record struct State(int Elevator, ItemPair[] Pairs);