using FoodCalendar.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public class Drink : DishPartBase<DrinkType>
    {
        public Drink(int calories) : base(calories)
        {
        }

        public Drink()
        {
        }
    }
}