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
            Id = Guid.Parse("7598e291-8719-4634-a360-d1e3127f8d30"),
            TimeRequired = 60,
            Description = "cook the eggs and ham"
        };
    }
}