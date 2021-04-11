using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Factories;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public class EntityTables
    {
        public static void PrintEntity()
        {
            var entities = new Dictionary<string, Action>()
            {
                {"Ingredient", PrintIngredients},
                {"Meal", PrintMeals},
                {"Dish", PrintDishes},
                {"All", PrintAllEntities}
            };
            var optionsHandler = new OptionsHandler(entities.Keys.ToList());
            var option = optionsHandler.HandleOptions("Choose entities to display");
            Console.Clear();
            entities[option]();
            Console.ReadKey();
        }

        private static void PrintAllEntities()
        {
            PrintIngredients();
            Console.WriteLine();
            PrintMeals();
            Console.WriteLine();
            PrintDishes();
        }

        public static void PrintIngredients()
        {
            Console.WriteLine("Ingredients:");
            var dbContextFactory = new DbContextFactory();
            var headersToValues = new Dictionary<string, Func<IngredientModel, string>>()
            {
                {"Name", i => i.Name},
                {"Amount Stored", i => $"{i.AmountStored}"},
                {"Unit Name", i => i.UnitName},
                {"Calories", i => $"{i.Calories}"}
            };
            var repo = new IngredientRepository(dbContextFactory);
            var table = GenericTable(repo.GetAll().ToList(), headersToValues);
            table.ForEach(Console.WriteLine);
        }

        public static void PrintMeals()
        {
            Console.WriteLine("Meals:");
            var dbContextFactory = new DbContextFactory();
            var headersToValues = new Dictionary<string, Func<MealModel, string>>()
            {
                {"Meal Name", m => $"{m.MealName}"},
                {"Total Time", m => $"{m.TotalTime}"},
                {"Calories", m => $"{m.Calories}"},
                {"Process: Time Required", m => $"{m.Process.TimeRequired}"},
                {"Process: Description", m => m.Process.Description},
                {
                    "Ingredient amounts", m => ListToString(
                        m.IngredientsUsed,
                        amountModel => $"{amountModel.Ingredient.Name}: {amountModel.Amount}"
                    )
                }
            };
            var repo = new MealRepository(dbContextFactory);
            var table = GenericTable(repo.GetAll().ToList(), headersToValues);
            table.ForEach(Console.WriteLine);
        }

        public static void PrintDishes()
        {
            Console.WriteLine("Dishes:");
            var dbContextFactory = new DbContextFactory();
            var headersToValues = new Dictionary<string, Func<DishModel, string>>()
            {
                {"Dish Name", d => d.DishName},
                {"Dish Time (Y/M/D H:M:S)", d => $"{d.DishTime}"},
                {"Total Time", d => $"{d.TotalTime}"},
                {"Calories", d => $"{d.Calories}"},
                {
                    "Meals", m => ListToString(
                        m.Meals,
                        meal => $"{meal.MealName}"
                    )
                },
            };
            var repo = new DishRepository(dbContextFactory);
            var table = GenericTable(repo.GetAll().ToList(), headersToValues);
            table.ForEach(Console.WriteLine);
        }

        private static string ListToString<T>(IEnumerable<T> list, Func<T, string> format)
        {
            var formattedList = list.Select(format);
            return string.Join(", ", formattedList);
        }

        private static List<string> GenericTable<T>(
            List<T> entities,
            Dictionary<string, Func<T, string>> variables
        )
            where T : ModelBase
        {
            var dataRows = new List<string>();
            var table = new string[variables.Count, entities.Count + 1];
            var columnLengths = new List<int>();
            var keys = variables.Keys.ToList();
            for (int i = 0; i < variables.Count; i++)
            {
                var lengths = new HashSet<int>();
                // Header name
                var name = keys[i];
                table[i, 0] = name;
                // Header string length
                lengths.Add(name.Length);
                // Loop through entities
                for (int j = 0; j < entities.Count; j++)
                {
                    // Get the property value
                    var value = variables[name](entities[j]);
                    // Insert property into table, +1 because header takes up one row
                    table[i, j + 1] = value;
                    if (value == null) continue;
                    // Value string length
                    lengths.Add(value.Length);
                }

                // Column length
                columnLengths.Add(lengths.Max());
            }

            // Length of one row
            var rowLen = 0;
            for (int i = 0; i < table.GetLength(1); i++)
            {
                // List of all the elements from table array
                var rowElements = new List<string>();
                for (int j = 0; j < table.GetLength(0); j++)
                {
                    var element = $"{table[j, i]}";
                    // Format length to max length of any string in column
                    var format = "{0,-" + columnLengths[j] + "}";
                    var stringElement = string.Format(format, element);
                    rowElements.Add(stringElement);
                }

                // Final formatting of a row
                var stringRow = "| ";
                stringRow += string.Join(" | ", rowElements);
                stringRow += " |";
                if (rowLen == 0) rowLen = stringRow.Length;
                dataRows.Add(stringRow);
            }

            var finalRows = new string[entities.Count + 4];
            using var enumerator = dataRows.GetEnumerator();
            var fillerString = new string('-', rowLen);
            for (int i = 0; i < finalRows.Length; i++)
            {
                if (i == 0 || i == 2 || i == entities.Count + 3)
                {
                    finalRows[i] = fillerString;
                    continue;
                }

                enumerator.MoveNext();
                finalRows[i] = enumerator.Current;
            }

            return finalRows.ToList();
        }
    }
}