﻿using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;

namespace FoodCalendar.BL.Mappers
{
    public static class ProcessMapper
    {
        public static ProcessModel MapEntityToModel(Process entity)
        {
            if (entity != null)
            {
                return new ProcessModel()
                {
                    Id = entity.Id,
                    Description = entity.Description,
                    TimeRequired = entity.TimeRequired
                };
            }

            return null;
        }

        public static Process MapModelToEntity(ProcessModel model, EntityFactory entityFactory)
        {
            var entity = entityFactory.Create<Process>(model.Id);
            entity.Id = model.Id;
            entity.Description = model.Description;
            entity.TimeRequired = model.TimeRequired;
            return entity;
        }
    }
}