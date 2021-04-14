using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodCalendar.BL.Models
{
    public class DishModel : ModelBase
    {
        public int TotalTime { get; set; }
        public string DishName { get; set; }
        public DateTime DishTime { get; set; }
        public int Calories { get; set; }
        public ICollection<MealModel> Meals { get; set; } = new List<MealModel>();

        public DishModel() : base()
        {
        }

        private sealed class DishModelEqualityComparer : IEqualityComparer<DishModel>
        {
            public bool Equals(DishModel x, DishModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.TotalTime == y.TotalTime &&
                       x.DishName == y.DishName &&
                       x.DishTime.Equals(y.DishTime) &&
                       x.Calories == y.Calories &&
                       x.Meals.SequenceEqual(y.Meals, MealModel.MealModelComparer);
            }

            public int GetHashCode(DishModel obj)
            {
                return HashCode.Combine(obj.TotalTime, obj.DishName, obj.DishTime, obj.Calories, obj.Meals);
            }
        }

        public static IEqualityComparer<DishModel> DishModelComparer { get; } = new DishModelEqualityComparer();

        public override bool Equals(object obj)
        {
            var dish = (DishModel) obj;
            return  DishModelComparer.Equals(this, dish) && dish != null && Id == dish.Id;
        }

        public override int GetHashCode()
        {
            return DishModelComparer.GetHashCode(this);
        }
    }
}