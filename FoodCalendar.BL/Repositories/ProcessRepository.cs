using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.BL.Repositories
{
    public class ProcessRepository : RepositoryBase<ProcessModel, Process>
    {
        public ProcessRepository(IDbContextFactory contextFactory) :
            base(
                ProcessMapper.MapEntityToModel,
                ProcessMapper.MapModelToEntity,
                null,
                contextFactory
            )
        {
        }
    }
}