using FoodCalendar.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public abstract class DishPartBase : EntityBase, IDishPart
    {
        public ICollection<IDishPart> ingredients { get; set; }
        public Procces procces { get; set; }
        public int Calories { get; set; }
        public int TotalTime { get; set; }

        protected DishPartBase(Procces procces, int calories, int totalTime)
        {
            ingredients = new List<IDishPart>();
            this.procces = procces;
            Calories = calories;
            TotalTime = totalTime;
        }
    }
}
