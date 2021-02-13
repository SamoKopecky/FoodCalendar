using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public class Day : EntityBase
    {
        public int CaloriesLimit { get; set; }
        public int CaloriesSum { get; set; }
        public ICollection<Dish> dishes { get; set; }

        public Day(int caloriesLimit, int caloriesSum, ICollection<Dish> dishes)
        {
            this.dishes = dishes;
            CaloriesLimit = caloriesLimit;
            CaloriesSum = caloriesSum;
            this.dishes = dishes;
        }
    }
}
