using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    class Day : EntityBase
    {
        int CaloriesLimit { get; set; }
        int CaloriesSum { get; set; }
        ICollection<Dish> dishes { get; set; }
    }
}
