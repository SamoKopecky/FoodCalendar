using FoodCalendar.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public class Food : DishPartBase
    {
        public Food(Procces procces, int calories, int totalTime) : base(procces, calories, totalTime)
        {
            foodTypes = new List<FoodType>();
        }

        public ICollection<FoodType> foodTypes { get; set; }


    }
}
