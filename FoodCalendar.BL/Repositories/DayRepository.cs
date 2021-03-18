using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using FoodCalendar.BL.Mappers;
using FoodCalendar.BL.Models;
using FoodCalendar.DAL.Entities;
using FoodCalendar.DAL.Factories;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.BL.Repositories
{
    public class DayRepository : RepositoryBase<DayModel, Day>
    {
        public DayRepository(IDbContextFactory contextFactory) :
            base(
                DayMapper.MapEntityToModel,
                DayMapper.MapModelToEntity,
                contextFactory
            )
        {
        }
    }
}