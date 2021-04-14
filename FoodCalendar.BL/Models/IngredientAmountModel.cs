using System;
using System.Collections.Generic;

namespace FoodCalendar.BL.Models
{
    public class IngredientAmountModel : ModelBase
    {
        public IngredientModel Ingredient { get; set; }
        public int Amount { get; set; }

        public IngredientAmountModel() : base()
        {
        }

        private sealed class IngredientAmountEqualityComparer : IEqualityComparer<IngredientAmountModel>
        {
            public bool Equals(IngredientAmountModel x, IngredientAmountModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return IngredientModel.IngredientModelComparer.Equals(x.Ingredient, y.Ingredient) &&
                       x.Amount == y.Amount;
            }

            public int GetHashCode(IngredientAmountModel obj)
            {
                return HashCode.Combine(obj.Ingredient, obj.Amount);
            }
        }

        public static IEqualityComparer<IngredientAmountModel> IngredientAmountComparer { get; } =
            new IngredientAmountEqualityComparer();

        public override bool Equals(object obj)
        {
            var ia = (IngredientAmountModel) obj;
            return IngredientAmountComparer.Equals(this, ia) && ia != null && Id == ia.Id;
        }

        public override int GetHashCode()
        {
            return IngredientAmountComparer.GetHashCode(this);
        }
    }
}