using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodCalendar.DAL.Entities
{
    public class Meal : EntityBase
    {
        public Guid ProcessId { get; set; }
        public Process Process { get; set; }
        public int Calories { get; set; }
        public int TotalTime { get; set; }
        public ICollection<IngredientAmount> IngredientsUsed { get; set; } = new List<IngredientAmount>();
        public Dish Dish { get; set; }

        public Meal() : base()
        {
        }


        private abstract class BasicMealEqualityComparer : IEqualityComparer<Meal>
        {
            public virtual bool Equals(Meal x, Meal y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.ProcessId.Equals(y.ProcessId) &&
                       Process.ProcessComparer.Equals(x.Process, y.Process) &&
                       x.Calories == y.Calories &&
                       x.TotalTime == y.TotalTime;
            }

            public int GetHashCode(Meal obj)
            {
                return HashCode.Combine(obj.ProcessId, obj.Process, obj.Calories, obj.TotalTime, obj.IngredientsUsed,
                    obj.Dish);
            }
        }

        private class MealEqualityComparerNoDishes : BasicMealEqualityComparer
        {
            public override bool Equals(Meal x, Meal y)
            {
                return y != null && x != null && base.Equals(x, y) &&
                       x.IngredientsUsed.SequenceEqual(y.IngredientsUsed,
                           IngredientAmount.IngredientAmountComparerNoMeal);
            }
        }

        private class MealEqualityComparerNoIngredientAmounts : BasicMealEqualityComparer
        {
            public override bool Equals(Meal x, Meal y)
            {
                return base.Equals(x, y) && Dish.DishComparerNoMeals.Equals(x?.Dish, y?.Dish);
            }
        }

        private class MealEqualityComparer : BasicMealEqualityComparer
        {
            public override bool Equals(Meal x, Meal y)
            {
                return x != null && y != null && base.Equals(x, y) &&
                       x.IngredientsUsed.SequenceEqual(y.IngredientsUsed,
                           IngredientAmount.IngredientAmountComparerNoMeal) &&
                       Dish.DishComparerNoMeals.Equals(x?.Dish, y?.Dish);
            }
        }

        public static IEqualityComparer<Meal> MealComparerNoIngredientAmounts { get; } =
            new MealEqualityComparerNoIngredientAmounts();

        public static IEqualityComparer<Meal> MealComparerNoDishes { get; } = new MealEqualityComparerNoDishes();
        public static IEqualityComparer<Meal> MealComparer { get; } = new MealEqualityComparer();
    }
}