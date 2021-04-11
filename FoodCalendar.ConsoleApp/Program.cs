using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Repositories;
using FoodCalendar.ConsoleApp.ConsoleHandlers;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Seeds;

namespace FoodCalendar.ConsoleApp
{
    public class Program
    {
        private const int IdLength = 8;

        public static void Main(string[] args)
        {
            //SeedDb();
            var contextFactory = new DbContextFactory();
            var addingOptions = new AddingOptions(contextFactory, IdLength);
            var entityTables = new EntityTables(contextFactory, IdLength);
            var choices = new Dictionary<string, Action>()
            {
                {"Add new entity", addingOptions.AddEntity},
                {"List an entity", entityTables.PrintEntity},
                {"List other information", () => { }},
                {"Update an entity", () => { }},
                {"Delete an entity", () => { }}
            };
            var choicesList = choices.Keys.ToList();
            choicesList.Add("Done");
            var optionHandler = new OptionsHandler(choicesList);
            do
            {
                var choice = optionHandler.HandleOptions("Choosing action");
                if (choice == "Done") break;
                choices[choice]();
            } while (true);
        }

        public static void SeedDb()
        {
            var dbContextFactory = new DbContextFactory();
            using var ctx = dbContextFactory.CreateDbContext();
            var repo = new DishRepository(dbContextFactory);
            var lunch = DishSeed.Lunch;
            repo.Insert(DishMapper.MapEntityToModel(lunch));
        }
    }
}