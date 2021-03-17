﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public class DishMeal : EntityBase
    {
        public Guid DishId { get; set; }
        public Dish Dish { get; set; }

        public Guid MealId { get; set; }
        public Meal Meal { get; set; }
    }
}