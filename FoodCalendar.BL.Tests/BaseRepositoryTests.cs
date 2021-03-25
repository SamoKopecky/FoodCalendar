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
        private List<DayModel> CreateTestDays()
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
            var monday = new DayModel()
            {
                CaloriesLimit = 5000,
                Dishes = {dinner, lunch}
            };
            var sunday = new DayModel()
            {
                CaloriesLimit = 6000,
                Dishes = {dinner}
            };
            return new List<DayModel>() {monday, sunday};
        }

        [Fact]
        public void InsertOrUpdate_Create()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new DayRepository(dbContextFactory);
            var day = CreateTestDays().First();

            repo.InsertOrUpdate(day);

            var dbDay = repo.GetAll().First();
            Assert.Equal(day, dbDay, DayModel.DayModelComparer);
        }

        [Fact]
        public void InsertOrUpdate_Update()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new DayRepository(dbContextFactory);
            var day = CreateTestDays().First();
            day.Id = new Guid("d49e0b93-c041-4c91-973a-ac2bd58a596e");

            repo.InsertOrUpdate(day);
            day = repo.GetById(day.Id);
            day.CaloriesSum = 50;
            repo.InsertOrUpdate(day);

            var dbDay = repo.GetAll().First();
            Assert.Equal(day, dbDay, DayModel.DayModelComparer);
        }

        [Fact]
        public void GetAll_Ingredient_List()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var repo = new IngredientRepository(dbContextFactory);
            var ingredientOne = new IngredientModel() {Calories = 54, Name = "eggs"};
            var ingredientTwo = new IngredientModel() {Calories = 50, Name = "ham"};

            repo.InsertOrUpdate(ingredientOne);
            repo.InsertOrUpdate(ingredientTwo);

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

            repo.InsertOrUpdate(ingredient);
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

            repo.InsertOrUpdate(ingredient);
            repo.Delete(ingredient);

            var ingredients = repo.GetAll();
            Assert.Empty(ingredients);
        }
    }
}