using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodCalendar.DAL.Entities
{
    public class Dish : EntityBase
    {
        public int TotalTime { get; set; }
        public string DishName { get; set; }
        public DateTime DishTime { get; set; }
        public int Calories { get; set; }
        public ICollection<DishMeal> DishMeals { get; set; } = new List<DishMeal>();
        public ICollection<DayDish> DayDishes { get; set; } = new List<DayDish>();

        public Dish() : base()
        {
        }

        private abstract class DishEqualityComparerBase : IEqualityComparer<Dish>
        {
            public virtual bool Equals(Dish x, Dish y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.TotalTime == y.TotalTime &&
                       x.DishName == y.DishName &&
                       x.DishTime.Equals(y.DishTime) &&
                       x.Calories == y.Calories;
            }

            public int GetHashCode(Dish obj)
            {
                return HashCode.Combine(obj.TotalTime, obj.DishName, obj.DishTime, obj.Calories, obj.DishMeals,
                    obj.DayDishes);
            }
        }


        private sealed class DishEqualityComparerWithoutDays : DishEqualityComparerBase
        {
            public override bool Equals(Dish x, Dish y)
            {
                return base.Equals(x, y) &&
                       x.DishMeals.Select(dm => dm.Meal)
                           .SequenceEqual(y.DishMeals
                               .Select(dm => dm.Meal), Meal.MealComparerWithoutDishes);
            }
        }

        public static IEqualityComparer<Dish> DishComparerWithoutDays { get; } = new DishEqualityComparerWithoutDays();

        private sealed class DishEqualityComparerWithoutMeals : DishEqualityComparerBase
        {
            public override bool Equals(Dish x, Dish y)
            {
                return base.Equals(x, y) &&
                       x.DayDishes.Select(dd => dd.Day)
                           .SequenceEqual(y.DayDishes
                               .Select(dd => dd.Day), Day.BasicDayComparerWithoutDishes);
            }
        }

        public static IEqualityComparer<Dish> DishComparerWithoutMeals { get; } = new DishEqualityComparerWithoutMeals();

        private sealed class DishEqualityComparer : DishEqualityComparerBase
        {
            public override bool Equals(Dish x, Dish y)
            {
                return base.Equals(x, y) &&
                       x.DishMeals.Select(dm => dm.Meal)
                           .SequenceEqual(y.DishMeals
                               .Select(dm => dm.Meal), Meal.MealComparerWithoutDishes) &&
                       x.DayDishes.Select(dd => dd.Day)
                           .SequenceEqual(y.DayDishes
                               .Select(dd => dd.Day), Day.BasicDayComparerWithoutDishes);
            }
        }

        public static IEqualityComparer<Dish> DishComparer { get; } = new DishEqualityComparer();
    }
}