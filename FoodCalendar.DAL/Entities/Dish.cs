using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodCalendar.DAL.Entities
{
    public class Dish : EntityBase
    {
        public int TotalTime { get; set; }
        public string DishName { get; set; }
        public DateTime DishTimeAndDate { get; set; }
        public int Calories { get; set; }
        public ICollection<Meal> Meals { get; set; } = new List<Meal>();

        public Dish() : base()
        {
        }

        private class DishEqualityComparerNoMeals : IEqualityComparer<Dish>
        {
            public virtual bool Equals(Dish x, Dish y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.TotalTime == y.TotalTime &&
                       x.DishName == y.DishName &&
                       x.DishTimeAndDate.Equals(y.DishTimeAndDate) &&
                       x.Calories == y.Calories;
            }

            public int GetHashCode(Dish obj)
            {
                return HashCode.Combine(obj.TotalTime, obj.DishName, obj.DishTimeAndDate, obj.Calories, obj.Meals);
            }
        }

        private sealed class DishEqualityComparer : DishEqualityComparerNoMeals
        {
            public override bool Equals(Dish x, Dish y)
            {
                return y != null && x != null && base.Equals(x, y) &&
                       x.Meals.SequenceEqual(y.Meals, Meal.MealComparerNoDishes);
            }
        }

        public static IEqualityComparer<Dish> DishComparerNoMeals { get; } = new DishEqualityComparerNoMeals();
        public static IEqualityComparer<Dish> DishComparer { get; } = new DishEqualityComparer();
    }
}