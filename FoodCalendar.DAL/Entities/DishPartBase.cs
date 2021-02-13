using FoodCalendar.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    abstract class DishPartBase : EntityBase, IDishPart
    {
        ICollection<IDishPart> Ingredients { get; set; }
        Procces procces { get; set; }
        int Calories { get; set; }
        int TotalTime { get; set; }
    }
}
