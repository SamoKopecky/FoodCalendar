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
        public ICollection<DishMeal> DishMeals { get; set; } = new List<DishMeal>();

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
                    obj.DishMeals);
            }
        }

        private class MealEqualityComparerWithoutDishes : BasicMealEqualityComparer
        {
            public override bool Equals(Meal x, Meal y)
            {
                return base.Equals(x, y) &&
                       x.IngredientsUsed.SequenceEqual(y.IngredientsUsed, IngredientAmount.IngredientAmountComparer);
            }
        }

        public static IEqualityComparer<Meal> MealComparerWithoutDishes { get; } =
            new MealEqualityComparerWithoutDishes();

        private class MealEqualityComparer : BasicMealEqualityComparer
        {
            public override bool Equals(Meal x, Meal y)
            {
                return base.Equals(x, y) &&
                       x.IngredientsUsed.SequenceEqual(y.IngredientsUsed, IngredientAmount.IngredientAmountComparer) &&
                       x.DishMeals.Select(dm => dm.Dish)
                           .SequenceEqual(y.DishMeals
                               .Select(dm => dm.Dish), Dish.DishComparerWithoutMeals);
            }
        }

        public static IEqualityComparer<Meal> MealComparer { get; } = new MealEqualityComparer();
    }
}