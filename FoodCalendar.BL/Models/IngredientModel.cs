using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.BL.Models
{
    public class IngredientModel : ModelBase
    {
        public string Name { get; set; }
        public int AmountStored { get; set; }
        public string UnitName { get; set; }
        public int Calories { get; set; }
    }
}