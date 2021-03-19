using System;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class IngredientAmount : EntityBase
    {

        public IngredientAmount()
        {
        }

        public Ingredient Ingredient { get; set; }
        public Meal Meal { get; set; }
        public int Amount { get; set; }


        private sealed class IngredientAmountEqualityComparer : IEqualityComparer<IngredientAmount>
        {
            public bool Equals(IngredientAmount x, IngredientAmount y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x.Ingredient, y.Ingredient) && x.Amount == y.Amount;
            }

            public int GetHashCode(IngredientAmount obj)
            {
                return HashCode.Combine(obj.Ingredient, obj.Meal, obj.Amount);
            }
        }

        public static IEqualityComparer<IngredientAmount> IngredientAmountComparer { get; } = new IngredientAmountEqualityComparer();
    }
}