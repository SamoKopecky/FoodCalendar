using FoodCalendar.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    class Food: DishPartBase
    {
        ICollection<FoodType> foodTypes { get; set; }
    }
}
