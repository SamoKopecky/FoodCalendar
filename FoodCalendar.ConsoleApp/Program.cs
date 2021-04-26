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
            var choices = new Dictionary<string, Action>
            {
                {"Add new entity", new AddNewEntity(contextFactory, IdLength).AddEntity},
                {"List an entity", new DisplayEntities(contextFactory, IdLength).DisplayEntity},
                {"Filter entities", new FilteredEntities(contextFactory, IdLength).FilterEntities},
                {"Update an entity", new UpdateEntities(contextFactory, IdLength).UpdateEntity},
                {"Delete an entity", new DeleteEntities(contextFactory, IdLength).DeleteEntity}
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