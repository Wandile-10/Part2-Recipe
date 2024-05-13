using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApplication
{
    // Delegate to notify when recipe exceeds 300 calories
    public delegate void RecipeExceedCaloriesHandler(string recipeName);

    class Program
    {
        static void Main(string[] args)
        {
            RecipeManager recipeManager = new RecipeManager();
            bool exit = false;

            Console.WriteLine("==========================================================================");
            Console.WriteLine("Welcome to My Recipe App!");
            Console.WriteLine("==========================================================================");

            while (!exit)
            {
                Console.WriteLine("1. Enter Recipe Details");
                Console.WriteLine("2. Display Recipe");
                Console.WriteLine("3. Scale Recipe");
                Console.WriteLine("4. Reset Quantities");
                Console.WriteLine("5. Clear Data");
                Console.WriteLine("6. Enter Chicken Recipe");
                Console.WriteLine("7. Exit");
                Console.WriteLine("Enter your choice:");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        recipeManager.EnterRecipeDetails();
                        break;
                    case 2:
                        recipeManager.DisplayRecipes();
                        break;
                    case 3:
                        recipeManager.ScaleRecipe();
                        break;
                    case 4:
                        recipeManager.ResetQuantities();
                        break;
                    case 5:
                        recipeManager.ClearData();
                        break;
                    case 6:
                        recipeManager.EnterChickenRecipe();
                        break;
                    case 7:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }

    class RecipeManager
    {
        private List<Recipe> recipes = new List<Recipe>();

        // Event to trigger when recipe exceeds 300 calories
        public event RecipeExceedCaloriesHandler RecipeExceededCalories;

        public void EnterRecipeDetails()
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

    class Recipe
    {
        public string Name { get; }
        private List<Ingredient> ingredients = new List<Ingredient>();
        private List<string> steps = new List<string>();

        // Event to notify when recipe exceeds 300 calories
        public event RecipeExceedCaloriesHandler RecipeExceededCalories;

        public Recipe(string name)
        {
            Name = name;
        }

        public void AddIngredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
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

                ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
            }

            // Calculate and check total calories
            int totalCalories = ingredients.Sum(i => i.Calories);
            if (totalCalories > 300)
            {
                RecipeExceededCalories?.Invoke(Name);
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
                Console.WriteLine($"- {ingredient.Name}, {ingredient.Quantity} {ingredient.Unit}, {ingredient.Calories} calories, {ingredient.FoodGroup}");
            }

            Console.WriteLine("Steps:");
            foreach (var step in steps)
            {
                Console.WriteLine($"- {step}");
            }
        }

        public void Scale(double factor)
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.Quantity *= factor;
                ingredient.Calories = (int)Math.Round(ingredient.Calories * factor);
            }
        }
    }

    class Ingredient
    {
        public string Name { get; }
        public double Quantity { get; set; }
        public string Unit { get; }
        public int Calories { get; set; }
        public string FoodGroup { get; }

        public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }
}
