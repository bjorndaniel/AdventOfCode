namespace AoC2015;

public class Day21
{
    public static Boss ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var hitPoints = int.Parse(lines[0].Split(':').Last().Trim());
        var damage = int.Parse(lines[1].Split(':').Last().Trim());
        var armor = int.Parse(lines[2].Split(':').Last().Trim());
        return new Boss(hitPoints, armor, damage);
    }

    [Solveable("2015/Puzzles/Day21.txt", "Day 21 part 1", 21)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var boss = ParseInput(filename);
        var player = new Player(100, 0, 0);
        var minCost = DFS_Win(player.HitPoints, boss);
        return new SolutionResult(minCost >= 0 ? minCost.ToString() : "");
    }

    [Solveable("2015/Puzzles/Day21.txt", "Day 21 part 2", 21)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var boss = ParseInput(filename);
        var player = new Player(100, 0, 0);
        var maxCostLose = FindMaxCostLose_DFS(player.HitPoints, boss);
        return new SolutionResult(maxCostLose >= 0 ? maxCostLose.ToString() : "");
    }

    private static bool PlayerWins(int playerHp, int playerDamage, int playerArmor, Boss boss)
    {
        var playerDeal = Math.Max(1, playerDamage - boss.Armor);
        var bossDeal = Math.Max(1, boss.Damage - playerArmor);
        var turnsToKillBoss = (boss.HitPoints + playerDeal - 1) / playerDeal;
        var turnsToKillPlayer = (playerHp + bossDeal - 1) / bossDeal;
        return turnsToKillBoss <= turnsToKillPlayer;
    }

    // DFS search that tries weapons -> armor ->0..2 rings and prunes by current best cost (for min win)
    private static int DFS_Win(int playerHp, Boss boss)
    {
        var best = int.MaxValue;

        foreach (var w in Weapons)
        {
            var baseCost = w.Cost;
            var baseDamage = w.Damage;
            var baseArmor = w.Armor;
            var armOptions = new List<Armory?> { null };
            armOptions.AddRange(Armories.Cast<Armory?>());

            foreach (var a in armOptions)
            {
                var cost0 = baseCost + (a?.Cost ?? 0);
                var dmg0 = baseDamage + (a?.Damage ?? 0);
                var arm0 = baseArmor + (a?.Armor ?? 0);
                if (cost0 >= best)
                {
                    continue;
                }

                if (PlayerWins(playerHp, dmg0, arm0, boss))
                {
                    best = Math.Min(best, cost0);
                    continue;
                }
                Recurse(0, 0, cost0, dmg0, arm0);
            }
        }

        return best == int.MaxValue ? -1 : best;

        void Recurse(int startIndex, int ringsChosen, int curCost, int curDmg, int curArm)
        {
            if (curCost >= best) return; // prune
            if (PlayerWins(playerHp, curDmg, curArm, boss))
            {
                best = Math.Min(best, curCost);
                return;
            }
            if (ringsChosen == 2)
            {
                return;
            }

            for (int i = startIndex; i < Rings.Count; i++)
            {
                var r = Rings[i];
                Recurse(i + 1, ringsChosen + 1, curCost + r.Cost, curDmg + r.Damage, curArm + r.Armor);
            }
        }
    }

    // DFS that finds the maximum cost that still results in a loss
    private static int FindMaxCostLose_DFS(int playerHp, Boss boss)
    {
        var bestLose = -1;

        foreach (var w in Weapons)
        {
            var baseCost = w.Cost;
            var baseDamage = w.Damage;
            var baseArmor = w.Armor;
            var armOptions = new List<Armory?> { null };
            armOptions.AddRange(Armories.Cast<Armory?>());

            foreach (var a in armOptions)
            {
                var cost0 = baseCost + (a?.Cost ?? 0);
                var dmg0 = baseDamage + (a?.Damage ?? 0);
                var arm0 = baseArmor + (a?.Armor ?? 0);

                // Check current combination (0 rings)
                if (!PlayerWins(playerHp, dmg0, arm0, boss))
                {
                    bestLose = Math.Max(bestLose, cost0);
                }

                // Explore adding up to2 distinct rings to potentially increase cost
                Recurse(0, 0, cost0, dmg0, arm0);
            }
        }

        return bestLose;

        void Recurse(int startIndex, int ringsChosen, int curCost, int curDmg, int curArm)
        {
            if (ringsChosen == 2) return;

            for (int i = startIndex; i < Rings.Count; i++)
            {
                var r = Rings[i];
                var nCost = curCost + r.Cost;
                var nDmg = curDmg + r.Damage;
                var nArm = curArm + r.Armor;

                if (!PlayerWins(playerHp, nDmg, nArm, boss))
                {
                    bestLose = Math.Max(bestLose, nCost);
                }

                Recurse(i + 1, ringsChosen + 1, nCost, nDmg, nArm);
            }
        }
    }

    public record Boss(int HitPoints, int Armor, int Damage);

    public record Player(int HitPoints, int Armor, int Damage);

    public record Weapon(WeaponType Type, int Cost, int Damage, int Armor);

    public record Armory(ArmorType Type, int Cost, int Damage, int Armor);

    public record Ring(RingType Type, int Cost, int Damage, int Armor);

    public enum WeaponType
    {
        Dagger,
        Shortsword,
        Warhammer,
        Longsword,
        Greataxe
    }

    public enum ArmorType
    {
        Leather,
        Chainmail,
        Splintmail,
        Bandedmail,
        Platemail
    }

    public enum RingType
    {
        DamagePlus1,
        DamagePlus2,
        DamagePlus3,
        DefensePlus1,
        DefensePlus2,
        DefensePlus3
    }

    private static List<Weapon> Weapons = new()
    {
        new Weapon(WeaponType.Dagger, 8, 4, 0),
        new Weapon(WeaponType.Shortsword, 10, 5, 0),
        new Weapon(WeaponType.Warhammer, 25, 6, 0),
        new Weapon(WeaponType.Longsword, 40, 7, 0),
        new Weapon(WeaponType.Greataxe, 74, 8, 0)
    };

    private static List<Armory> Armories = new()
    {
        new Armory(ArmorType.Leather, 13, 0, 1),
        new Armory(ArmorType.Chainmail, 31, 0, 2),
        new Armory(ArmorType.Splintmail, 53, 0, 3),
        new Armory(ArmorType.Bandedmail, 75, 0, 4),
        new Armory(ArmorType.Platemail, 102, 0, 5)
    };

    private static List<Ring> Rings = new()
    {
        new Ring(RingType.DamagePlus1, 25, 1, 0),
        new Ring(RingType.DamagePlus2, 50, 2, 0),
        new Ring(RingType.DamagePlus3, 100, 3, 0),
        new Ring(RingType.DefensePlus1, 20, 0, 1),
        new Ring(RingType.DefensePlus2, 40, 0, 2),
        new Ring(RingType.DefensePlus3, 80, 0, 3)
    };
}