using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    class Dish : EntityBase
    {
        int TotalTime { get; set; }
        string DishName { get; set; }
        DateTime DishTime { get; set; }
        int Calories { get; set; }
        ICollection<Food> foods { get; set; }
        ICollection<Drink> drinks { get; set; }
        ICollection<Desert> deserts { get; set; }
    }
}
