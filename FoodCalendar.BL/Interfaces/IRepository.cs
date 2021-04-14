using System;
using System.Collections.Generic;
using FoodCalendar.BL.Models;

namespace FoodCalendar.BL.Interfaces
{
    public interface IRepository<TModel> where TModel : ModelBase
    {
        /// <summary>
        /// Deletes the given model.
        /// </summary>
        /// <param name="model">Model to delete.</param>
        void Delete(TModel model);

        /// <summary>
        /// Finds and deletes the model of given Id.
        /// </summary>
        /// <param name="id">ID of the model to delete.</param>
        void Delete(Guid id);

        /// <summary>
        /// Inserts a new model into the DB.
        /// </summary>
        /// <param name="model">Model to insert.</param>
        /// <returns>Inserted model with filed IDs.</returns>
        TModel Insert(TModel model);

        /// <summary>
        /// Updates a model.
        /// </summary>
        /// <param name="model">Update model.</param>
        void Update(TModel model);

        /// <summary>
        /// Finds and returns the model of the given Id.
        /// </summary>
        /// <param name="id">Id of the model to be returned.</param>
        /// <returns>Found model.</returns>
        TModel GetById(Guid id);

        /// <summary>
        /// Returns a list of all available models.
        /// </summary>
        /// <returns>List of models.</returns>
        ICollection<TModel> GetAll();
    }
}