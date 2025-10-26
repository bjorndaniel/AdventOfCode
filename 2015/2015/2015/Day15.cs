namespace AoC2015;
public class Day15
{
    public static List<Ingredient> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var ingredients = new List<Ingredient>();
        foreach (var line in lines)
        {
            var ingredientParts = line.Split(": ");
            var name = ingredientParts[0];
            var attributes = ingredientParts[1].Split(", ").Select(a => a.Split(" ")[1]).Select(int.Parse).ToArray();
            ingredients.Add(new Ingredient(name, attributes[0], attributes[1], attributes[2], attributes[3], attributes[4]));
        }
        return ingredients;
    }

    [Solveable("2015/Puzzles/Day15.txt", "Day 15 part 1", 15)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var ingredients = ParseInput(filename);
        var maxScore = MaxScoreForIngredients(ingredients, 100);
        return new SolutionResult(maxScore.ToString());
    }

    [Solveable("2015/Puzzles/Day15.txt", "Day 15 part 2", 15)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var ingredients = ParseInput(filename);
        var maxScore = MaxScoreForIngredients(ingredients, 100, 500);
        return new SolutionResult(maxScore.ToString());
    }

    private static long MaxScoreForIngredients(List<Ingredient> ingredients, int totalTeaspoons, int maxCalories = 0)
    {
        var maxScore = 0L;
        var counts = new int[ingredients.Count];

        void Recurse(int index, int remaining)
        {
            if (index == ingredients.Count - 1)
            {
                counts[index] = remaining;
                var capacity = 0L;
                var durability = 0L;
                var flavor = 0L;
                var texture = 0L;
                var calories = 0L;
                for (var i = 0; i < ingredients.Count; i++)
                {
                    var amt = counts[i];
                    var ing = ingredients[i];
                    capacity += (long)amt * ing.Capacity;
                    durability += (long)amt * ing.Durability;
                    flavor += (long)amt * ing.Flavor;
                    texture += (long)amt * ing.Texture;
                    calories += (long)amt * ing.Calories;
                }
                if (capacity < 0)
                {
                    capacity = 0;
                }
                if (durability < 0)
                {
                    durability = 0;
                }
                if (flavor < 0)
                {
                    flavor = 0;
                }
                if (texture < 0)
                {
                    texture = 0;
                }

                if (maxCalories > 0 && calories != maxCalories)
                {
                    return;
                }

                var score = capacity * durability * flavor * texture;
                if (score > maxScore)
                {
                    maxScore = score;
                }
                return;
            }

            for (var i = 0; i <= remaining; i++)
            {
                counts[index] = i;
                Recurse(index + 1, remaining - i);
            }
        }

        Recurse(0, totalTeaspoons);
        return maxScore;
    }

    public record Ingredient(string Name, int Capacity, int Durability, int Flavor, int Texture, int Calories);
}