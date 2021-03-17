using System.Collections.Generic;
using FoodCalendar.DAL.Enums;

namespace FoodCalendar.DAL.Entities
{
    public class Desert : MealBase
    {
        public ICollection<DesertType> DesertTypes;
    }
}