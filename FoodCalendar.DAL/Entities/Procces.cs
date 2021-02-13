using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Entities
{
    public class Procces
    {
        public int TimeRequired { get; set; }
        public string Description { get; set; }

        public Procces(int timeRequired, string description)
        {
            TimeRequired = timeRequired;
            Description = description;
        }
    }
}
