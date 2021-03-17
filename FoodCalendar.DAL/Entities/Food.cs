using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FoodCalendar.DAL.Enums;

namespace FoodCalendar.DAL.Entities
{
    public class Food : MealBase
    {
        [NotMapped]
        public ICollection<FoodType> FoodTypes { get; set; }

        public Food()
        {
        }

        public Food(ICollection<FoodType> foodTypes)
        {
            FoodTypes = foodTypes;
        }

        public Food(int calories, ICollection<FoodType> foodTypes) : base(calories)
        {
            FoodTypes = foodTypes;
        }
    }
}