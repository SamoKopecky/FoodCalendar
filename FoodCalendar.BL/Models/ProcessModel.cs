using System;
using System.Collections.Generic;

namespace FoodCalendar.BL.Models
{
    public class ProcessModel : ModelBase
    {
        public int TimeRequired { get; set; }
        public string Description { get; set; }

        public ProcessModel() : base()
        {
        }

        private sealed class ProcessModelEqualityComparer : IEqualityComparer<ProcessModel>
        {
            public bool Equals(ProcessModel x, ProcessModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.TimeRequired == y.TimeRequired && x.Description == y.Description;
            }

            public int GetHashCode(ProcessModel obj)
            {
                return HashCode.Combine(obj.TimeRequired, obj.Description);
            }
        }

        public static IEqualityComparer<ProcessModel> ProcessModelComparer { get; } =
            new ProcessModelEqualityComparer();

        public override bool Equals(object obj)
        {
            var process = (ProcessModel) obj;
            return ProcessModelComparer.Equals(this, process) && process != null && process.Id == Id;
        }

        public override int GetHashCode()
        {
            return ProcessModelComparer.GetHashCode(this);
        }
    }
}