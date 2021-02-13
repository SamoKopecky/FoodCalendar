using FoodCalendar.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public class Drink : DishPartBase
    {
        public Drink(Procces procces, int calories, int totalTime) : base(procces, calories, totalTime)
        {
        }

        public ICollection<DrinkType> drinkTypes { get; set; }

    }
}
