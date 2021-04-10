using System;
using System.Collections.Generic;
using FoodCalendar.BL.Models;

namespace FoodCalendar.BL.Interfaces
{
    public interface IRepository<TModel> where TModel : ModelBase 
    {
        void Delete(TModel model);
        void Delete(Guid id);
        void Insert(TModel model);
        void Update(TModel model);
        TModel GetById(Guid id);
        ICollection<TModel> GetAll();
    }
}