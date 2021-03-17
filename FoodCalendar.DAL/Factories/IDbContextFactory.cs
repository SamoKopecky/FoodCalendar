using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalendar.DAL.Factories
{
    interface IDbContextFactory
    {
        FoodCalendarDbContext CreateDbContext();
    }
}