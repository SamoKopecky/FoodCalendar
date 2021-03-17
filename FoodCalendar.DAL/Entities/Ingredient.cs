using System.Collections.Generic;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.DAL.Entities
{
    public class Ingredient : EntityBase, IDishPart
    {
        public string Name { get; set; }
        public int AmountStored { get; set; }
        public string UnitName { get; set; }
        public int Calories { get; set; }

        public ICollection<IngredientAmount> IngredientAmounts { get; set; }

        public Ingredient(string name, int amountStored, string unitName, int calories)
        {
            Name = name;
            AmountStored = amountStored;
            UnitName = unitName;
            Calories = calories;
        }
    }
}