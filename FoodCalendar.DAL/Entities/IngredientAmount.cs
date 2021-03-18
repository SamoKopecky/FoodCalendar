using System;

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


        protected bool Equals(IngredientAmount other)
        {
            return Equals(Ingredient, other.Ingredient) && Equals(Meal, other.Meal) && Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IngredientAmount) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ingredient, Meal, Amount);
        }

        public static bool operator ==(IngredientAmount left, IngredientAmount right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(IngredientAmount left, IngredientAmount right)
        {
            return !Equals(left, right);
        }
    }
}