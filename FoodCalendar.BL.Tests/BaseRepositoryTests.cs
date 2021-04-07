using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Factories;
using FoodCalendar.BL.Repositories;
using Xunit;

namespace FoodCalendar.BL.Tests
{
    public class BaseRepositoryTests
    {
        private List<DishModel> CreateTestDays()
        {
            var eggs = new IngredientModel() {Name = "egg", AmountStored = 5};
            var ham = new IngredientModel() {Name = "ham", AmountStored = 10};
            var eggsAmount = new IngredientAmountModel() {Amount = 10, Ingredient = eggs};
            var eggsAmountTwo = new IngredientAmountModel() {Amount = 15, Ingredient = eggs};
            var hamAmount = new IngredientAmountModel() {Amount = 10, Ingredient = ham};
            var mealOne = new MealModel()
            {
                Calories = 10,
                IngredientsUsed = {eggsAmount, hamAmount},
                Process = new ProcessModel()
                {
                    Description = "make em",
                    TimeRequired = 60
                }
            };
            var mealTwo = new MealModel()
            {
                Calories = 20,
                IngredientsUsed = {eggsAmountTwo, hamAmount},
                Process = new ProcessModel()
                {
                    Description = "make em good",
                    TimeRequired = 120
                }
            };
            var lunch = new DishModel()
            {
                Calories = 200,
                DishName = "lunch",
                Meals = {mealOne}
            };
            var dinner = new DishModel()
            {
                Calories = 200,
                DishName = "dinner",
                Meals = {mealTwo}
            };
            
            return new List<DishModel>() {lunch, dinner};
        }

        [Fact]
        public void InsertOrUpdate_Create()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new DishRepository(dbContextFactory);
            var day = CreateTestDays().First();

            repo.Insert(day);

            var dbDay = repo.GetAll().First();
            Assert.Equal(day, dbDay, DishModel.DishModelComparer);
        }

        [Fact]
        public void InsertOrUpdate_Update()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new DishRepository(dbContextFactory);
            var ingredientOne = new IngredientModel() {Calories = 54, Name = "eggs"};
            var ia = new IngredientAmountModel() {Amount = 2, Ingredient = ingredientOne};
            var m = new MealModel()
            {
                Calories = 2, IngredientsUsed = new List<IngredientAmountModel>() {ia},
                Process = new ProcessModel() {Description = "a"}
            };
            var dish = new DishModel()
            {
                Calories = 4, Meals = new List<MealModel>() {m}
            };

            repo.Insert(dish);


            var ingredients = repo.GetAll().First();
            ingredients.Meals.First().IngredientsUsed.First().Amount = 200;
            repo.Update(ingredients);
            Assert.Equal(ingredients, repo.GetAll().First(), DishModel.DishModelComparer);
        }

        [Fact]
        public void GetAll_Ingredient_List()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new IngredientRepository(dbContextFactory);
            var ingredientOne = new IngredientModel() {Calories = 54, Name = "eggs"};
            var ingredientTwo = new IngredientModel() {Calories = 50, Name = "ham"};

            repo.Insert(ingredientOne);
            repo.Insert(ingredientTwo);

            var ingredients = repo.GetAll();
            Assert.Contains(ingredientOne, ingredients, IngredientModel.IngredientModelComparer);
            Assert.Contains(ingredientTwo, ingredients, IngredientModel.IngredientModelComparer);
        }

        [Fact]
        public void DeleteById_Ingredient_One()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new IngredientRepository(dbContextFactory);
            var id = new Guid("a12ef0f4-babf-43be-aaee-3c009b3372bd");
            var ingredient = new IngredientModel() {Calories = 54, Name = "eggs", Id = id};

            repo.Insert(ingredient);
            repo.Delete(id);

            var ingredients = repo.GetAll();
            Assert.Empty(ingredients);
        }

        [Fact]
        public void DeleteByModel_Ingredient_One()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new IngredientRepository(dbContextFactory);
            var id = new Guid("a12ef0f4-babf-43be-aaee-3c009b3372bd");
            var ingredient = new IngredientModel() {Calories = 54, Name = "eggs", Id = id};

            repo.Insert(ingredient);
            repo.Delete(ingredient);

            var ingredients = repo.GetAll();
            Assert.Empty(ingredients);
        }
    }
}