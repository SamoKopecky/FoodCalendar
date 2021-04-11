using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public class DeleteEntities : ConsoleHandler
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
            var optionsHandler = new OptionsHandler(entities.Keys.ToList());
            var option = optionsHandler.HandleOptions("Deleting entity");
            entities[option]();
        }

        private void DeleteDish()
        {
            var repo = new DishRepository(DbContextFactory);
            var table = Utils.GetDishTable(DbContextFactory, IdLength);
            var ingredient =
                Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Dish", IdLength);
            repo.Delete(ingredient.Id);
        }

        private void DeleteMeal()
        {
            var repo = new MealRepository(DbContextFactory);
            var table = Utils.GetMealTable(DbContextFactory, IdLength);
            var ingredient =
                Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Meal", IdLength);
            repo.Delete(ingredient.Id);
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