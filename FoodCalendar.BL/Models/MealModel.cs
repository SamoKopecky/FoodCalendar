using System.Collections.Generic;

namespace FoodCalendar.BL.Models
{
    public class MealModel : ModelBase
    {
        public ProcessModel Process { get; set; }
        public int Calories { get; set; }
        public int TotalTime { get; set; }
        public ICollection<IngredientAmountModel> IngredientsUsed { get; set; }
    }
}