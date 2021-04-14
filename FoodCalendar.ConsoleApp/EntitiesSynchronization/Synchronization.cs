using System;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.EntitiesSynchronization
{
    public class Synchronization
    {
        public static void SynchronizeEntity<TModel>(TModel entity, IDbContextFactory dbContextFactory)
            where TModel : ModelBase
        {
            switch (entity)
            {
                case MealModel meal:
                    SynchronizeMeal(meal, dbContextFactory);
                    break;
                case DishModel dish:
                    SynchronizeDish(dish, dbContextFactory);
                    break;
            }
        }

        private static void SynchronizeMeal(MealModel meal, IDbContextFactory dbContextFactory)
        {
            var dbMeal = new MealRepository(dbContextFactory).GetById(meal.Id);
            var caloriesBefore = meal.Calories;
            meal.TotalTime = meal.Process.TimeRequired;
            foreach (var ia in meal.IngredientsUsed)
            {
                // Check if the ingredient is being added or updated
                if (dbMeal != null && dbMeal.IngredientsUsed.Contains(ia)) continue;
                var ingredient = ia.Ingredient;
                var futureAmount = ingredient.AmountStored -= ia.Amount;
                if (futureAmount < 0)
                {
                    throw new Exception("Not enough ingredients in the store.");
                }

                ingredient.AmountStored = futureAmount;
                // If the meal is being copied and already has calories
                if (dbMeal == null && caloriesBefore != 0) continue;
                meal.Calories += ia.Amount * ia.Ingredient.Calories;
            }
        }

        private static void SynchronizeDish(DishModel dish, IDbContextFactory dbContextFactory)
        {
            var dbDish = new DishRepository(dbContextFactory).GetById(dish.Id);
            foreach (var meal in dish.Meals)
            {
                if (dbDish != null && dbDish.Meals.Contains(meal)) continue;
                SynchronizeMeal(meal, dbContextFactory);
                dish.TotalTime += meal.TotalTime;
                dish.Calories += meal.Calories;
            }
        }
    }
}