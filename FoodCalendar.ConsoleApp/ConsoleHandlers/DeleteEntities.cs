using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public class DeleteEntities : ConsoleHandlerBase
    {
        public DeleteEntities(IDbContextFactory dbContextFactory, int idLength) : base(dbContextFactory, idLength)
        {
        }

        public void DeleteEntity()
        {
            var entities = new Dictionary<string, Action>()
            {
                {"Ingredient", DeleteIngredient},
                {"Meal", DeleteMeal},
                {"Dish", DeleteDish},
            };
            var choices = entities.Keys.ToList();
            choices.Add("Done");
            var optionsHandler = new OptionsHandler(choices);
            var option = optionsHandler.HandleOptions("Deleting entity");
            if (option == "Done") return;
            entities[option]();
        }

        private void DeleteDish()
        {
            var repo = new DishRepository(DbContextFactory);
            var table = Utils.GetDishTable(DbContextFactory, IdLength);
            var dish =
                Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Dish", IdLength);
            repo.Delete(dish.Id);
        }

        private void DeleteMeal()
        {
            var repo = new MealRepository(DbContextFactory);
            var table = Utils.GetMealTable(DbContextFactory, IdLength);
            var meal =
                Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Meal", IdLength);
            repo.Delete(meal.Id);
        }

        private void DeleteIngredient()
        {
            var repo = new IngredientRepository(DbContextFactory);
            var table = Utils.GetIngredientTable(DbContextFactory, IdLength);
            var ingredient =
                Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Ingredient", IdLength);
            repo.Delete(ingredient.Id);
        }
    }
}