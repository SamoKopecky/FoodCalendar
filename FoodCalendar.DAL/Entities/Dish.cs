using System;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class Dish : EntityBase
    {
        public int TotalTime { get; set; }
        public string DishName { get; set; }
        public DateTime DishTime { get; set; }
        public int Calories { get; set; }
        public ICollection<DishMeal> DishMeals { get; set; }
        public ICollection<DayDish> DayDishes { get; set; }

        public Dish()
        {
        }

        public Dish(string dishName, DateTime dishTime)
        {
            DishMeals = new List<DishMeal>();
            DishName = dishName;
            DishTime = dishTime;
            TotalTime = 0;
            Calories = 0;
        }

        protected bool Equals(Dish other)
        {
            return TotalTime == other.TotalTime && DishName == other.DishName && DishTime.Equals(other.DishTime) &&
                   Calories == other.Calories && Equals(DishMeals, other.DishMeals) &&
                   Equals(DayDishes, other.DayDishes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Dish) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TotalTime, DishName, DishTime, Calories, DishMeals, DayDishes);
        }

        public static bool operator ==(Dish left, Dish right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Dish left, Dish right)
        {
            return !Equals(left, right);
        }
    }
}