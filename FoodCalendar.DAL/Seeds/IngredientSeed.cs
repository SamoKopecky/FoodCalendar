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
            Id = Guid.Parse("0f71dc39-987d-4e6c-9732-ae73b254e160"),
            Name = "Ham",
            AmountStored = 10,
            UnitName = "Ks",
            Calories = 2
        };

        public static readonly Ingredient Egg = new Ingredient()
        {
            Id = Guid.Parse("171548b2-a7eb-4eeb-b4bf-b7c57a491103"),
            Name = "Egg",
            AmountStored = 15,
            UnitName = "Ks",
            Calories = 5
        };
    }
}