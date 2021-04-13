using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.ConsoleApp.DbSynchronization;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public class AddNewEntity : ConsoleHandlerBase
    {
        public AddNewEntity(IDbContextFactory dbContextFactory, int idLength) : base(dbContextFactory, idLength)
        {
        }

        public void AddEntity()
        {
            var entities = new Dictionary<string, Func<ModelBase>>()
            {
                {"Ingredient", () => CreateIngredient(new IngredientModel())},
                {"Meal", () => CreateMeal(new MealModel())},
                {"Dish", () => CreateDish(new DishModel())}
            };
            var optionHandler = new OptionsHandler(entities.Keys.ToList());
            var createFunction = entities[optionHandler.HandleOptions("Choosing entity")];
            ModelBase entity;
            try
            {
                entity = createFunction();
            }
            catch (Exception e)
            {
                Utils.DisplayError(e);
                return;
            }

            if (entity == null) return;

            switch (entity)
            {
                case IngredientModel ingredient:
                    new IngredientRepository(DbContextFactory).Insert(ingredient);
                    break;
                case MealModel meal:
                    new MealRepository(DbContextFactory).Insert(meal);
                    break;
                case DishModel dish:
                    new DishRepository(DbContextFactory).Insert(dish);
                    break;
            }
        }

        public DishModel CreateDish(DishModel dish)
        {
            var propertiesWithString = new Dictionary<string, Action<string, DishModel>>()
            {
                {"Total Time", (s, d) => d.TotalTime = Utils.ScanProperty<int>(s)},
                {"Dish Name", (s, d) => d.DishName = Utils.ScanProperty<string>(s)},
                {"Dish Time (Y/M/D H:M:S)", (s, d) => d.DishTime = Utils.ScanProperty<DateTime>(s)},
                {"Calories", (s, d) => d.Calories = Utils.ScanProperty<int>(s)},
            };
            var properties = new Dictionary<string, Action<DishModel>>()
            {
                {"Create and add a new meal", d => d.Meals.Add(CreateMeal(new MealModel()))},
                {"Add an existing meal", d => d.Meals.Add(AddExistingMeal())}
            };
            FillEntity(propertiesWithString, properties, dish, "Adding dish");
            return dish;
        }

        public MealModel CreateMeal(MealModel meal)
        {
            meal.Process ??= new ProcessModel();
            var propertiesWithString = new Dictionary<string, Action<string, MealModel>>()
            {
                {"Dish Name", (s, m) => m.MealName = Utils.ScanProperty<string>(s)},
                {"Calories", (s, m) => m.Calories = Utils.ScanProperty<int>(s)},
            };
            var properties = new Dictionary<string, Action<MealModel>>()
            {
                {"Add process", m => m.Process = CreateProcess()},
                {"Add ingredient amount", m => m.IngredientsUsed.Add(CreateIngredientUsed())}
            };
            FillEntity(propertiesWithString, properties, meal, "Adding meal");

            return meal;
        }

        public IngredientModel CreateIngredient(IngredientModel ingredient)
        {
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
                {"Create and add new ingredient", ia => ia.Ingredient = CreateIngredient(new IngredientModel())},
                {"Add an existing ingredient", ia => ia.Ingredient = AddExistingIngredient()}
            };
            FillEntity(propertiesWithString, properties, ingredientAm, "Adding ingredient amount");
            return ingredientAm;
        }

        private IngredientModel AddExistingIngredient()
        {
            var repo = new IngredientRepository(DbContextFactory);
            var table = Utils.GetIngredientTable(DbContextFactory, IdLength);
            return Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Ingredient", IdLength);
        }

        private MealModel AddExistingMeal()
        {
            var repo = new MealRepository(DbContextFactory);
            var table = Utils.GetMealTable(DbContextFactory, IdLength);
            var meal = Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Meal", IdLength);
            meal.Id = Guid.Empty;
            meal.Process.Id = Guid.Empty;
            meal.IngredientsUsed.ToList().ForEach(ia => ia.Id = Guid.Empty);
            return meal;
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

            Synchronization.SynchronizeEntity(entity);
        }
    }
}