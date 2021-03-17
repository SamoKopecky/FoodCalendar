using System;
using FoodCalendar.DAL.Interfaces;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class Meal : EntityBase, IDishPart
    {
        public Guid ProcessId { get; set; }
        private Process _process;

        public Process Process
        {
            get => _process;
            set
            {
                if (value.TimeRequired <= 0) throw new DivideByZeroException();
                TotalTime += value.TimeRequired;
                _process = value;
            }
        }

        public int Calories { get; set; }
        public int TotalTime { get; set; }

        public ICollection<IngredientAmount> IngredientsUsed { get; private set; }

        public ICollection<DishMeal> DishMeals { get; set; }

        private void Initialize()
        {
            IngredientsUsed = new List<IngredientAmount>();
            TotalTime = 0;
        }

        public Meal()
        {
            Initialize();
            Calories = 0;
        }

        public Meal(int calories)
        {
            Initialize();
            Calories = calories;
        }

        public void SumCalories()
        {
            Calories = CycleSumCalories(this.IngredientsUsed);
        }

        protected int CycleSumCalories(ICollection<IngredientAmount> ingredientsUsed)
        {
            var sum = 0;
            foreach (var ingredientAmount in ingredientsUsed)
            {
                sum += ingredientAmount.Amount * ingredientAmount.Ingredient.Calories;
            }

            return sum;
        }

        public void AddIngredient(Ingredient ingredient, int amount)
        {
            IngredientsUsed.Add(new IngredientAmount(amount, ingredient));
        }
    }
}