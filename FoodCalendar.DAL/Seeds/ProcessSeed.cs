using System;
using System.Collections.Generic;
using System.Text;
using FoodCalendar.DAL.Entities;

namespace FoodCalendar.DAL.Seeds
{
    public static class ProcessSeed
    {
        public static readonly Process HamAndEggsProcess = new Process()
        {
            TimeRequired = 60,
            Description = "cook the eggs and ham"
        };
    }
}