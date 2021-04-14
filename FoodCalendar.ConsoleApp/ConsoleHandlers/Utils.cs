using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public class Utils
    {
        public static T ScanProperty<T>(string propertyName)
        {
            Console.Write($"Enter value for {propertyName}: ");
            var parameter = Console.ReadLine();
            return (T) Convert.ChangeType(parameter, typeof(T));
        }

        public static T GetExistingEntity<T>(
            List<T> entities,
            List<string> table,
            string entityName,
            int idLength
        )
            where T : ModelBase
        {
            Console.Clear();
            foreach (var row in table)
            {
                Console.WriteLine(row);
            }

            table = table.Where(s => s[0] == '|').ToList();
            var shortIds = table.Select(row => row.Substring(2, idLength)).ToList();
            shortIds.RemoveAt(0);
            var id = GetBestMatch(shortIds, entityName);
            return entities.First(m => $"{m.Id}".Substring(0, id.Length) == id);
        }


        public static List<string> GetIngredientTable(IDbContextFactory dbContextFactory, int idLength,
            Func<IngredientModel, bool> filter = null)
        {
            var headersToValues = new Dictionary<string, Func<IngredientModel, string>>()
            {
                {"Short ID", i => $"{i.Id}".Substring(0, idLength)},
                {"Name", i => i.Name},
                {"Amount Stored", i => $"{i.AmountStored}"},
                {"Unit Name", i => i.UnitName},
                {"Calories", i => $"{i.Calories}"}
            };
            var repo = new IngredientRepository(dbContextFactory);
            var collection = FilterEntities(repo.GetAll(), filter);
            var table = GenericTable(collection, headersToValues);
            return table;
        }

        public static List<string> GetMealTable(IDbContextFactory dbContextFactory, int idLength,
            Func<MealModel, bool> filter = null)
        {
            var headersToValues = new Dictionary<string, Func<MealModel, string>>()
            {
                {"Short ID", m => $"{m.Id}".Substring(0, idLength)},
                {"Meal Name", m => $"{m.MealName}"},
                {"Total Time", m => $"{m.TotalTime}"},
                {"Calories", m => $"{m.Calories}"},
                {"Process description", m => m.Process.Description},
                {
                    "Ingredient amounts", m => ListToString(
                        m.IngredientsUsed,
                        amountModel => $"{amountModel.Ingredient.Name}: {amountModel.Amount}"
                    )
                }
            };
            var repo = new MealRepository(dbContextFactory);
            var collection = FilterEntities(repo.GetAll(), filter);
            var table = GenericTable(collection, headersToValues);
            return table;
        }

        public static List<string> GetDishTable(IDbContextFactory dbContextFactory, int idLength,
            Func<DishModel, bool> filter = null)
        {
            var headersToValues = new Dictionary<string, Func<DishModel, string>>()
            {
                {"Short ID", d => $"{d.Id}".Substring(0, idLength)},
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
            var collection = FilterEntities(repo.GetAll(), filter);
            var table = GenericTable(collection, headersToValues);
            return table;
        }

        private static List<T> FilterEntities<T>(
            IEnumerable<T> entities,
            Func<T, bool> filter
        )
            where T : ModelBase
        {
            List<T> collection;
            if (filter == null)
            {
                collection = entities.ToList();
            }
            else
            {
                collection = entities.Where(filter).ToList();
            }

            return collection;
        }

        private static string GetBestMatch(IReadOnlyCollection<string> shortIds, string name)
        {
            var matches = new List<string>();

            do
            {
                Console.Write($"Enter {name} ID: ");
                var userId = Console.ReadLine();
                if (userId == null) continue;
                var smallIds = shortIds.ToList().Select(s => s.Substring(0, userId.Length)).ToList();
                if (!smallIds.Contains(userId))
                {
                    Console.Write("No match found enter again: ");
                    continue;
                }

                matches = smallIds.Where(s => s == userId).ToList();
            } while (matches.Count > 1 || matches.Count == 0);

            return matches[0];
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

        public static void DisplayError(Exception e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
            Console.ReadKey();
        }
    }
}