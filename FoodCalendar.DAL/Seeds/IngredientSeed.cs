using System;
using System.Collections.Generic;
using System.Text;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.DAL.Seeds
{
    public static class IngredientSeed
    {
        public static readonly Ingredient Ham = new Ingredient()
        {
            Name = "Ham",
            AmountStored = 10,
            UnitName = "Ks",
            Calories = 2,
        };

        public static readonly Ingredient Egg = new Ingredient()
        {
            Name = "Egg",
            AmountStored = 15,
            UnitName = "Ks",
            Calories = 5
        };
    }
}