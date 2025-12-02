namespace AoC2016;

public class Day10
{
    public static (List<ValueInstruction> valueInstructions, List<BotInstruction> botInstructions) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var valueInstructions = new List<ValueInstruction>();
        var botInstructions = new List<BotInstruction>();
        
        foreach (var line in lines)
        {
            if (line.StartsWith("value"))
            {
                // value 5 goes to bot 2
                var parts = line.Split(' ');
                var value = int.Parse(parts[1]);
                var bot = int.Parse(parts[5]);
                valueInstructions.Add(new ValueInstruction(value, bot));
            }
            else
            {
                // bot 2 gives low to bot 1 and high to bot 0
                // bot 1 gives low to output 1 and high to bot 0
                var parts = line.Split(" ");
                var bot = int.Parse(parts[1]);
                var lowIsBot = parts[5] == "bot";
                var lowTarget = int.Parse(parts[6]);
                var highIsBot = parts[10] == "bot";
                var highTarget = int.Parse(parts[11]);
                
                botInstructions.Add(new BotInstruction(bot, lowIsBot, lowTarget, highIsBot, highTarget));
            }
        }
        
        return (valueInstructions, botInstructions);
    }

    [Solveable("2016/Puzzles/Day10.txt", "Day 10 part 1", 10)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (valueInstructions, botInstructions) = ParseInput(filename);
        
        // For test: bot comparing 5 and 2
        // For real: bot comparing 61 and 17
        int targetLow = filename.Contains("test") ? 2 : 17;
        int targetHigh = filename.Contains("test") ? 5 : 61;
        
        var (comparingBot, _) = SimulateBots(valueInstructions, botInstructions, targetLow, targetHigh);
        
        return new SolutionResult(comparingBot?.ToString() ?? "");
    }

    [Solveable("2016/Puzzles/Day10.txt", "Day 10 part 2", 10)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (valueInstructions, botInstructions) = ParseInput(filename);
        
        var (_, outputs) = SimulateBots(valueInstructions, botInstructions);
        
        if (outputs.ContainsKey(0) && outputs.ContainsKey(1) && outputs.ContainsKey(2))
        {
            var result = outputs[0] * outputs[1] * outputs[2];
            return new SolutionResult(result.ToString());
        }
        
        return new SolutionResult("");
    }

    private static (int? comparingBot, Dictionary<int, int> outputs) SimulateBots(
        List<ValueInstruction> valueInstructions, 
        List<BotInstruction> botInstructions,
        int? targetLow = null,
        int? targetHigh = null)
    {
        var bots = new Dictionary<int, List<int>>();
        var outputs = new Dictionary<int, int>();
        var instructions = botInstructions.ToDictionary(bi => bi.BotId);
        int? comparingBot = null;
        
        // Initialize bots with starting values
        foreach (var valueInst in valueInstructions)
        {
            if (!bots.ContainsKey(valueInst.BotId))
            {
                bots[valueInst.BotId] = new List<int>();
            }
            bots[valueInst.BotId].Add(valueInst.Value);
        }
        
        // Process until no bot has 2 chips
        bool changed = true;
        while (changed)
        {
            changed = false;
            
            // Find a bot with 2 chips
            foreach (var botId in bots.Keys.ToList())
            {
                if (bots[botId].Count == 2)
                {
                    var chips = bots[botId].OrderBy(x => x).ToList();
                    var low = chips[0];
                    var high = chips[1];
                    
                    // Check if this bot is comparing the target values
                    if (targetLow.HasValue && targetHigh.HasValue && 
                        low == targetLow.Value && high == targetHigh.Value)
                    {
                        comparingBot = botId;
                    }
                    
                    // Clear this bot's chips
                    bots[botId].Clear();
                    
                    // Get the instruction for this bot
                    if (instructions.TryGetValue(botId, out var instruction))
                    {
                        // Give low chip
                        if (instruction.LowIsBot)
                        {
                            if (!bots.ContainsKey(instruction.LowTarget))
                            {
                                bots[instruction.LowTarget] = new List<int>();
                            }
                            bots[instruction.LowTarget].Add(low);
                        }
                        else
                        {
                            outputs[instruction.LowTarget] = low;
                        }
                        
                        // Give high chip
                        if (instruction.HighIsBot)
                        {
                            if (!bots.ContainsKey(instruction.HighTarget))
                            {
                                bots[instruction.HighTarget] = new List<int>();
                            }
                            bots[instruction.HighTarget].Add(high);
                        }
                        else
                        {
                            outputs[instruction.HighTarget] = high;
                        }
                    }
                    
                    changed = true;
                    break; // Start over to process in order
                }
            }
        }
        
        return (comparingBot, outputs);
    }
}

public record ValueInstruction(int Value, int BotId);

public record BotInstruction(int BotId, bool LowIsBot, int LowTarget, bool HighIsBot, int HighTarget);