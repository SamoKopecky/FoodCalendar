using System.Collections.Generic;
using FoodCalendar.DAL.Enums;

namespace FoodCalendar.DAL.Entities
{
    public class Food : DishPartBase
    {
        public ICollection<FoodType> FoodTypes { get; set; }
    }
}