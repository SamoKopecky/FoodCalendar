using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.ConsoleApp.ConsoleHandlers;
using FoodCalendar.DAL.Entities;
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
            var createFunction = entities[optionHandler.HandleOptions("Choosing entity")];
            var entity = createFunction();
            if (entity == null) return;

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

        private static DishModel CreateDish()
        {
            var dish = new DishModel();
            var propertiesWithString = new Dictionary<string, Action<string, DishModel>>()
            {
                {"Total Time", (s, d) => d.TotalTime = Utils.ScanProperty<int>(s)},
                {"Dish Name", (s, d) => d.DishName = Utils.ScanProperty<string>(s)},
                {"Dish Time (Y/M/D H:M:S)", (s, d) => d.DishTime = Utils.ScanProperty<DateTime>(s)},
                {"Calories", (s, d) => d.Calories = Utils.ScanProperty<int>(s)},
            };
            var properties = new Dictionary<string, Action<DishModel>>()
            {
                {"Create and add a new meal", d => d.Meals.Add(CreateMeal())},
                {"Add an existing meal", d => d.Meals.Add(CreateMeal())}
            };
            // TODO: Function for adding an existing meal
            FillEntity(propertiesWithString, properties, dish, "Adding dish");
            return dish;
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
                {"Add process", m => m.Process = CreateProcess()},
                {"Add ingredient amount", m => m.IngredientsUsed.Add(CreateIngredientUsed())}
            };
            FillEntity(propertiesWithString, properties, meal, "Adding meal");

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
            FillEntity(properties, null, ingredient, "Adding ingredient");
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

            FillEntity(properties, null, process, "Adding process");
            return process;
        }

        private static IngredientAmountModel CreateIngredientUsed()
        {
            var ingredientAm = new IngredientAmountModel();
            var propertiesWithString = new Dictionary<string, Action<string, IngredientAmountModel>>()
            {
                {"Amount", (s, ia) => ia.Amount = Utils.ScanProperty<int>(s)}
            };
            var properties = new Dictionary<string, Action<IngredientAmountModel>>()
            {
                {"Create and add new ingredient", ia => ia.Ingredient = CreateIngredient()},
                {"Add an existing ingredient", ia => ia.Ingredient = new IngredientModel()}
            };
            FillEntity(propertiesWithString, properties, ingredientAm, "Adding ingredient amount");
            // TODO: Function for adding an existing ingredient
            return ingredientAm;
        }

        private static void FillEntity<TModel>(
            Dictionary<string, Action<string, TModel>> propertiesWithString,
            Dictionary<string, Action<TModel>> properties,
            TModel entity,
            string actionDescription
        )
            where TModel : ModelBase
        {
            var choices = new List<string>();
            if (propertiesWithString != null) choices.AddRange(propertiesWithString.Keys.ToList());
            if (properties != null) choices.AddRange(properties.Keys.ToList());
            choices.Add("Done");
            var optionsHandler = new OptionsHandler(choices);
            do
            {
                var choice = optionsHandler.HandleOptions(actionDescription);
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