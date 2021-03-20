using System;
using System.Collections.Generic;

namespace FoodCalendar.BL.Models
{
    public class DishModel : ModelBase
    {
        public int TotalTime { get; set; }
        public string DishName { get; set; }
        public DateTime DishTime { get; set; }
        public int Calories { get; set; }
        public ICollection<MealModel> Meals { get; set; }
    }
}