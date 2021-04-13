using System;
using System.Collections.Generic;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.DAL.Seeds
{
    public static class DishSeed
    {
        public static readonly Dish Lunch = new Dish()
        {
            TotalTime = 60,
            DishName = "Lunch",
            DishTimeAndDate = new DateTime(2021, 3, 21, 18, 00, 00),
            Calories = 9,
            Meals = new List<Meal>() {MealSeed.HamAndEggs}
        };
    }
}