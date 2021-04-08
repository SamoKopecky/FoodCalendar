using System;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class Process : EntityBase
    {
        public int TimeRequired { get; set; }
        public string Description { get; set; }
        public Meal Meal { get; set; }

        public Process() : base()
        {
        }

        private class ProcessEqualityComparerNoMeal : IEqualityComparer<Process>
        {
            public virtual bool Equals(Process x, Process y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.TimeRequired == y.TimeRequired && x.Description == y.Description;
            }

            public int GetHashCode(Process obj)
            {
                return HashCode.Combine(obj.TimeRequired, obj.Description, obj.Meal);
            }
        }

        private class ProcessEqualityComparer : ProcessEqualityComparerNoMeal
        {
            public override bool Equals(Process x, Process y)
            {
                return base.Equals(x, y) && Meal.MealComparerNoProcess.Equals(x?.Meal, y?.Meal);
            }
        }

        public static IEqualityComparer<Process> ProcessComparer { get; } = new ProcessEqualityComparer();
        public static IEqualityComparer<Process> ProcessComparerNoMeal { get; } = new ProcessEqualityComparerNoMeal();
    }
}