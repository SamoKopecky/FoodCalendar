using System;
using FoodCalendar.DAL.Interfaces;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public abstract class DishPartBase<T> : EntityBase, IDishPart
    {
        private Process _process;

        public Process Process
        {
            get => _process;
            set
            {
                if (value.TimeRequired <= 0) throw new DivideByZeroException();
                TotalTime = value.TimeRequired;
                _process = value;
            }
        }

        public int Calories { get; set; }
        public int TotalTime { get; set; }
        public ICollection<T> DishPartTypes { get; set; }

        public Dictionary<IDishPart, int> IngredientsUsed { get; private set; }

        private void Initialize()
        {
            IngredientsUsed = new Dictionary<IDishPart, int>();
            DishPartTypes = new List<T>();
            TotalTime = 0;
        }

        protected DishPartBase()
        {
            Initialize();
            Calories = 0;
        }

        protected DishPartBase(int calories)
        {
            Initialize();
            Calories = calories;
        }

        public void SumCalories()
        {
            Calories = CycleSumCalories(this.IngredientsUsed);
        }

        protected int CycleSumCalories(Dictionary<IDishPart, int> ingredientsUsed)
        {
            var sum = 0;
            foreach (var (key, value) in ingredientsUsed)
            {
                sum += key switch
                {
                    Ingredient ingredient => ingredient.Calories * value,
                    Desert desert => CycleSumCalories(desert.IngredientsUsed),
                    Food food => CycleSumCalories(food.IngredientsUsed),
                    Drink drink => CycleSumCalories(drink.IngredientsUsed),
                    _ => throw new Exception("Unknown class type")
                };
            }

            return sum;
        }
    }
}