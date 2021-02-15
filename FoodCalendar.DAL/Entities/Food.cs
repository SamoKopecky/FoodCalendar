using FoodCalendar.DAL.Enums;

namespace FoodCalendar.DAL.Entities
{
    public class Food : DishPartBase<FoodType>
    {
        public Food(int calories) : base(calories)
        {
        }

        public Food()
        {   
        }
    }
}