using System;

namespace FoodCalendar.DAL.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        protected EntityBase() : base()
        {
            Id = new Guid();
        }
    }
}