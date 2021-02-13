using FoodCalendar.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    class Ingredient: EntityBase, IDishPart
    {
        string Name { get; set; }
        int Amount { get; set; }
        string UnitName { get; set; }
        int Calories { get; set; }
    }   
}
