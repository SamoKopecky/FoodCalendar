using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Seeds;
using Microsoft.EntityFrameworkCore.Internal;

namespace FoodCalendar.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //SeedDb();
            var dbContextFactory = new DbContextFactory();
            using var ctx = dbContextFactory.CreateDbContext();

            var all2 = ctx.Days.FirstOrDefault();
            var repo = new DayRepository(new DbContextFactory());
            var all = repo.GetAll();
        }

        public static void SeedDb()
        {
            var dbContextFactory = new DbContextFactory();
            using (var ctx = dbContextFactory.CreateDbContext())
            {
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
                lunch.DishMeals.Add(new DishMeal()
                {
                    Meal = hamAndEggs
                });

                var monday = DaySeed.Monday;
                monday.Dishes.Add(new DayDish()
                {
                    Dish = lunch
                });
                var test = DayMapper.MapEntityToModel(monday);
                var test2 = DayMapper.MapModelToEntity(test);

                ctx.Add(test2);
                ctx.SaveChanges();
                var test3 = ctx.Days.FirstOrDefault();
                Console.WriteLine("now way this prints");
                var test4 = DayMapper.MapEntityToModel(test3);
                Console.WriteLine("now way this prints");
            }
        }
    }
}