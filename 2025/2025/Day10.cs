namespace AoC2025;

using Google.OrTools.Sat;

public class Day10
{
    public static List<Machine> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var machines = new List<Machine>();
        foreach (var line in lines)
        {
            var inputs = line.Split(' ');
            var lights = inputs[0].Trim('[', ']').ToCharArray();
            var buttonGroups = new List<List<int>>();
            var joltageGroups = new List<List<int>>();
            for (var i = 1; i < inputs.Length; i++)
            {
                if (inputs[i].StartsWith("("))
                {
                    var buttonStr = inputs[i].Trim('(', ')');
                    if (!string.IsNullOrEmpty(buttonStr))
                    {
                        buttonGroups.Add(buttonStr.Split(',').Select(int.Parse).ToList());
                    }
                }
                else if (inputs[i].StartsWith("{"))
                {
                    var joltageStr = inputs[i].Trim('{', '}');
                    if (!string.IsNullOrEmpty(joltageStr))
                    {
                        var joltages = joltageStr.Split(',').Select(int.Parse).ToList();
                        joltageGroups.Add(joltages);
                    }
                }
            }
            machines.Add(new Machine(lights, buttonGroups, joltageGroups));
        }
        return machines;
    }

    [Solveable("2025/Puzzles/Day10.txt", "Day 10 part 1", 10)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var machines = ParseInput(filename);
        var totalPresses = 0;

        foreach (var machine in machines)
        {
            var minPresses = FindMinimumPresses(machine, printer);
            if (minPresses == -1)
            {
                return new SolutionResult("Error: No solution found");
            }
            totalPresses += minPresses;
        }

        return new SolutionResult(totalPresses.ToString());
    }

    [Solveable("2025/Puzzles/Day10.txt", "Day 10 part 2", 10, true)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var machines = ParseInput(filename);
        var totalPresses = 0L;

        foreach (var machine in machines)
        {
            var minPresses = SolveWithORTools(machine, printer);
            if (minPresses == -1)
            {
                return new SolutionResult("Error: No solution found");
            }
            totalPresses += minPresses;
        }

        return new SolutionResult(totalPresses.ToString());
    }

    private static int FindMinimumPresses(Machine machine, IPrinter printer)
    {
        var target = new string(machine.Lights);
        var numLights = machine.Lights.Length;
        var initialState = new string('.', numLights);

        if (initialState == target)
        {
            return 0;
        }

        var queue = new Queue<(string state, int presses)>();
        var visited = new HashSet<string>();

        queue.Enqueue((initialState, 0));
        visited.Add(initialState);

        var maxDepth = 20;

        while (queue.Count > 0)
        {
            var (currentState, presses) = queue.Dequeue();
            
            if (presses >= maxDepth)
            {
                continue;
            }

            // Try pressing each button
            for (var i = 0; i < machine.Buttons.Count; i++)
            {
                var newState = PressButton(currentState, machine.Buttons[i]);
                if (newState == target)
                {
                    return presses + 1;
                }

                if (!visited.Contains(newState))
                {
                    visited.Add(newState);
                    queue.Enqueue((newState, presses + 1));
                }
            }
        }

        return -1;
    }

    private static long SolveWithORTools(Machine machine, IPrinter printer)
    {
        var target = machine.Joltages[0];
        var buttons = machine.Buttons;
        
        // Determine number of counters from the maximum index referenced in buttons
        var maxCounterIndex = buttons.SelectMany(b => b).Max();
        var numCounters = maxCounterIndex + 1;
        var numButtons = buttons.Count;
        // Create the CP-SAT model
        var model = new CpModel();

        // Create variables for how many times each button is pressed
        var maxPresses = target.Sum();
        var buttonPresses = new IntVar[numButtons];
        for (var i = 0; i < numButtons; i++)
        {
            buttonPresses[i] = model.NewIntVar(0, maxPresses, $"button_{i}");
        }

        // Add constraints: for each counter, sum of (button_presses * button_affects_counter) == target
        for (var counterIdx = 0; counterIdx < numCounters; counterIdx++)
        {
            var terms = new List<IntVar>();
            var coeffs = new List<int>();

            for (var buttonIdx = 0; buttonIdx < numButtons; buttonIdx++)
            {
                if (buttons[buttonIdx].Contains(counterIdx))
                {
                    terms.Add(buttonPresses[buttonIdx]);
                    coeffs.Add(1);
                }
            }

            // Get the target for this counter (or 0 if not specified)
            var targetValue = counterIdx < target.Count ? target[counterIdx] : 0;

            if (terms.Count > 0)
            {
                model.Add(LinearExpr.WeightedSum(terms.ToArray(), coeffs.ToArray()) == targetValue);
            }
            else
            {
                if (targetValue != 0)
                {
                    return -1;
                }
            }
        }

        // Objective: minimize total button presses
        model.Minimize(LinearExpr.Sum(buttonPresses));

        // Solve
        var solver = new CpSolver();
        var status = solver.Solve(model);

        if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
        {
            var totalPresses = 0L;
            for (var i = 0; i < numButtons; i++)
            {
                var presses = solver.Value(buttonPresses[i]);
                totalPresses += presses;
            }
            // Verify the solution
            var resultCounters = new int[numCounters];
            for (var i = 0; i < numButtons; i++)
            {
                var presses = (int)solver.Value(buttonPresses[i]);
                foreach (var counterIdx in buttons[i])
                {
                    if (counterIdx >= 0 && counterIdx < numCounters)
                    {
                        resultCounters[counterIdx] += presses;
                    }
                }
            }
            return totalPresses;
        }

        return -1;
    }

    private static string PressButton(string state, List<int> toggles)
    {
        var lights = state.ToCharArray();
        foreach (var index in toggles)
        {
            if (index >= 0 && index < lights.Length)
            {
                lights[index] = lights[index] == '.' ? '#' : '.';
            }
        }
        return new string(lights);
    }
}

public record Machine(char[] Lights, List<List<int>> Buttons, List<List<int>> Joltages);