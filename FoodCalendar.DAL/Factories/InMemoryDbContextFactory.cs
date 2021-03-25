using System.Diagnostics;
using FoodCalendar.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.DAL.Factories
{
    public class InMemoryDbContextFactory : IDbContextFactory
    {
        private readonly string _dbName;

        public InMemoryDbContextFactory(string dbName)
        {
            _dbName = dbName;
        }

        public InMemoryDbContextFactory(StackTrace stackTrace)
        {
            _dbName = stackTrace.GetFrame(0)?.GetMethod()?.Name;
        }

        public FoodCalendarDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodCalendarDbContext>()
                .UseInMemoryDatabase(_dbName)
                .EnableSensitiveDataLogging();
            return new FoodCalendarDbContext(optionsBuilder.Options);
        }
    }
}