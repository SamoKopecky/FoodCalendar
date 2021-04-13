using System;
using FoodCalendar.BL.Models;

namespace FoodCalendar.ConsoleApp.DbSynchronization
{
    public class Synchronization
    {
        public static void SynchronizeEntity<TModel>(TModel entity)
            where TModel : ModelBase
        {
            switch (entity)
            {
                case MealModel meal:
                    SynchronizeMeal(meal);
                    break;
                case DishModel dish:
                    SynchronizeDish(dish);
                    break;
            }
        }

        private static void SynchronizeMeal(MealModel meal)
        {
            var caloriesBefore = meal.Calories;
            meal.TotalTime = meal.Process.TimeRequired;
            foreach (var ia in meal.IngredientsUsed)
            {
                var current = ia.Ingredient;
                if (current == null) break;
                var futureAmount = current.AmountStored -= ia.Amount;
                if (futureAmount < 0)
                {
                    throw new Exception("Not enough ingredients in the store.");
                }

                current.AmountStored = futureAmount;
                if (caloriesBefore != 0) continue;
                var currentCalories = ia.Amount * ia.Ingredient.Calories;
                meal.Calories += currentCalories;
            }
        }

        private static void SynchronizeDish(DishModel dish)
        {
            var caloriesBefore = dish.Calories;
            var timeBefore = dish.TotalTime;
            foreach (var meal in dish.Meals)
            {
                SynchronizeMeal(meal);
                if (timeBefore == 0)
                {
                    dish.TotalTime += meal.TotalTime;
                }

                if (caloriesBefore == 0)
                {
                    dish.Calories += meal.Calories;
                }
            }
        }
    }
}