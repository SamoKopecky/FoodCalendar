using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public class IngredientAmount : EntityBase
    {
        public IngredientAmount(int amount, Ingredient ingredient)
        {
            Amount = amount;
            Ingredient = ingredient;
        }

        public Ingredient Ingredient { get; }
        public int Amount { get; }
    }
}