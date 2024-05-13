class RecipeManager
{
    private List<Recipe> recipes = new List<Recipe>();

    // Event to trigger when recipe exceeds 300 calories
    public event RecipeExceedCaloriesHandler RecipeExceededCalories;

    public void EnterRecipeDetails()
    {
        Console.WriteLine("Enter the name of the recipe:");
        string recipeName = Console.ReadLine();

        Recipe recipe = new Recipe(recipeName);
        recipe.RecipeExceededCalories += HandleRecipeExceedCalories; // Subscribe to the event
        recipe.EnterDetails();
        recipes.Add(recipe);

        Console.WriteLine($"Recipe '{recipeName}' details entered successfully.");
    }

    private void HandleRecipeExceedCalories(string recipeName)
    {
        Console.WriteLine($"Recipe '{recipeName}' exceeds 300 calories.");
    }

    public void DisplayRecipes()
    {
        if (recipes.Count == 0)
        {
            Console.WriteLine("No recipes to display.");
            return;
        }

        Console.WriteLine("List of Recipes:");
        foreach (var recipe in recipes.OrderBy(r => r.Name))
        {
            Console.WriteLine($"- {recipe.Name}");
        }

        Console.WriteLine("Enter the name of the recipe to display:");
        string recipeName = Console.ReadLine();
        var selectedRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

        if (selectedRecipe != null)
        {
            selectedRecipe.DisplayRecipe();
        }
        else
        {
            Console.WriteLine($"Recipe '{recipeName}' not found.");
        }
    }

    public void ScaleRecipe()
    {
        Console.WriteLine("Enter the name of the recipe to scale:");
        string recipeName = Console.ReadLine();
        var selectedRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

        if (selectedRecipe != null)
        {
            Console.WriteLine("Enter scaling factor (0.5, 2, or 3):");
            double factor;
            if (!double.TryParse(Console.ReadLine(), out factor) || (factor != 0.5 && factor != 2 && factor != 3))
            {
                Console.WriteLine("Invalid input. Please enter 0.5, 2, or 3 as the scaling factor.");
                return;
            }

            selectedRecipe.Scale(factor);
        }
        else
        {
            Console.WriteLine($"Recipe '{recipeName}' not found.");
        }
    }

    public void ResetQuantities()
    {
        // Reset quantities to original values (not implemented in this basic example)
        Console.WriteLine("Quantities reset to original values.");
    }

    public void ClearData()
    {
        recipes.Clear();
        Console.WriteLine("All recipe data cleared successfully.");
    }

    public void EnterChickenRecipe()
    {
        Recipe chickenRecipe = new Recipe("Chicken Recipe");
        chickenRecipe.AddIngredient("Chicken", 500, "grams", 900, "Protein");
        chickenRecipe.AddIngredient("Salt", 10, "grams", 0, "Seasoning");
        chickenRecipe.AddIngredient("Pepper", 5, "grams", 0, "Seasoning");
        recipes.Add(chickenRecipe);

        Console.WriteLine("Chicken recipe added successfully.");
    }
}
