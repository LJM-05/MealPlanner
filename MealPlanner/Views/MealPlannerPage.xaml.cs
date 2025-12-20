using MealPlanner.Models;
using MealPlanner.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.Linq;

namespace MealPlanner.Views;

public partial class MealPlannerPage : ContentPage
{
    // These link to the services we created earlier
    private readonly MealPlanService _mealPlanService = new MealPlanService();
    private readonly RecipeService _recipeService = new RecipeService();

    public MealPlannerPage()
    {
        InitializeComponent();
    }

    // This runs every time you click on the "My Plan" tab
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadData();
    }

    private async Task LoadData()
    {
        try
        {
            // 1. Load your favorites into the Picker so you can choose them
            var favorites = await _recipeService.LoadFavoritesAsync();

            if (favorites != null && favorites.Any())
            {
                RecipePicker.ItemsSource = favorites;
            }
            else
            {
                CurrentMealLabel.Text = "Go to Search and favorite some meals first!";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Could not load favorites: " + ex.Message, "OK");
        }
    }

    private async void OnSavePlanClicked(object sender, EventArgs e)
    {
        // 1. Get the selected Day from the first picker
        var selectedDay = DayPicker.SelectedItem?.ToString();

        // 2. Get the selected Recipe from the second picker
        var selectedRecipe = RecipePicker.SelectedItem as Recipe;

        if (string.IsNullOrEmpty(selectedDay) || selectedRecipe == null)
        {
            await DisplayAlert("Missing Info", "Please select both a day and a meal.", "OK");
            return;
        }

        try
        {
            // 3. Save to the JSON file via the Service
            await _mealPlanService.SaveMealPlanAsync(selectedDay, selectedRecipe);

            // 4. Update the UI to show success
            CurrentMealLabel.Text = $"Successfully planned {selectedRecipe.strMeal} for {selectedDay}!";
            await DisplayAlert("Success", "Meal Plan Saved!", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Save failed: " + ex.Message, "OK");
        }
    }
}