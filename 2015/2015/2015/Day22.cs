namespace AoC2015;

public class Day22
{
    public static Boss ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var hitPoints = int.Parse(lines[0].Split(':').Last().Trim());
        var damage = int.Parse(lines[1].Split(':').Last().Trim());
        return new Boss(hitPoints, 0, damage);
    }

    [Solveable("2015/Puzzles/Day22.txt", "Day 22 part 1", 22)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var boss = ParseInput(filename);
        var playerHp = filename.Contains("test") ? 10 : 50;
        var playerMana = filename.Contains("test") ? 250 : 500;
        
        var minMana = FindMinManaToWin(playerHp, playerMana, boss, hardMode: false);
        return new SolutionResult(minMana >= 0 ? minMana.ToString() : "");
    }

    [Solveable("2015/Puzzles/Day22.txt", "Day 22 part 2", 22)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var boss = ParseInput(filename);
        var playerHp = 50;
        var playerMana = 500;
        
        var minMana = FindMinManaToWin(playerHp, playerMana, boss, hardMode: true);
        return new SolutionResult(minMana >= 0 ? minMana.ToString() : "");
    }

    private static int FindMinManaToWin(int playerHp, int playerMana, Boss boss, bool hardMode)
    {
        var minManaSpent = int.MaxValue;
        var queue = new PriorityQueue<GameState, int>();
        
        var initialState = new GameState(
            PlayerHp: playerHp,
            PlayerMana: playerMana,
            BossHp: boss.HitPoints,
            BossDamage: boss.Damage,
            ManaSpent: 0,
            ShieldTimer: 0,
            PoisonTimer: 0,
            RechargeTimer: 0,
            IsPlayerTurn: true
        );
        
        queue.Enqueue(initialState, 0);
        
        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            
            if (state.ManaSpent >= minManaSpent)
            {
                continue;
            }
            
            if (state.IsPlayerTurn)
            {
                var currentHp = hardMode ? state.PlayerHp - 1 : state.PlayerHp;
                if (currentHp <= 0)
                {
                    continue;
                }
                
                var afterEffects = ApplyEffects(state with { PlayerHp = currentHp });
                
                if (afterEffects.BossHp <= 0)
                {
                    minManaSpent = Math.Min(minManaSpent, afterEffects.ManaSpent);
                    continue;
                }
                
                foreach (var spell in Spells)
                {
                    var newState = TryCastSpell(afterEffects, spell);
                    if (newState != null)
                    {
                        if (newState.BossHp <= 0)
                        {
                            minManaSpent = Math.Min(minManaSpent, newState.ManaSpent);
                        }
                        else
                        {
                            queue.Enqueue(newState with { IsPlayerTurn = false }, newState.ManaSpent);
                        }
                    }
                }
            }
            else
            {
                var afterEffects = ApplyEffects(state);
                
                if (afterEffects.BossHp <= 0)
                {
                    minManaSpent = Math.Min(minManaSpent, afterEffects.ManaSpent);
                    continue;
                }
                
                var armor = afterEffects.ShieldTimer > 0 ? 7 : 0;
                var damage = Math.Max(1, afterEffects.BossDamage - armor);
                var newHp = afterEffects.PlayerHp - damage;
                
                if (newHp > 0)
                {
                    var newState = afterEffects with { PlayerHp = newHp, IsPlayerTurn = true };
                    queue.Enqueue(newState, newState.ManaSpent);
                }
            }
        }
        
        return minManaSpent == int.MaxValue ? -1 : minManaSpent;
    }

    private static GameState ApplyEffects(GameState state)
    {
        var newState = state;
        
        if (newState.ShieldTimer > 0)
        {
            newState = newState with { ShieldTimer = newState.ShieldTimer - 1 };
        }
        
        if (newState.PoisonTimer > 0)
        {
            newState = newState with 
            { 
                BossHp = newState.BossHp - 3,
                PoisonTimer = newState.PoisonTimer - 1 
            };
        }
        
        if (newState.RechargeTimer > 0)
        {
            newState = newState with 
            { 
                PlayerMana = newState.PlayerMana + 101,
                RechargeTimer = newState.RechargeTimer - 1 
            };
        }
        
        return newState;
    }

    private static GameState? TryCastSpell(GameState state, SpellData spell)
    {
        if (state.PlayerMana < spell.ManaCost)
        {
            return null;
        }
        
        if (spell.Name == "Shield" && state.ShieldTimer > 0)
        {
            return null;
        }
        if (spell.Name == "Poison" && state.PoisonTimer > 0)
        {
            return null;
        }
        if (spell.Name == "Recharge" && state.RechargeTimer > 0)
        {
            return null;
        }
        
        var newState = state with 
        { 
            PlayerMana = state.PlayerMana - spell.ManaCost,
            ManaSpent = state.ManaSpent + spell.ManaCost
        };
        
        switch (spell.Name)
        {
            case "Magic Missile":
                newState = newState with { BossHp = newState.BossHp - 4 };
                break;
            case "Drain":
                newState = newState with 
                { 
                    BossHp = newState.BossHp - 2,
                    PlayerHp = newState.PlayerHp + 2
                };
                break;
            case "Shield":
                newState = newState with { ShieldTimer = 6 };
                break;
            case "Poison":
                newState = newState with { PoisonTimer = 6 };
                break;
            case "Recharge":
                newState = newState with { RechargeTimer = 5 };
                break;
        }
        
        return newState;
    }

    private record GameState(
        int PlayerHp,
        int PlayerMana,
        int BossHp,
        int BossDamage,
        int ManaSpent,
        int ShieldTimer,
        int PoisonTimer,
        int RechargeTimer,
        bool IsPlayerTurn
    );

    private record SpellData(string Name, int ManaCost);

    private static readonly List<SpellData> Spells = new()
    {
        new SpellData("Magic Missile", 53),
        new SpellData("Drain", 73),
        new SpellData("Shield", 113),
        new SpellData("Poison", 173),
        new SpellData("Recharge", 229),
    };
}