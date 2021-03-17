using System;
using System.Collections.Generic;
using System.Text;
using FoodCalendar.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.DAL.Seeds
{
    public static class DishSeed
    {
        public static readonly Dish Lunch = new Dish()
        {
            Id = Guid.Parse("7a4fda42-ee8a-413b-9b3a-0191d5b42ec7"),
            TotalTime = 60,
            DishName = "Lunch",
            DishTime = new DateTime(2021, 3, 21, 18, 00, 00),
            Calories = 9,
            DishMeals = new List<DishMeal>(),
            DayDishes = new List<DayDish>()
        };
    }
}