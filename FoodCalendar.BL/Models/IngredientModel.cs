using System;
using System.Collections.Generic;

namespace FoodCalendar.BL.Models
{
    public class IngredientModel : ModelBase
    {
        public string Name { get; set; }
        public int AmountStored { get; set; }
        public string UnitName { get; set; }
        public int Calories { get; set; }

        public IngredientModel() : base()
        {
        }

        private sealed class IngredientModelEqualityComparer : IEqualityComparer<IngredientModel>
        {
            public bool Equals(IngredientModel x, IngredientModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Name == y.Name && x.AmountStored == y.AmountStored && x.UnitName == y.UnitName &&
                       x.Calories == y.Calories;
            }

            public int GetHashCode(IngredientModel obj)
            {
                return HashCode.Combine(obj.Name, obj.AmountStored, obj.UnitName, obj.Calories);
            }
        }

        public static IEqualityComparer<IngredientModel> IngredientModelComparer { get; } =
            new IngredientModelEqualityComparer();

        public override bool Equals(object obj)
        {
            var ingredient = (IngredientModel) obj;
            return IngredientModelComparer.Equals(this, ingredient) && ingredient != null && ingredient.Id == Id;
        }

        public override int GetHashCode()
        {
            return IngredientModelComparer.GetHashCode(this);
        }
    }
}