using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.DAL.Entities
{
    public class Ingredient : EntityBase, IDishPart
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string UnitName { get; set; }
        public int Calories { get; set; }

        public Ingredient(string name, int amount, string unitName, int calories)
        {
            Name = name;
            Amount = amount;
            UnitName = unitName;
            Calories = calories;
        }
    }   
}
