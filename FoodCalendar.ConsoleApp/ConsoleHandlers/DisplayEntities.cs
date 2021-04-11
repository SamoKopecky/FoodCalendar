using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public class DisplayEntities : ConsoleHandler
    {
        public DisplayEntities(IDbContextFactory dbContextFactory, int idLength) : base(dbContextFactory, idLength)
        {
        }

        public void DisplayEntity()
        {
            var entities = new Dictionary<string, Action>()
            {
                {"Ingredient", DisplayIngredients},
                {"Meal", DisplayMeals},
                {"Dish", DisplayDishes},
                {"All", DisplayAllEntities}
            };
            var optionsHandler = new OptionsHandler(entities.Keys.ToList());
            var option = optionsHandler.HandleOptions("Choose entities to display");
            Console.Clear();
            entities[option]();
            Console.ReadKey();
        }

        private void DisplayAllEntities()
        {
            DisplayIngredients();
            Console.WriteLine();
            DisplayMeals();
            Console.WriteLine();
            DisplayDishes();
        }

        private void DisplayIngredients()
        {
            Console.WriteLine("Ingredients:");
            var table = Utils.GetIngredientTable(DbContextFactory, IdLength);
            table.ForEach(Console.WriteLine);
        }

        private void DisplayMeals()
        {
            Console.WriteLine("Meals:");
            var table = Utils.GetMealTable(DbContextFactory, IdLength);
            table.ForEach(Console.WriteLine);
        }

        private void DisplayDishes()
        {
            Console.WriteLine("Dishes:");
            var table = Utils.GetDishTable(DbContextFactory, IdLength);
            table.ForEach(Console.WriteLine);
        }
    }
}