using MealPlanner.Models;
using MealPlanner.Services;

namespace MealPlanner.Views;

public partial class RecipeDetailPage : ContentPage
{
    private readonly RecipeService _recipeService = new RecipeService();
    public RecipeDetailPage(Recipe recipe)
    {
        InitializeComponent();
        BindingContext = recipe; // This links the UI to the specific recipe data [cite: 56]
    }

    private async void OnSaveFavoriteClicked(object sender, EventArgs e)
    {
        // The BindingContext is the Recipe object we passed earlier
        if (BindingContext is Recipe selectedRecipe)
        {
            await _recipeService.SaveToFavoritesAsync(selectedRecipe);

            // Visual feedback (UX improvement)
            await DisplayAlert("Saved", $"{selectedRecipe.strMeal} added to favorites!", "OK");
        }
    }
}