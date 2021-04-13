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
            var addEntity = new AddNewEntity(contextFactory, IdLength);
            var displayEntities = new DisplayEntities(contextFactory, IdLength);
            var filteredEntities = new FilteredEntities(contextFactory, IdLength);
            var deleteEntities = new DeleteEntities(contextFactory, IdLength);
            var updateEntities = new UpdateEntities(contextFactory, IdLength);
            var choices = new Dictionary<string, Action>
            {
                {"Add new entity", addEntity.AddEntity},
                {"List an entity", displayEntities.DisplayEntity},
                {"Filter entities", filteredEntities.FilterEntities},
                {"Update an entity", updateEntities.UpdateEntity},
                {"Delete an entity", deleteEntities.DeleteEntity}
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