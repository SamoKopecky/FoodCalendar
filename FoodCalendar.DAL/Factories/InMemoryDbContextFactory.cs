using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.DAL.Factories
{
    public class InMemoryDbContextFactory : IDbContextFactory
    {
        public FoodCalendarDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodCalendarDbContext>();
            optionsBuilder.UseInMemoryDatabase("InMemoryFoodCalendar");
            optionsBuilder.EnableSensitiveDataLogging();
            return new FoodCalendarDbContext(optionsBuilder.Options);
        }
    }
}