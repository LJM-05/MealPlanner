namespace MealPlanner.Models;

public class MealPlan
{
    public string DayOfWeek { get; set; } // Monday, Tuesday, etc.
    public Recipe Breakfast { get; set; }
    public Recipe Lunch { get; set; }
    public Recipe Dinner { get; set; }
}
