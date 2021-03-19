using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodCalendar.DAL.Entities
{
    public class Day : EntityBase
    {
        public DateTime Date { get; set; }
        public int CaloriesLimit { get; set; }
        public int CaloriesSum { get; set; }
        public ICollection<DayDish> Dishes { get; set; } = new List<DayDish>();

        public Day()
        {
        }

        private class DayEqualityComparerWithoutDishes : IEqualityComparer<Day>
        {
            public virtual bool Equals(Day x, Day y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Date.Equals(y.Date) &&
                       x.CaloriesLimit == y.CaloriesLimit &&
                       x.CaloriesSum == y.CaloriesSum;
            }

            public int GetHashCode(Day obj)
            {
                return HashCode.Combine(obj.Date, obj.CaloriesLimit, obj.CaloriesSum, obj.Dishes);
            }
        }

        public static IEqualityComparer<Day> BasicDayComparerWithoutDishes { get; } = new DayEqualityComparerWithoutDishes();

        private sealed class DayEqualityComparer : DayEqualityComparerWithoutDishes
        {
            public override bool Equals(Day x, Day y)
            {
                return base.Equals(x, y) &&
                       x.Dishes.Select(dd => dd.Dish)
                           .SequenceEqual(y.Dishes
                               .Select(dd => dd.Dish), Dish.DishComparerWithoutDays);
            }
        }

        public static IEqualityComparer<Day> DayComparer { get; } = new DayEqualityComparer();
    }
}