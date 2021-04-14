using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.ConsoleApp.EntitiesSynchronization;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public class AddNewEntity : ConsoleHandlerBase
    {
        public AddNewEntity(IDbContextFactory dbContextFactory, int idLength) : base(dbContextFactory, idLength)
        {
        }

        /// <summary>
        /// Generate a menu to add a new entity. Correct create functions
        /// are then called.
        /// </summary>
        public void AddEntity()
        {
            var entities = new Dictionary<string, Func<ModelBase>>()
            {
                {"Ingredient", () => CreateIngredient(new IngredientModel())},
                {"Meal", () => CreateMeal(new MealModel())},
                {"Dish", () => CreateDish(new DishModel())}
            };
            var choices = entities.Keys.ToList();
            choices.Add("Done");
            var optionHandler = new OptionsHandler(choices);
            var option = optionHandler.HandleOptions("Choosing entity");
            if (option == "Done") return;
            var createFunction = entities[option];
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

        /// <summary>
        /// Create or update dish model. To create a new dish call function with empty model.
        /// </summary>
        /// <param name="dish">Dish to updated/created.</param>
        /// <returns>Created/updated model.</returns>
        public DishModel CreateDish(DishModel dish)
        {
            var propertiesWithString = new Dictionary<string, Action<string, DishModel>>()
            {
                {"Dish Name", (s, d) => d.DishName = Utils.ScanProperty<string>(s)},
                {"Dish Time (Y/M/D H:M:S)", (s, d) => d.DishTime = Utils.ScanProperty<DateTime>(s)},
            };
            var properties = new Dictionary<string, Action<DishModel>>()
            {
                {"Create and add a new meal", d => d.Meals.Add(CreateMeal(new MealModel(), true))},
                {"Add an existing meal", d => d.Meals.Add(AddExistingMeal())}
            };
            FillEntity(propertiesWithString, properties, dish, "Adding dish");
            return dish;
        }

        /// <summary>
        /// Create or update meal model. To create a new meal call function with empty model.
        /// </summary>
        /// <param name="meal">Meal to updated/created.</param>
        /// <returns>Created/updated model.</returns>
        public MealModel CreateMeal(MealModel meal, bool implicitCreation = false)
        {
            meal.Process ??= new ProcessModel();
            var propertiesWithString = new Dictionary<string, Action<string, MealModel>>()
            {
                {"Meal Name", (s, m) => m.MealName = Utils.ScanProperty<string>(s)},
            };
            var properties = new Dictionary<string, Action<MealModel>>()
            {
                {"Add process", m => m.Process = CreateProcess()},
                {"Add ingredient amount", m => m.IngredientsUsed.Add(CreateIngredientUsed())}
            };
            FillEntity(propertiesWithString, properties, meal, "Adding meal");
            // Needed when a meal is created implicitly by the creation of a dish
            if (!implicitCreation) return meal;
            var repo = new MealRepository(DbContextFactory);
            meal = repo.Insert(meal);
            return meal;
        }

        /// <summary>
        /// Create or update ingredient model. To create a new ingredient call function with empty model.
        /// </summary>
        /// <param name="ingredient">ingredient to updated/created.</param>
        /// <returns>Created/updated model.</returns>
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

        /// <summary>
        /// Create a new process model.
        /// </summary>
        /// <returns>Created process model.</returns>
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

        /// <summary>
        /// Create a list of ingredient amount models.
        /// </summary>
        /// <returns>List of created models.</returns>
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

        /// <summary>
        /// Choose which ingredient to get from a
        /// table of existing ingredients in the Database.
        /// </summary>
        /// <returns></returns>
        private IngredientModel AddExistingIngredient()
        {
            var repo = new IngredientRepository(DbContextFactory);
            var table = Utils.GetIngredientTable(DbContextFactory, IdLength);
            return Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Ingredient", IdLength);
        }

        /// <summary>
        /// Choose which meal to get from a table of existing meals in the Database.
        /// Since there is no many-to-many relation between dish and meal entity a new
        /// model is created each time a meal is going to be reused.
        /// </summary>
        /// <returns>A copy of an existing meal.</returns>
        private MealModel AddExistingMeal()
        {
            var repo = new MealRepository(DbContextFactory);
            var table = Utils.GetMealTable(DbContextFactory, IdLength);
            var meal = Utils.GetExistingEntity(repo.GetAll().ToList(), table, "Meal", IdLength);
            // Reset the ids so that a new meal can be inserted
            meal.Id = Guid.Empty;
            meal.Process.Id = Guid.Empty;
            meal.IngredientsUsed.ToList().ForEach(ia => ia.Id = Guid.Empty);
            return meal;
        }

        /// <summary>
        /// Generic function for filing a model. The properties of a model
        /// are filled with the anonymous functions from parameter properties and
        /// propertiesWithString.
        /// </summary>
        /// <typeparam name="TModel">Specific type of the model</typeparam>
        /// <param name="propertiesWithString">Functions for filling simple properties</param>
        /// <param name="properties">Functions for filling properties of relations</param>
        /// <param name="entity">Model to be filled.</param>
        /// <param name="actionDescription">String description of the model to be filled.</param>
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

            Synchronization.SynchronizeEntity(entity, DbContextFactory);
        }
    }
}