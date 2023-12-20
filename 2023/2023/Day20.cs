namespace AoC2023;
public class Day20
{
    public static Dictionary<string, Module> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new Dictionary<string, Module>();
        var modules = new Dictionary<string, List<string>>();
        foreach (var line in lines)
        {
            var (module, outputs) = (line.Split("->")[0].Trim(), line.Split("->")[1]);
            modules.Add(module, outputs.Split(",").Select(_ => _.Trim()).ToList());
        }
        foreach (var (module, outputs) in modules)
        {
            var outputList = outputs.Select(_ => (_, Pulse.Low)).ToList();
            var inputList = modules.Where(_ => _.Value.Contains(module[1..])).Select(_ => (_.Key == "broadcaster" ? _.Key : _.Key[1..], Pulse.Low)).ToList();
            if (module == "broadcaster")
            {
                result.Add(module, new Module(ModuleType.Broadcaster, module, inputList, outputList));
            }
            else if (module.StartsWith("%"))
            {
                result.Add(module[1..], new Module(ModuleType.FlipFlop, module[1..], inputList, outputList));
            }
            else if (module.StartsWith("&"))
            {
                result.Add(module[1..], new Module(ModuleType.Conjuction, module[1..], inputList, outputList));
            }
            else
            {
                result.Add(module, new Module(ModuleType.Output, module, inputList, outputList));
            }
        }

        return result;
    }

    [Solveable("2023/Puzzles/Day20.txt", "Day 20 part 1", 20)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var modules = ParseInput(filename);
        var sentHigh = 0L;
        var sentLow = 0L;
        for (int i = 0; i < 1000; i++)
        {
            sentLow++;//Push the button
            var queue = new Queue<(string sender, string receiver, Pulse pulse)>();
            foreach (var output in modules["broadcaster"].Outputs)
            {
                queue.Enqueue(("broadcaster", output.module, Pulse.Low));
                sentLow++;
            }
            while (queue.Any())
            {
                var (sender, receiver, pulse) = queue.Dequeue();
                var senderModule = modules[sender];

                if (modules.TryGetValue(receiver, out Module? module))
                {
                    var toSend = module.HandlePulse(senderModule, pulse);
                    if (toSend != Pulse.None)
                    {
                        foreach (var output in module.Outputs)
                        {
                            queue.Enqueue((module.Name, output.module, toSend));
                            if (toSend == Pulse.High)
                            {
                                sentHigh++;
                            }
                            else
                            {
                                sentLow++;
                            }
                        }
                    }

                }
            }
        }

        return new SolutionResult((sentHigh * sentLow).ToString());
    }

    [Solveable("2023/Puzzles/Day20.txt", "Day 20 part 2", 20)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var modules = ParseInput(filename);
        var isDone = false;
        var counter = 1L;
        var st = 0L;
        var tn = 0L;
        var hh = 0L;
        var dt = 0L;
        while(!isDone)
        {
            var queue = new Queue<(string sender, string receiver, Pulse pulse)>();
            foreach (var output in modules["broadcaster"].Outputs)
            {
                queue.Enqueue(("broadcaster", output.module, Pulse.Low));
            }
            while (queue.Any())
            {
                var (sender, receiver, pulse) = queue.Dequeue();
                var senderModule = modules[sender];

                if (modules.TryGetValue(receiver, out Module? module))
                {
                    var toSend = module.HandlePulse(senderModule, pulse);
                    if (toSend != Pulse.None)
                    {
                        foreach (var output in module.Outputs)
                        {
                            queue.Enqueue((module.Name, output.module, toSend));
                            if(output.module == "rx" && counter % 1000000 == 0)
                            {
                                printer.Print($"Pulse: {toSend} counter {counter}");
                                if(toSend == Pulse.Low)
                                {
                                    isDone = true;
                                    break;
                                }
                            }
                            if (output.module == "st" && toSend == Pulse.Low)
                            {
                                st = counter;
                            }
                            if (output.module == "tn" && toSend == Pulse.Low)
                            {
                                tn = counter;
                            }
                            if (output.module == "hh" && toSend == Pulse.Low)
                            {
                                hh = counter;
                            }
                            if (output.module == "dt" && toSend == Pulse.Low)
                            {
                                dt = counter;
                            }
                            if (st > 0 && tn > 0 && hh > 0 && dt > 0)
                            {
                                isDone = true;
                                break;
                            }
                        }
                    }

                }
            }
            if (!isDone)
            {
                counter++;
            }
        }
        return new SolutionResult(Helpers.CalculateLCM([st, tn, hh, dt]).ToString());
    }

    public class Module(ModuleType type, string name, List<(string module, Pulse last)> inputs, List<(string module, Pulse last)> outputs)
    {
        public string Name { get; set; } = name;
        public ModuleType Type { get; set; } = type;
        public List<(string module, Pulse last)> Outputs { get; } = outputs;
        public List<(string module, Pulse last)> Inputs { get; } = inputs;
        public bool IsOn { get; set; }
        public Pulse HandlePulse(Module senderModule, Pulse pulse)
        {
            switch (Type)
            {
                case ModuleType.FlipFlop:
                    if (pulse == Pulse.Low)
                    {
                        IsOn = !IsOn;
                        return IsOn ? Pulse.High : Pulse.Low;
                    }
                    return Pulse.None;
                case ModuleType.Conjuction:
                    var input = Inputs.First(_ => _.module == senderModule.Name);
                    Inputs.Remove(input);
                    Inputs.Add((input.module, pulse));
                    if (Inputs.All(_ => _.last == Pulse.High))
                    {
                        return Pulse.Low;
                    }
                    else
                    {
                        return Pulse.High;
                    }

                case ModuleType.Output:
                    return Pulse.None;
                default:
                    return Pulse.None;
            }
        }

    }

    public enum ModuleType
    {
        Broadcaster,
        FlipFlop,
        Conjuction,
        Output

    }

    public enum Pulse
    {
        Low,
        High,
        None
    }
}