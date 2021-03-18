using System;
using System.Collections.Generic;

namespace FoodCalendar.DAL.Entities
{
    public class Process : EntityBase
    {
        public int TimeRequired { get; set; }
        public string Description { get; set; }
        public Meal Meal { get; set; }

        public Process()
        {
        }

        public Process(int timeRequired, string description)
        {
            TimeRequired = timeRequired;
            Description = description;
        }

        protected bool Equals(Process other)
        {
            return TimeRequired == other.TimeRequired && Description == other.Description && Equals(Meal, other.Meal);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Process) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TimeRequired, Description, Meal);
        }

        public static bool operator ==(Process left, Process right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Process left, Process right)
        {
            return !Equals(left, right);
        }
    }
}