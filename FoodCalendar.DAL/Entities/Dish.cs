using System;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class Dish : EntityBase
    {
        public int TotalTime { get; set; }
        public string DishName { get; set; }
        public DateTime DishTime { get; set; }
        public int Calories { get; set; }
        public ICollection<Food> Foods { get; set; }
        public ICollection<Drink> Drinks { get; set; }
        public ICollection<Desert> Deserts { get; set; }

        public Dish(string dishName, DateTime dishTime)
        {
            Foods = new List<Food>();
            Drinks = new List<Drink>();
            Deserts = new List<Desert>();
            DishName = dishName;
            DishTime = dishTime;
            TotalTime = 0;
            Calories = 0;
        }

        public void SumTimeAndCalories()
        {
            foreach (var food in Foods)
            {
                TotalTime += food.TotalTime;
                Calories += food.Calories;
            }

            foreach (var drink in Drinks)
            {
                TotalTime += drink.TotalTime;
                Calories += drink.Calories;
            }

            foreach (var desert in Deserts)
            {
                TotalTime += desert.TotalTime;
                Calories += desert.Calories;
            }
        }
    }
}