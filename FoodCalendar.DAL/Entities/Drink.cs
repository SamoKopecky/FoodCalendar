using FoodCalendar.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    class Drink: DishPartBase
    {
        ICollection<DrinkType> drinkTypes { get; set; }
    }
}
