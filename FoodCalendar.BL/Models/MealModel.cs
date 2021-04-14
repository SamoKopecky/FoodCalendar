using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodCalendar.BL.Models
{
    public class MealModel : ModelBase
    {
        public ProcessModel Process { get; set; }
        public string MealName { get; set; }
        public int Calories { get; set; }
        public int TotalTime { get; set; }
        public ICollection<IngredientAmountModel> IngredientsUsed { get; set; } = new List<IngredientAmountModel>();

        public MealModel() : base()
        {
        }

        private sealed class MealModelEqualityComparer : IEqualityComparer<MealModel>
        {
            public bool Equals(MealModel x, MealModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return ProcessModel.ProcessModelComparer.Equals(x.Process, y.Process) &&
                       x.Calories == y.Calories &&
                       x.TotalTime == y.TotalTime &&
                       x.MealName == y.MealName &&
                       x.IngredientsUsed.SequenceEqual(y.IngredientsUsed,
                           IngredientAmountModel.IngredientAmountComparer);
                ;
            }

            public int GetHashCode(MealModel obj)
            {
                return HashCode.Combine(obj.Process, obj.Calories, obj.TotalTime, obj.IngredientsUsed);
            }
        }

        public static IEqualityComparer<MealModel> MealModelComparer { get; } = new MealModelEqualityComparer();

        public override bool Equals(object obj)
        {
            var meal = (MealModel) obj;
            return MealModelComparer.Equals(this, meal) && meal != null && Id == meal.Id;
        }

        public override int GetHashCode()
        {
            return MealModelComparer.GetHashCode(this);
        }
    }
}