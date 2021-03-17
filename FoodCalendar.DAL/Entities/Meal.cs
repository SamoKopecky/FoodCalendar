using System;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class Meal : EntityBase
    {
        public Guid ProcessId { get; set; }
        public Process Process { get; set; }
        public int Calories { get; set; }
        public int TotalTime { get; set; }
        public ICollection<IngredientAmount> IngredientsUsed { get; set; }
        public ICollection<DishMeal> DishMeals { get; set; }

        public Meal()
        {
            DishMeals = new List<DishMeal>();
            IngredientsUsed = new List<IngredientAmount>();
            TotalTime = 0;
            Calories = 0;
        }
    }
}