using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.BL.Models
{
    public class DayModel : ModelBase
    {
        public DateTime Date { get; set; }
        public int CaloriesLimit { get; set; }
        public int CaloriesSum { get; set; }
        public ICollection<DishModel> Dishes { get; set; } = new List<DishModel>();

        public DayModel() : base()
        {
        }

        private sealed class DayModelEqualityComparer : IEqualityComparer<DayModel>
        {
            public bool Equals(DayModel x, DayModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Date.Equals(y.Date) &&
                       x.CaloriesLimit == y.CaloriesLimit &&
                       x.CaloriesSum == y.CaloriesSum &&
                       x.Dishes.SequenceEqual(y.Dishes, DishModel.DishModelComparer);
            }

            public int GetHashCode(DayModel obj)
            {
                return HashCode.Combine(obj.Date, obj.CaloriesLimit, obj.CaloriesSum, obj.Dishes);
            }
        }

        public static IEqualityComparer<DayModel> DayModelComparer { get; } = new DayModelEqualityComparer();
    }
}