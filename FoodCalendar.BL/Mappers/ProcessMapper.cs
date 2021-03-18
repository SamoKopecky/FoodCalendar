using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;

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

        public static Process MapModelToEntity(ProcessModel model)
        {
            if (model != null)
            {
                return new Process()
                {
                    Id = model.Id,
                    Description = model.Description,
                    TimeRequired = model.TimeRequired
                };
            }
            return null;
        }
    }
}