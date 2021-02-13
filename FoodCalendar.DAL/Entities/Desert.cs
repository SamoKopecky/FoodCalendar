using FoodCalendar.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    class Desert: DishPartBase
    {
        ICollection<DesertType> desertTypes { get; set; }
    }
}
