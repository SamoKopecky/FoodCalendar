using FoodCalendar.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public class Food : DishPartBase<FoodType>
    {
        public Food(int calories) : base(calories)
        {
        }

        public Food()
        {   
        }
    }
}