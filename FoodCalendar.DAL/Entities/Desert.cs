using System.Collections.Generic;
using FoodCalendar.DAL.Enums;

namespace FoodCalendar.DAL.Entities
{
    public class Desert : DishPartBase
    {
        public ICollection<DesertType> DesertTypes;
    }
}