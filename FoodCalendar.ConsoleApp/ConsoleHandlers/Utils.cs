using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodCalendar.BL.Models;
using FoodCalendar.BL.Repositories;
using FoodCalendar.DAL.Factories;

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

        

        
    }
}