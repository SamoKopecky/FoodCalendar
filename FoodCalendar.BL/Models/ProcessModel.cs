using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.BL.Models
{
    public class ProcessModel : ModelBase
    {
        public int TimeRequired { get; set; }
        public string Description { get; set; }
    }
}