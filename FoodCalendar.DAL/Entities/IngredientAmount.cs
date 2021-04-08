using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace FoodCalendar.DAL.Entities
{
    public class IngredientAmount : EntityBase
    {
        public IngredientAmount() : base()
        {
        }

        public Ingredient Ingredient { get; set; }
        public Meal Meal { get; set; }
        public int Amount { get; set; }


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

        private class IngredientAmountEqualityComparerNoIngredient : BaseIngredientAmountEqualityComparer
        {
            public override bool Equals(IngredientAmount x, IngredientAmount y)
            {
                return base.Equals(x, y) && Meal.MealComparerNoIngredientAmounts.Equals(x?.Meal, y?.Meal);
            }
        }


        private class IngredientAmountEqualityComparerNoMeal : BaseIngredientAmountEqualityComparer
        {
            public override bool Equals(IngredientAmount x, IngredientAmount y)
            {
                return base.Equals(x, y) &&
                       Ingredient.IngredientComparerNoIngredientAmounts.Equals(x?.Ingredient, y?.Ingredient);
            }
        }

        private class IngredientAmountEqualityComparer : BaseIngredientAmountEqualityComparer
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