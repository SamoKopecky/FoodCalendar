using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.BL.Models
{
    public class IngredientAmountModel : ModelBase
    {
        public IngredientModel Ingredient { get; set; }
        public int Amount { get; set; }
    }
}