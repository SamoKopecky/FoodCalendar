using System;
using System.Collections.Generic;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.DAL.Entities
{
    public class Dish : EntityBase
    {
        public int TotalTime { get; set; }
        public string DishName { get; set; }
        public DateTime DishTime { get; set; }
        public int Calories { get; set; }
        public ICollection<DishPartBase> Meals { get; set; }

        public Dish(string dishName, DateTime dishTime)
        {
            Meals = new List<DishPartBase>();
            DishName = dishName;
            DishTime = dishTime;
            TotalTime = 0;
            Calories = 0;
        }

        public void SumTimeAndCalories()
        {
            foreach (var meal in Meals)
            {
                TotalTime += meal.TotalTime;
                Calories += meal.Calories;
            }
        }
    }
}