namespace MealPlanner.Models;

public class Recipe
{
    // These names must match the API's JSON keys exactly
    public string idMeal { get; set; }
    public string strMeal { get; set; }        // Name of the recipe
    public string strInstructions { get; set; } // Cooking steps
    public string strMealThumb { get; set; }    // Image URL
    public string strCategory { get; set; }    // e.g., Seafood, Dessert
    public string strArea { get; set; }        // e.g., Italian, British
}

// This helper class is needed because the API returns a list called "meals"
public class RecipeResponse
{
    public List<Recipe> meals { get; set; }
}
