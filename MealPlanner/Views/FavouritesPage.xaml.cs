using MealPlanner.Models;
using MealPlanner.Services;

namespace MealPlanner.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly RecipeService _recipeService = new RecipeService();

    public FavoritesPage()
    {
        InitializeComponent();
    }

    // This runs every time you navigate to this tab
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var favorites = await _recipeService.LoadFavoritesAsync();
        FavoritesListView.ItemsSource = favorites;
    }

    private async void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Recipe;
        if (selected != null)
        {
            await Navigation.PushAsync(new RecipeDetailPage(selected));
        }
        ((CollectionView)sender).SelectedItem = null;
    }
}