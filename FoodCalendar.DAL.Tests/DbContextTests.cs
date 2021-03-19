using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Interfaces;
using Xunit;
namespace FoodCalendar.DAL.Tests
{
    
    public class DbContextTests
    {
        private readonly IDbContextFactory _ctxFactory = new InMemoryDbContextFactory();

        [Fact]
        public void OneToOne_ProcessToMeal_Create()
        {
            using var ctx = _ctxFactory.CreateDbContext();
            var process = new Process() {Description = "do stuff", TimeRequired = 5};
            var meal = new Meal() {Process = process, Calories = 10};

            ctx.Meals.Add(meal);
            ctx.SaveChanges();

            var dbProcess = ctx.Processes.FirstOrDefault();
            var dbMeal = ctx.Meals.FirstOrDefault();
            Assert.Equal(process, dbProcess, Process.ProcessComparer);
            Assert.Equal(meal, dbMeal, Meal.MealComparer);
        }

        [Fact]
        public void ManyToOne_IngredientAmountToIngredient_Create()
        {
            using var ctx = _ctxFactory.CreateDbContext();
            var ingredient = new Ingredient() {Name = "egg", AmountStored = 3};
            var amountOne = new IngredientAmount() {Ingredient = ingredient, Amount = 1};
            var amountTwo = new IngredientAmount() {Ingredient = ingredient, Amount = 2};

            ctx.IngredientAmounts.Add(amountOne);
            ctx.IngredientAmounts.Add(amountTwo);
            ctx.SaveChanges();

            var dbAmountOne = ctx.IngredientAmounts.FirstOrDefault(ia => ia.Amount == 1);
            var dbAmountTwo = ctx.IngredientAmounts.FirstOrDefault(ia => ia.Amount == 2);
            Assert.Equal(amountOne, dbAmountOne, IngredientAmount.IngredientAmountComparer);
            Assert.Equal(amountTwo, dbAmountTwo, IngredientAmount.IngredientAmountComparer);
        }

        [Fact]
        public void ManyToOne_IngredientAmountToMeal_Create()
        {
            using var ctx = _ctxFactory.CreateDbContext();
            var ingredient = new Ingredient() {Name = "egg", AmountStored = 3};
            var amountOne = new IngredientAmount() {Ingredient = ingredient, Amount = 1};
            var amountTwo = new IngredientAmount() {Ingredient = ingredient, Amount = 2};
            var meal = new Meal() {IngredientsUsed = {amountOne, amountTwo}, Calories = 5};

            ctx.Meals.Add(meal);
            ctx.SaveChanges();

            var dbMeal = ctx.Meals.FirstOrDefault();
            Assert.Equal(meal, dbMeal, Meal.MealComparer);
        }

        [Fact]
        public void ManyToMany_MealToDish_Create()
        {
            using var ctx = _ctxFactory.CreateDbContext();
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

            ctx.Dishes.Add(dishTwo);
            ctx.Dishes.Add(dishOne);
            ctx.SaveChanges();

            var dbDishOne = ctx.Dishes.FirstOrDefault(d => d.DishName == "lunch");
            var dbDishTwo = ctx.Dishes.FirstOrDefault(d => d.DishName == "dinner");

            Assert.Equal(dishOne, dbDishOne, Dish.DishComparer);
            Assert.Equal(dishTwo, dbDishTwo, Dish.DishComparer);
        }

        [Fact]
        public void ManyToMany_DishToDay_Create()
        {
            using var ctx = _ctxFactory.CreateDbContext();
            var dishOne = new Dish() {DishName = "lunch"};
            var dishTwo = new Dish() {DishName = "dinner"};
            var dayDishOne = new List<DayDish>() {new DayDish() {Dish = dishOne}, new DayDish() {Dish = dishTwo}};
            var dayDishTwo = new List<DayDish>() {new DayDish() {Dish = dishOne}};
            var dayOne = new Day() {Dishes = dayDishOne, Date = new DateTime(2000, 1, 1)};
            var dayTwo = new Day() {Dishes = dayDishTwo, Date = new DateTime(2001, 1, 1)};

            ctx.Days.Add(dayOne);
            ctx.Days.Add(dayTwo);
            ctx.SaveChanges();

            var dbDayOne = ctx.Days.FirstOrDefault(d => d.Date == new DateTime(2000, 1, 1));
            var dbDayTwo = ctx.Days.FirstOrDefault(d => d.Date == new DateTime(2001, 1, 1));
            Assert.Equal(dayOne, dbDayOne, Day.DayComparer);
            Assert.Equal(dayTwo, dbDayTwo, Day.DayComparer);
        }
    }
}