using FoodCalendar.DAL.Enums;

namespace FoodCalendar.DAL.Entities
{
    public class Desert : DishPartBase<DesertType>
    {
        public Desert(int calories) : base(calories)
        {
        }

        public Desert()
        {
        }
    }
}