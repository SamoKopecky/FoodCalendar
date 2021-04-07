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
        public ICollection<DishMeal> DishMeals { get; set; } = new List<DishMeal>();

        public Dish() : base()
        {
        }

        private class DishEqualityComparerWithoutMeals : IEqualityComparer<Dish>
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
                return HashCode.Combine(obj.TotalTime, obj.DishName, obj.DishTimeAndDate, obj.Calories, obj.DishMeals);
            }
        }

        public static IEqualityComparer<Dish> DishComparerWithoutMeals { get; } =
            new DishEqualityComparerWithoutMeals();

        private sealed class DishEqualityComparer : DishEqualityComparerWithoutMeals
        {
            public override bool Equals(Dish x, Dish y)
            {
                return base.Equals(x, y) &&
                       x.DishMeals.Select(dm => dm.Meal)
                           .SequenceEqual(y.DishMeals
                               .Select(dm => dm.Meal), Meal.MealComparerWithoutDishes);
            }
        }

        public static IEqualityComparer<Dish> DishComparer { get; } = new DishEqualityComparer();
    }
}