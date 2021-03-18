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
        public ICollection<IngredientAmount> IngredientAmounts { get; set; }


        public Ingredient(string name, int amountStored, string unitName, int calories)
        {
            Name = name;
            AmountStored = amountStored;
            UnitName = unitName;
            Calories = calories;
        }

        protected bool Equals(Ingredient other)
        {
            return Name == other.Name && AmountStored == other.AmountStored && UnitName == other.UnitName &&
                   Calories == other.Calories && Equals(IngredientAmounts, other.IngredientAmounts);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Ingredient) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, AmountStored, UnitName, Calories, IngredientAmounts);
        }

        public static bool operator ==(Ingredient left, Ingredient right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Ingredient left, Ingredient right)
        {
            return !Equals(left, right);
        }
    }
}