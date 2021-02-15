using FoodCalendar.DAL.Enums;

namespace FoodCalendar.DAL.Entities
{
    public class Drink : DishPartBase<DrinkType>
    {
        public Drink(int calories) : base(calories)
        {
        }

        public Drink()
        {
        }
    }
}