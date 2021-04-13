using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Process = FoodCalendar.DAL.Entities.Process;

namespace FoodCalendar.DAL.Tests
{
    public class DbContextTests
    {
        [Fact]
        public void OneToOne_ProcessToMeal_Create()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var process = new Process() {Description = "do stuff", TimeRequired = 5};
            var meal = new Meal() {Process = process, Calories = 10};
            using (var ctx = dbContextFactory.CreateDbContext())
            {
                ctx.Meals.Add(meal);
                ctx.SaveChanges();
            }

            Meal dbMeal;

            using (var ctx = dbContextFactory.CreateDbContext())
            {
                dbMeal = ctx.Meals.Select(m => m)
                    .Include(m => m.Process)
                    .FirstOrDefault();
            }

            Assert.Equal(meal, dbMeal, Meal.MealComparer);
            Assert.Equal(meal.Process, dbMeal?.Process, Process.ProcessComparer);
        }

        [Fact]
        public void ManyToOne_IngredientAmountToIngredient_Create()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var ingredient = new Ingredient() {Name = "egg", AmountStored = 3};
            var amountOne = new IngredientAmount() {Ingredient = ingredient, Amount = 1};
            var amountTwo = new IngredientAmount() {Ingredient = ingredient, Amount = 2};
            using (var ctx = dbContextFactory.CreateDbContext())
            {
                ctx.IngredientAmounts.Add(amountOne);
                ctx.IngredientAmounts.Add(amountTwo);
                ctx.SaveChanges();
            }

            IngredientAmount dbAmountOne;
            IngredientAmount dbAmountTwo;
            using (var ctx = dbContextFactory.CreateDbContext())
            {
                dbAmountOne = ctx.IngredientAmounts.Select(ia => ia)
                    .Include(ia => ia.Ingredient)
                    .FirstOrDefault(ia => ia.Amount == 1);
                dbAmountTwo = ctx.IngredientAmounts.Select(ia => ia)
                    .Include(ia => ia.Ingredient)
                    .FirstOrDefault(ia => ia.Amount == 2);
            }

            Assert.Equal(amountOne, dbAmountOne, IngredientAmount.IngredientAmountComparer);
            Assert.Equal(amountTwo, dbAmountTwo, IngredientAmount.IngredientAmountComparer);
            Assert.Equal(amountOne.Ingredient, dbAmountOne?.Ingredient, Ingredient.IngredientComparer);
        }

        [Fact]
        public void ManyToOne_IngredientAmountToMeal_Create()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var ingredient = new Ingredient() {Name = "egg", AmountStored = 3};
            var amountOne = new IngredientAmount() {Ingredient = ingredient, Amount = 1};
            var amountTwo = new IngredientAmount() {Ingredient = ingredient, Amount = 2};
            var meal = new Meal() {IngredientsUsed = {amountOne, amountTwo}, Calories = 5};
            using (var ctx = dbContextFactory.CreateDbContext())
            {
                ctx.Meals.Add(meal);
                ctx.SaveChanges();
            }

            Meal dbMeal;
            using (var ctx = dbContextFactory.CreateDbContext())
            {
                dbMeal = ctx.Meals.Select(m => m)
                    .Include(m => m.IngredientsUsed)
                    .ThenInclude(ia => ia.Ingredient)
                    .FirstOrDefault();
            }

            Assert.Equal(meal, dbMeal, Meal.MealComparer);
        }

        [Fact]
        public void ManyToMany_MealToDish_Create()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var ingredientOne = new Ingredient() {Name = "egg", AmountStored = 3};
            var amountOne = new IngredientAmount() {Ingredient = ingredientOne, Amount = 1};
            var mealOne = new Meal() {IngredientsUsed = {amountOne}, Calories = 5};
            var ingredientTwo = new Ingredient() {Name = "ham", AmountStored = 4};
            var amountTwo = new IngredientAmount() {Ingredient = ingredientTwo, Amount = 1};
            var mealTwo = new Meal() {IngredientsUsed = {amountTwo}, Calories = 5};
            var dishMealOne = new List<Meal>() {mealOne, mealTwo};
            var dishOne = new Dish() {DishName = "lunch", Meals = dishMealOne};
            var dishMealTwo = new List<Meal>() {mealOne};
            var dishTwo = new Dish() {DishName = "dinner", Meals = dishMealTwo};

            using (var ctx = dbContextFactory.CreateDbContext())
            {
                ctx.Dishes.Add(dishTwo);
                ctx.Dishes.Add(dishOne);
                ctx.SaveChanges();
            }

            Dish dbDishOne;
            Dish dbDishTwo;

            using (var ctx = dbContextFactory.CreateDbContext())
            {
                dbDishOne = ctx.Dishes.Select(d => d)
                    .Include(d => d.Meals)
                    .ThenInclude(m => m.IngredientsUsed)
                    .ThenInclude(ia => ia.Ingredient)
                    .FirstOrDefault(d => d.DishName == "lunch");
                dbDishTwo = ctx.Dishes.Select(d => d)
                    .Include(d => d.Meals)
                    .ThenInclude(m => m.IngredientsUsed)
                    .ThenInclude(ia => ia.Ingredient)
                    .FirstOrDefault(d => d.DishName == "dinner");
            }

            Assert.Equal(dishOne, dbDishOne, Dish.DishComparer);
            Assert.Equal(dishTwo, dbDishTwo, Dish.DishComparer);
        }

        [Fact]
        public void None_Ingredient_Create()
        {
            var dbContextFactory = new InMemoryDbContextFactory(new StackTrace());
            var ingredient = new Ingredient() {Name = "egg", AmountStored = 3};

            using (var ctx = dbContextFactory.CreateDbContext())
            {
                ctx.Add(ingredient);
                ctx.SaveChanges();
            }

            Ingredient dbIngredient;

            using (var ctx = dbContextFactory.CreateDbContext())
            {
                dbIngredient = ctx.Ingredients.Select(i => i).FirstOrDefault();
            }

            Assert.Equal(ingredient, dbIngredient, Ingredient.IngredientComparer);
        }
    }
}