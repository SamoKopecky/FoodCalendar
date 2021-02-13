using FoodCalendar.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public class Desert : DishPartBase
    {
        public ICollection<DesertType> desertTypes { get; set; }

        public Desert(Procces procces, int calories, int totalTime, DesertType desertType) : base(procces, calories, totalTime)
        {
            this.desertTypes = desertTypes;
        }
    }
}
