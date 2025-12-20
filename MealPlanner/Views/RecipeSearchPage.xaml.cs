using System.Collections.Generic; 
using System.Linq;
using MealPlanner.Models;
using MealPlanner.Services;
using MealPlanner.Views;

namespace MealPlanner.Views;

public partial class RecipeSearchPage : ContentPage
{
    // Create an instance of the service we built
    private readonly RecipeService _recipeService = new RecipeService();

    public RecipeSearchPage()
    {
        InitializeComponent();
    }

    private async void OnSearchClicked(object sender, EventArgs e)
    {
        string query = SearchEntry.Text;

        if (string.IsNullOrWhiteSpace(query))
            return;

        // Show a loading indicator (optional but good for UX)
        // This fulfills the "interactivity/animation" requirement [cite: 60]

        // Use the service to get the recipe (this handles API + Cache) 
        var recipe = await _recipeService.GetRecipeWithCachingAsync(query);

        if (recipe != null)
        {
            // Wrap the single recipe in a list to show it in the CollectionView
            RecipesListView.ItemsSource = new List<Recipe> { recipe };
        }
        else
        {
            await DisplayAlert("Not Found", "No recipe found with that name.", "OK");
        }
    }

        private async void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
    {
        var selectedRecipe = e.CurrentSelection.FirstOrDefault() as Recipe;
        if (selectedRecipe == null) return;

        // Navigate to the Detail Page and pass the selected recipe data
        await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe));

        // Clear selection so the user can click it again later
        ((CollectionView)sender).SelectedItem = null;
    }
}