using System;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class IngredientAmount : EntityBase
    {
        public int Amount { get; set; }
        public Ingredient Ingredient { get; set; }
        public Meal Meal { get; set; }

        public IngredientAmount() : base()
        {
        }

        private abstract class BaseIngredientAmountEqualityComparer : IEqualityComparer<IngredientAmount>
        {
            public virtual bool Equals(IngredientAmount x, IngredientAmount y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Amount == y.Amount;
            }

            public int GetHashCode(IngredientAmount obj)
            {
                return HashCode.Combine(obj.Ingredient, obj.Meal, obj.Amount);
            }
        }

        private sealed class IngredientAmountEqualityComparerNoIngredient : BaseIngredientAmountEqualityComparer
        {
            public override bool Equals(IngredientAmount x, IngredientAmount y)
            {
                return base.Equals(x, y) && Meal.MealComparerNoIngredientAmounts.Equals(x?.Meal, y?.Meal);
            }
        }


        private sealed class IngredientAmountEqualityComparerNoMeal : BaseIngredientAmountEqualityComparer
        {
            public override bool Equals(IngredientAmount x, IngredientAmount y)
            {
                return base.Equals(x, y) &&
                       Ingredient.IngredientComparerNoIngredientAmounts.Equals(x?.Ingredient, y?.Ingredient);
            }
        }

        private sealed class IngredientAmountEqualityComparer : BaseIngredientAmountEqualityComparer
        {
            public override bool Equals(IngredientAmount x, IngredientAmount y)
            {
                return base.Equals(x, y) &&
                       Ingredient.IngredientComparerNoIngredientAmounts.Equals(x?.Ingredient, y?.Ingredient) &&
                       Meal.MealComparerNoIngredientAmounts.Equals(x?.Meal, y?.Meal);
            }
        }

        public static IEqualityComparer<IngredientAmount> IngredientAmountComparerNoIngredient { get; } =
            new IngredientAmountEqualityComparerNoIngredient();

        public static IEqualityComparer<IngredientAmount> IngredientAmountComparerNoMeal { get; } =
            new IngredientAmountEqualityComparerNoMeal();

        public static IEqualityComparer<IngredientAmount> IngredientAmountComparer { get; } =
            new IngredientAmountEqualityComparer();
    }
}