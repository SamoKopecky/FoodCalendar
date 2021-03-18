using System;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class Day : EntityBase
    {
        public DateTime Date { get; set; }
        public int CaloriesLimit { get; set; }
        public int CaloriesSum { get; set; }
        public ICollection<DayDish> Dishes { get; set; }

        public Day()
        {
        }

        public Day(int caloriesLimit)
        {
            Dishes = new List<DayDish>();
            CaloriesLimit = caloriesLimit;
            CaloriesSum = 0;
        }

        protected bool Equals(Day other)
        {
            return Date.Equals(other.Date) && CaloriesLimit == other.CaloriesLimit &&
                   CaloriesSum == other.CaloriesSum && Equals(Dishes, other.Dishes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Day) obj);
        }

        public static bool operator ==(Day left, Day right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Day left, Day right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Date, CaloriesLimit, CaloriesSum, Dishes);
        }
    }
}