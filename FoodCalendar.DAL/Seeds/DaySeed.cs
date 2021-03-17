using System;
using System.Collections.Generic;
using System.Text;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.DAL.Seeds
{
    public static class DaySeed
    {
        public static readonly Day Monday = new Day()
        {
            Id = Guid.Parse("980050ee-ff8a-4549-98f9-0154873093fc"),
            Date = new DateTime(2021, 3, 21),
            CaloriesLimit = 50,
            CaloriesSum = 9,
            Dishes = new List<DayDish>()
        };
    }
}