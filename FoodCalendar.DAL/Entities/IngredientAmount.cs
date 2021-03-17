namespace FoodCalendar.DAL.Entities
{
    public class IngredientAmount : EntityBase
    {
        // Needed for entity framework
        public IngredientAmount()
        {
        }

        public IngredientAmount(int amount, Ingredient ingredient)
        {
            Amount = amount;
            Ingredient = ingredient;
        }

        public Ingredient Ingredient { get; set; }
        public Meal Meal { get; set; }
        public int Amount { get; set; }
    }
}