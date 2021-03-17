using System;
using System.Collections.Generic;
using System.Text;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.DAL.Seeds
{
    public static class MealSeed
    {
        public static readonly Meal HamAndEggs = new Meal()
        {
            Id = Guid.Parse("e0f4beb1-51ed-497b-9760-7e06b3f35bfd"),
            Calories = 10,
            TotalTime = 60,
            DishMeals = new List<DishMeal>()
        };
    }
}