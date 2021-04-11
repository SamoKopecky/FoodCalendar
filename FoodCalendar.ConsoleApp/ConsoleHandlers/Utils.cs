using System;
using System.Collections.Generic;
using System.Linq;
using FoodCalendar.BL.Models;

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

            Console.Write($"Enter {entityName} ID: ");
            table = table.Where(s => s[0] == '|').ToList();
            var shortIds = table.Select(row => row.Substring(2, idLength)).ToList();
            shortIds.RemoveAt(0);
            var id = GetBestMatch(shortIds);
            return entities.First(m => $"{m.Id}".Substring(0, id.Length) == id);
        }

        private static string GetBestMatch(IReadOnlyCollection<string> shortIds)
        {
            List<string> matches;
            do
            {
                var userId = Console.ReadLine();
                matches = shortIds.Where(s => userId != null && s.Substring(0, userId.Length) == userId)
                    .ToList();
            } while (matches.Count > 1);

            return matches[0];
        }
    }
}