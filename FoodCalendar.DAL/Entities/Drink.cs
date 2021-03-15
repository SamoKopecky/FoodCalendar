using System.Collections.Generic;
using FoodCalendar.DAL.Enums;

namespace FoodCalendar.DAL.Entities
{
    public class Drink : DishPartBase
    {
        public ICollection<DrinkType> DrinkTypes { get; set; }
    }
}