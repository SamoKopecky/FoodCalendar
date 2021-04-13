using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public class UpdateEntities : ConsoleHandlerBase
    {
        public AddNewEntity AddNewEntity;

        public UpdateEntities(IDbContextFactory dbContextFactory, int idLength) : base(dbContextFactory, idLength)
        {
            AddNewEntity = new AddNewEntity(dbContextFactory, idLength);
        }

        public void UpdateEntity()
        {
            var entities = new Dictionary<string, Action>()
            {
                {"Ingredient", UpdateIngredient},
                {"Meal", UpdateMeal},
                {"Dish", UpdateDish}
            };
            var choices = entities.Keys.ToList();
            choices.Add("Done");
            var optionsHandler = new OptionsHandler(choices);
            var option = optionsHandler.HandleOptions("Updating entity");
            if (option == "Done")
            {
                return;
            }

            try
            {
                entities[option]();
            }
            catch (Exception e)
            {
                Utils.DisplayError(e);
            }
        }

        private void UpdateMeal()
        {
            var repo = new MealRepository(DbContextFactory);
            var table = Utils.GetMealTable(DbContextFactory, IdLength);
            var meal =
                Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Meal", IdLength);
            AddNewEntity.CreateMeal(meal);
            repo.Update(meal);
        }

        private void UpdateDish()
        {
            var repo = new DishRepository(DbContextFactory);
            var table = Utils.GetDishTable(DbContextFactory, IdLength);
            var dish =
                Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Dish", IdLength);
            AddNewEntity.CreateDish(dish);
            repo.Update(dish);
        }

        private void UpdateIngredient()
        {
            var repo = new IngredientRepository(DbContextFactory);
            var table = Utils.GetIngredientTable(DbContextFactory, IdLength);
            var ingredient =
                Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Ingredient", IdLength);
            AddNewEntity.CreateIngredient(ingredient);
            repo.Update(ingredient);
        }
    }
}