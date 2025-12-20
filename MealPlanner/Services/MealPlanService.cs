using System.Text.Json;
using MealPlanner.Models;

namespace MealPlanner.Services;

public class MealPlanService
{
    private readonly string _planFile = Path.Combine(FileSystem.AppDataDirectory, "mealplan.json");

    // Saves the plan for a specific day
    public async Task SaveMealPlanAsync(string day, Recipe recipe)
    {
        var currentPlans = await GetWeeklyPlanAsync();

        // Find if the day already exists, if so update it, else add new
        var existingDay = currentPlans.FirstOrDefault(p => p.DayOfWeek == day);
        if (existingDay != null)
        {
            existingDay.Dinner = recipe; // For now, we'll assign to 'Dinner'
        }
        else
        {
            currentPlans.Add(new MealPlan { DayOfWeek = day, Dinner = recipe });
        }

        string json = JsonSerializer.Serialize(currentPlans);
        await File.WriteAllTextAsync(_planFile, json);
    }

    public async Task<List<MealPlan>> GetWeeklyPlanAsync()
    {
        if (!File.Exists(_planFile)) return new List<MealPlan>();

        string json = await File.ReadAllTextAsync(_planFile);
        return JsonSerializer.Deserialize<List<MealPlan>>(json) ?? new List<MealPlan>();
    }
}
