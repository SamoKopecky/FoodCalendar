using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.BL.Models
{
    public class DayModel : ModelBase
    {
        public DateTime Date { get; set; }
        public int CaloriesLimit { get; set; }
        public int CaloriesSum { get; set; }
        public ICollection<DishModel> Dishes { get; set; }
    }
}
