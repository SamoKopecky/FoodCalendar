using System;

namespace FoodCalendar.BL.Models
{
    public abstract class ModelBase
    {
        public Guid Id { get; set; }

        protected ModelBase()
        {
            Id = new Guid();
        }
    }
}