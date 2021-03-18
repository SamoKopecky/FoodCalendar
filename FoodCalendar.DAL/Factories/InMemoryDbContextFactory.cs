using FoodCalendar.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodCalendar.DAL.Factories
{
    public class InMemoryDbContextFactory : IDbContextFactory
    {
        public FoodCalendarDbContext CreateDbContext()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            var optionsBuilder = new DbContextOptionsBuilder<FoodCalendarDbContext>()
                .UseInMemoryDatabase("InMemoryFoodCalendar")
                .EnableSensitiveDataLogging()
                .UseInternalServiceProvider(serviceProvider);
            return new FoodCalendarDbContext(optionsBuilder.Options);
        }
    }
}