using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public class Dish : EntityBase
    {
        public int TotalTime { get; set; }
        public string DishName { get; set; }
        public DateTime DishTime { get; set; }
        public int Calories { get; set; }
        public ICollection<Food> foods { get; set; }
        public ICollection<Drink> drinks { get; set; }
        public ICollection<Desert> deserts { get; set; }

        public Dish(int totalTime, string dishName, DateTime dishTime, int calories)
        {
            foods = new List<Food>();
            drinks = new List<Drink>();
            deserts = new List<Desert>();
            TotalTime = totalTime;
            DishName = dishName;
            DishTime = dishTime;
            Calories = calories;
        }
    }
}
