using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Seeds;
using MovieCatalog.DAL.Factories;

namespace FoodCalendar.App
{
    public class Program
    {


        public static void Main(string[] args)
        {

            SeedDb();
            //var dbContextFactory = new DbContextFactory();
            //var repo = new DishRepository(dbContextFactory);
            //var ingredientOne = new IngredientModel() { Calories = 54, Name = "eggs" };
            //var ia = new IngredientAmountModel() { Amount = 2, Ingredient = ingredientOne };
            //var m = new MealModel()
            //{
            //    Calories = 2,
            //    IngredientsUsed = new List<IngredientAmountModel>() { ia },
            //    Process = new ProcessModel() { Description = "a" }
            //};
            //var dish = new DishModel()
            //{
            //    Calories = 4,
            //    Meals = new List<MealModel>() { m }
            //};

            //repo.Insert(dish);


            //var ingredients = repo.GetAll().First();
            //ingredients.Meals.First().IngredientsUsed.First().Amount = 200;
            //repo.Update(ingredients);
        }

        public static void SeedDb()
        {
            var dbContextFactory = new DbContextFactory();
            using var ctx = dbContextFactory.CreateDbContext();
            var eggs = IngredientSeed.Egg;
            var ham = IngredientSeed.Ham;
            var hamAmount = new IngredientAmount() {Amount = 2, Ingredient = ham};
            var eggAmount = new IngredientAmount() {Amount = 1, Ingredient = eggs};
            var process = ProcessSeed.HamAndEggsProcess;

            var hamAndEggs = MealSeed.HamAndEggs;
            hamAndEggs.IngredientsUsed.Add(eggAmount);
            hamAndEggs.IngredientsUsed.Add(hamAmount);
            hamAndEggs.Process = process;

            var lunch = DishSeed.Lunch;
            lunch.Meals.Add(hamAndEggs);

            ctx.Dishes.Add(lunch);
            ctx.SaveChanges();
        }
    }
}