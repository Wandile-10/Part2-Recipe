using RecipeApplication;

class Recipe
{
    public string Name { get; }
    private List<Ingredient> ingredients = new List<Ingredient>();
    private List<string> steps = new List<string>();

    public Recipe(string name)
    {
        Name = name;
    }

    public void EnterDetails()
    {
        Console.WriteLine("Enter the number of ingredients:");
        int ingredientCount;
        if (!int.TryParse(Console.ReadLine(), out ingredientCount) || ingredientCount <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer for the number of ingredients.");
            return;
        }

        for (int i = 0; i < ingredientCount; i++)
        {
            Console.WriteLine($"Enter ingredient {i + 1} name:");
            string name = Console.ReadLine();

            Console.WriteLine($"Enter quantity for {name}:");
            double quantity;
            if (!double.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive number for the quantity.");
                return;
            }

            Console.WriteLine($"Enter unit for {name}:");
            string unit = Console.ReadLine();

            Console.WriteLine($"Enter number of calories for {name}:");
            int calories;
            if (!int.TryParse(Console.ReadLine(), out calories) || calories <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer for the calories.");
                return;
            }

            Console.WriteLine($"Enter food group for {name}:");
            string foodGroup = Console.ReadLine();

            ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup)) ;
        }

        Console.WriteLine("Enter the number of steps:");
        int stepCount;
        if (!int.TryParse(Console.ReadLine(), out stepCount) || stepCount <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer for the number of steps.");
            return;
        }

        for (int i = 0; i < stepCount; i++)
        {
            Console.WriteLine($"Enter step {i + 1}:");
            string description = Console.ReadLine();
            steps.Add(description);
        }
    }

    public void DisplayRecipe()
    {
        Console.WriteLine($"Recipe: {Name}");
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in ingredients)
        {
            Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}, {ingredient.Calories} calories, Food Group: {ingredient.FoodGroup}");
        }
        Console.WriteLine("Steps:");
        foreach (var step in steps)
        {
            Console.WriteLine(step);
        }

        // Calculate and display total calories
        int totalCalories = ingredients.Sum(i => i.Calories);
        Console.WriteLine($"Total Calories: {totalCalories}");
    }

    public void ScaleRecipe()
    {
        Console.WriteLine("Enter scaling factor (0.5, 2, or 3):");
        double factor;
        if (!double.TryParse(Console.ReadLine(), out factor) || (factor != 0.5 && factor != 2 && factor != 3))
        {
            Console.WriteLine("Invalid input. Please enter 0.5, 2, or 3 as the scaling factor.");
            return;
        }

        // Scale quantities of ingredients
        foreach (var ingredient in ingredients)
        {
            ingredient.Quantity *= factor;
        }

        Console.WriteLine("Recipe scaled successfully.");
    }

    // Other methods...
}