using System;
using System.Collections.Generic;
using System.Text;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.DAL.Seeds
{
    public static class IngredientAmountSeed
    {
        public static readonly IngredientAmount HamAmount = new IngredientAmount()
        {
            Id = Guid.Parse("ecf1fdb7-07c5-468b-b2ed-87fd66c45937"),
            Amount = 2,
        };

        public static readonly IngredientAmount EggAmount = new IngredientAmount()
        {
            Id = Guid.Parse("285c33fb-fc96-4591-bfaf-b385f6e9c35d"),
            Amount = 1,
        };
    }
}
