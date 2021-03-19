using System;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class Ingredient : EntityBase
    {
        public Ingredient()
        {
        }

        public string Name { get; set; }
        public int AmountStored { get; set; }
        public string UnitName { get; set; }
        public int Calories { get; set; }
        public ICollection<IngredientAmount> IngredientAmounts { get; set; } = new List<IngredientAmount>();

        private sealed class IngredientEqualityComparer : IEqualityComparer<Ingredient>
        {
            public bool Equals(Ingredient x, Ingredient y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Name == y.Name && x.AmountStored == y.AmountStored && x.UnitName == y.UnitName &&
                       x.Calories == y.Calories;
            }

            public int GetHashCode(Ingredient obj)
            {
                return HashCode.Combine(obj.Name, obj.AmountStored, obj.UnitName, obj.Calories, obj.IngredientAmounts);
            }
        }

        public static IEqualityComparer<Ingredient> IngredientComparer { get; } = new IngredientEqualityComparer();
    }
}