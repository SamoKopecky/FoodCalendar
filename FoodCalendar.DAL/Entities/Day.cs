using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class Day : EntityBase
    {
        public int CaloriesLimit { get; set; }
        public int CaloriesSum { get; set; }
        public ICollection<Dish> Dishes { get; set; }

        public Day(int caloriesLimit)
        {
            Dishes = new List<Dish>();
            CaloriesLimit = caloriesLimit;
            CaloriesSum = 0;
        }

        public void SumCalories()
        {
            foreach (var dish in Dishes)
            {
                CaloriesSum += dish.Calories;
            }
        }
    }
}