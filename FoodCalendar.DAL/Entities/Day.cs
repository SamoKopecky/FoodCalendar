using System;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class Day : EntityBase
    {
        public DateTime Date { get; set; }
        public int CaloriesLimit { get; set; }
        public int CaloriesSum { get; set; }
        public ICollection<DayDish> Dishes { get; set; }


        public Day(int caloriesLimit)
        {
            Dishes = new List<DayDish>();
            CaloriesLimit = caloriesLimit;
            CaloriesSum = 0;
        }

        public void SumCalories()
        {
            foreach (var dish in Dishes)
            {
                CaloriesSum += dish.Dish.Calories;
            }
        }
    }
}