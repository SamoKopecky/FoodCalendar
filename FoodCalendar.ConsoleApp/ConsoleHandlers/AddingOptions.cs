﻿using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public class AddingOptions
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly int _idLength;

        public AddingOptions(IDbContextFactory dbContextFactory, int idLength)
        {
            _dbContextFactory = dbContextFactory;
            _idLength = idLength;
        }

        public void AddEntity()
        {
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
                    new IngredientRepository(_dbContextFactory).Insert(ingredient);
                    break;
                case MealModel meal:
                    new MealRepository(_dbContextFactory).Insert(meal);
                    break;
                case DishModel dish:
                    new DishRepository(_dbContextFactory).Insert(dish);
                    break;
            }
        }

        private DishModel CreateDish()
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
                {"Add an existing meal", d => d.Meals.Add(AddExistingMeal())}
            };
            FillEntity(propertiesWithString, properties, dish, "Adding dish");
            return dish;
        }

        private MealModel CreateMeal()
        {
            var meal = new MealModel()
            {
                Process = new ProcessModel()
            };
            var propertiesWithString = new Dictionary<string, Action<string, MealModel>>()
            {
                {"Dish Name", (s, m) => m.MealName = Utils.ScanProperty<string>(s)},
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

        private IngredientModel CreateIngredient()
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

        private ProcessModel CreateProcess()
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

        private IngredientAmountModel CreateIngredientUsed()
        {
            var ingredientAm = new IngredientAmountModel();
            var propertiesWithString = new Dictionary<string, Action<string, IngredientAmountModel>>()
            {
                {"Amount", (s, ia) => ia.Amount = Utils.ScanProperty<int>(s)}
            };
            var properties = new Dictionary<string, Action<IngredientAmountModel>>()
            {
                {"Create and add new ingredient", ia => ia.Ingredient = CreateIngredient()},
                {"Add an existing ingredient", ia => ia.Ingredient = AddExistingIngredient()}
            };
            FillEntity(propertiesWithString, properties, ingredientAm, "Adding ingredient amount");
            return ingredientAm;
        }

        private IngredientModel AddExistingIngredient()
        {
            var entityTables = new EntityTables(_dbContextFactory, _idLength);
            var repo = new IngredientRepository(_dbContextFactory);
            var table = entityTables.GetIngredientTable();
            return Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Ingredient", _idLength);
        }

        private MealModel AddExistingMeal()
        {
            var entityTables = new EntityTables(_dbContextFactory, _idLength);
            var repo = new MealRepository(_dbContextFactory);
            var table = entityTables.GetMealTable();
            return Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Meal", _idLength);
        }


        private void FillEntity<TModel>(
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