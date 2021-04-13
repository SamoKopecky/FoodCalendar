using FoodCalendar.DAL.Entities;

namespace FoodCalendar.DAL.Seeds
{
    public static class IngredientAmountSeed
    {
        public static readonly IngredientAmount HamAmount = new IngredientAmount()
        {
            Amount = 2,
            Ingredient = IngredientSeed.Ham
        };

        public static readonly IngredientAmount EggAmount = new IngredientAmount()
        {
            Amount = 1,
            Ingredient = IngredientSeed.Egg
        };
    }
}
