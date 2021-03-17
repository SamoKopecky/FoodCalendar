using System.Collections.Generic;
using FoodCalendar.DAL.Enums;

namespace FoodCalendar.DAL.Entities
{
    public class Drink : MealBase
    {
        public ICollection<DrinkType> DrinkTypes { get; set; }
    }
}