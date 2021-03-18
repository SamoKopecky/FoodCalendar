using System;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class Meal : EntityBase
    {
        public Guid ProcessId { get; set; }
        public Process Process { get; set; }
        public int Calories { get; set; }
        public int TotalTime { get; set; }
        public ICollection<IngredientAmount> IngredientsUsed { get; set; }
        public ICollection<DishMeal> DishMeals { get; set; }

        public Meal()
        {
            DishMeals = new List<DishMeal>();
            IngredientsUsed = new List<IngredientAmount>();
            TotalTime = 0;
            Calories = 0;
        }

        protected bool Equals(Meal other)
        {
            return ProcessId.Equals(other.ProcessId) && Equals(Process, other.Process) && Calories == other.Calories &&
                   TotalTime == other.TotalTime && Equals(IngredientsUsed, other.IngredientsUsed) &&
                   Equals(DishMeals, other.DishMeals);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Meal) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProcessId, Process, Calories, TotalTime, IngredientsUsed, DishMeals);
        }

        public static bool operator ==(Meal left, Meal right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Meal left, Meal right)
        {
            return !Equals(left, right);
        }
    }
}