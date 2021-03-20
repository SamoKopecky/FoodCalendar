using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update;
using Xunit;
using Process = FoodCalendar.DAL.Entities.Process;

namespace FoodCalendar.DAL.Tests
{
    public class DbContextTests
    {
        static IDbContextFactory CreateDbContextFactory(StackTrace stackTrace)
        {
            var methodName = stackTrace.GetFrame(0)?.GetMethod()?.Name;
            return new InMemoryDbContextFactory(methodName);
        }

        [Fact]
        public void OneToOne_ProcessToMeal_Create()
        {
            var process = new Process() {Description = "do stuff", TimeRequired = 5};
            var meal = new Meal() {Process = process, Calories = 10};
            using (var ctx = CreateDbContextFactory(new StackTrace()).CreateDbContext())
            {
                ctx.Meals.Add(meal);
                ctx.SaveChanges();
            }

            Meal dbMeal;

            using (var ctx = CreateDbContextFactory(new StackTrace()).CreateDbContext())
            {
                dbMeal = ctx.Meals.Select(m => m)
                    .Include(m => m.Process)
                    .FirstOrDefault();
            }

            Assert.Equal(meal, dbMeal, Meal.MealComparer);
        }

        [Fact]
        public void ManyToOne_IngredientAmountToIngredient_Create()
        {
            var ingredient = new Ingredient() {Name = "egg", AmountStored = 3};
            var amountOne = new IngredientAmount() {Ingredient = ingredient, Amount = 1};
            var amountTwo = new IngredientAmount() {Ingredient = ingredient, Amount = 2};
            using (var ctx = CreateDbContextFactory(new StackTrace()).CreateDbContext())
            {
                ctx.IngredientAmounts.Add(amountOne);
                ctx.IngredientAmounts.Add(amountTwo);
                ctx.SaveChanges();
            }

            IngredientAmount dbAmountOne;
            IngredientAmount dbAmountTwo;
            using (var ctx = CreateDbContextFactory(new StackTrace()).CreateDbContext())
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
        }

        [Fact]
        public void ManyToOne_IngredientAmountToMeal_Create()
        {
            var ingredient = new Ingredient() {Name = "egg", AmountStored = 3};
            var amountOne = new IngredientAmount() {Ingredient = ingredient, Amount = 1};
            var amountTwo = new IngredientAmount() {Ingredient = ingredient, Amount = 2};
            var meal = new Meal() {IngredientsUsed = {amountOne, amountTwo}, Calories = 5};
            using (var ctx = CreateDbContextFactory(new StackTrace()).CreateDbContext())
            {
                ctx.Meals.Add(meal);
                ctx.SaveChanges();
            }

            Meal dbMeal;
            using (var ctx = CreateDbContextFactory(new StackTrace()).CreateDbContext())
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
            var dbContextFactory = CreateDbContextFactory(new StackTrace());
            var ingredientOne = new Ingredient() {Name = "egg", AmountStored = 3};
            var amountOne = new IngredientAmount() {Ingredient = ingredientOne, Amount = 1};
            var mealOne = new Meal() {IngredientsUsed = {amountOne}, Calories = 5};
            var ingredientTwo = new Ingredient() {Name = "ham", AmountStored = 4};
            var amountTwo = new IngredientAmount() {Ingredient = ingredientTwo, Amount = 1};
            var mealTwo = new Meal() {IngredientsUsed = {amountTwo}, Calories = 5};
            var dishMealOne = new List<DishMeal>() {new DishMeal() {Meal = mealOne}, new DishMeal() {Meal = mealTwo}};
            var dishOne = new Dish() {DishName = "lunch", DishMeals = dishMealOne};
            var dishMealTwo = new List<DishMeal>() {new DishMeal() {Meal = mealOne}};
            var dishTwo = new Dish() {DishName = "dinner", DishMeals = dishMealTwo};

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
                    .Include(d => d.DishMeals)
                    .ThenInclude(dm => dm.Meal)
                    .ThenInclude(m => m.IngredientsUsed)
                    .ThenInclude(ia => ia.Ingredient)
                    .FirstOrDefault(d => d.DishName == "lunch");
                dbDishTwo = ctx.Dishes.Select(d => d)
                    .Include(d => d.DishMeals)
                    .ThenInclude(dm => dm.Meal)
                    .ThenInclude(m => m.IngredientsUsed)
                    .ThenInclude(ia => ia.Ingredient)
                    .FirstOrDefault(d => d.DishName == "dinner");
            }

            Assert.Equal(dishOne, dbDishOne, Dish.DishComparer);
            Assert.Equal(dishTwo, dbDishTwo, Dish.DishComparer);
        }

        [Fact]
        public void ManyToMany_DishToDay_Create()
        {
            var dbContextFactory = CreateDbContextFactory(new StackTrace());
            var dishOne = new Dish() {DishName = "lunch"};
            var dishTwo = new Dish() {DishName = "dinner"};
            var dayDishOne = new List<DayDish>() {new DayDish() {Dish = dishOne}, new DayDish() {Dish = dishTwo}};
            var dayDishTwo = new List<DayDish>() {new DayDish() {Dish = dishOne}};
            var dayOne = new Day() {Dishes = dayDishOne, Date = new DateTime(2000, 1, 1)};
            var dayTwo = new Day() {Dishes = dayDishTwo, Date = new DateTime(2001, 1, 1)};
            using (var ctx = dbContextFactory.CreateDbContext())
            {
                ctx.Days.Add(dayOne);
                ctx.Days.Add(dayTwo);
                ctx.SaveChanges();
            }

            Day dbDayOne;
            Day dbDayTwo;
            using (var ctx = dbContextFactory.CreateDbContext())
            {
                dbDayOne = ctx.Days.Select(d => d)
                    .Include(d => d.Dishes)
                    .ThenInclude(dd => dd.Dish)
                    .ThenInclude(d => d.DayDishes)
                    .FirstOrDefault(d => d.Date == new DateTime(2000, 1, 1));
                dbDayTwo = ctx.Days.Select(d => d)
                    .Include(d => d.Dishes)
                    .ThenInclude(dd => dd.Dish)
                    .ThenInclude(d => d.DayDishes)
                    .FirstOrDefault(d => d.Date == new DateTime(2001, 1, 1));
            }

            //if (dbDayTwo != null) dbDayTwo.Dishes.First().Dish.DishName = "test";
            Assert.Equal(dayOne, dbDayOne, Day.DayComparer);
            Assert.Equal(dayTwo, dbDayTwo, Day.DayComparer);
        }
    }
}