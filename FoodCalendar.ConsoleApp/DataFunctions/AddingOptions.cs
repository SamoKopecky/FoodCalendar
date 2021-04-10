using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.ConsoleApp.ConsoleHandlers;
using FoodCalendar.DAL.Factories;

namespace FoodCalendar.ConsoleApp.DataFunctions
{
    public class AddingOptions
    {
        public static void AddEntity()
        {
            var dbContextFactory = new DbContextFactory();
            var entities = new Dictionary<string, Func<ModelBase>>()
            {
                {"Ingredient", CreateIngredient},
                {"Meal", CreateMeal},
                {"Dish", CreateDish}
            };
            var optionHandler = new OptionsHandler(entities.Keys.ToList());
            var createFunction = entities[optionHandler.HandleOptions()];
            var entity = createFunction();
            if (entity == null)
            {
                return;
            }

            switch (entity)
            {
                case IngredientModel ingredient:
                    new IngredientRepository(dbContextFactory).Insert(ingredient);
                    break;
                case MealModel meal:
                    new MealRepository(dbContextFactory).Insert(meal);
                    break;
                case DishModel dish:
                    new DishRepository(dbContextFactory).Insert(dish);
                    break;
            }
        }

        private static ModelBase CreateDish()
        {
            throw new NotImplementedException();
        }

        private static MealModel CreateMeal()
        {
            var meal = new MealModel()
            {
                Process = new ProcessModel()
            };
            var propertiesWithString = new Dictionary<string, Action<string, MealModel>>()
            {
                {"Calories", (s, m) => m.Calories = Utils.ScanProperty<int>(s)},
                {"Total Time", (s, m) => m.TotalTime = Utils.ScanProperty<int>(s)}
            };
            var properties = new Dictionary<string, Action<MealModel>>()
            {
                {"Process", m => m.Process = CreateProcess()},
                {"Ingredients", m => m.IngredientsUsed = CreateIngredientsUsed()}
            };
            FillEntity(propertiesWithString, properties, meal);

            return meal;
        }

        private static IngredientModel CreateIngredient()
        {
            var ingredient = new IngredientModel();
            var properties = new Dictionary<string, Action<string, IngredientModel>>()
            {
                {"Name", (s, i) => i.Name = Utils.ScanProperty<string>(s)},
                {"Amount Stored", (s, i) => i.AmountStored = Utils.ScanProperty<int>(s)},
                {"Unit Name", (s, i) => i.UnitName = Utils.ScanProperty<string>(s)},
                {"Calories", (s, i) => i.Calories = Utils.ScanProperty<int>(s)},
            };
            FillEntity(properties, null, ingredient);
            return ingredient;
        }

        private static ProcessModel CreateProcess()
        {
            var process = new ProcessModel();
            var properties = new Dictionary<string, Action<string, ProcessModel>>()
            {
                {"Time Required", (s, p) => p.TimeRequired = Utils.ScanProperty<int>(s)},
                {"Description", (s, p) => p.Description = Utils.ScanProperty<string>(s)}
            };

            FillEntity(properties, null, process);
            return process;
        }

        private static ICollection<IngredientAmountModel> CreateIngredientsUsed()
        {
            var ingredients = new List<IngredientAmountModel>();
            var ingredientAm = new IngredientAmountModel();
            var propertiesWithString = new Dictionary<string, Action<string, IngredientAmountModel>>()
            {
                {"Amount", (s, ia) => ia.Amount = Utils.ScanProperty<int>(s)}
            };
            var properties = new Dictionary<string, Action<IngredientAmountModel>>()
            {
                {"Create a new ingredient", ia => ia.Ingredient = CreateIngredient()},
                {"Add an existing ingredient", ia => ia.Ingredient = new IngredientModel()}
            };
            FillEntity(propertiesWithString, properties, ingredientAm);
            // TODO: Multiple ias
            ingredients.Add(ingredientAm);
            return ingredients;
        }

        private static void FillEntity<TModel>(
            Dictionary<string, Action<string, TModel>> propertiesWithString,
            Dictionary<string, Action<TModel>> properties,
            TModel entity)
            where TModel : ModelBase
        {
            var choices = new List<string>();
            if (propertiesWithString != null) choices.AddRange(propertiesWithString.Keys.ToList());
            if (properties != null) choices.AddRange(properties.Keys.ToList());
            choices.Add("Done");
            var optionsHandler = new OptionsHandler(choices);
            do
            {
                var choice = optionsHandler.HandleOptions();
                if (choice == "Done") break;
                if (propertiesWithString != null && propertiesWithString.Keys.Contains(choice))
                {
                    propertiesWithString[choice](choice, entity);
                }
                else if (properties != null && properties.Keys.Contains(choice))
                {
                    properties[choice](entity);
                }
            } while (true);
        }
    }
}