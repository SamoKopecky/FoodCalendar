using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public class FilteredEntities : ConsoleHandlerBase
    {
        public FilteredEntities(IDbContextFactory dbContextFactory, int idLength) : base(dbContextFactory,
            idLength)
        {
        }

        /// <summary>
        /// Chose a filter to use on a table of models.
        /// </summary>
        public void FilterEntities()
        {
            var filters = new Dictionary<string, Action>()
            {
                {"Display days meals", FilterDays}
            };
            var keys = filters.Keys.ToList();
            keys.Add("Done");
            var optionsHandler = new OptionsHandler(keys);
            var option = optionsHandler.HandleOptions("Enter filter type:");
            if (option == "Done") return;
            filters[option]();
        }

        /// <summary>
        /// Filter the days with the given date and displays them.
        /// </summary>
        private void FilterDays()
        {
            Console.Clear();
            var repo = new DishRepository(DbContextFactory);
            bool converted;
            DateTime inputDate;
            do
            {
                Console.WriteLine("Enter date (Y/M/D): ");
                var input = Console.ReadLine();
                converted = DateTime.TryParse(input, out inputDate);
            } while (!converted);

            bool Filter(DishModel dish) => dish.DishTime.Date == inputDate.Date;
            var dishTable = Utils.GetDishTable(DbContextFactory, IdLength, Filter);
            var dishes = repo.GetAll().Where(Filter);
            dishTable.ForEach(Console.WriteLine);
            var ids = new List<Guid>();
            foreach (var dish in dishes)
            {
                dish.Meals.ToList().ForEach(meal => ids.Add(meal.Id));
            }

            var table = Utils.GetMealTable(DbContextFactory, IdLength, meal => ids.Contains(meal.Id));
            table.ForEach(Console.WriteLine);

            Console.ReadKey();
        }
    }
}