using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public class DayDish : EntityBase
    {
        public Guid DayId { get; set; }
        public Day Day { get; set; }

        public Guid DishId { get; set; }
        public Dish Dish { get; set; }
    }
}