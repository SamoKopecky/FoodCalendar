using Microsoft.EntityFrameworkCore;

namespace FoodCalendar.DAL.Factories
{
    public class InMemoryDbContextFactory
    {
        public FoodCalendarDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodCalendarDbContext>();
            optionsBuilder.UseInMemoryDatabase("InMemoryFoodCalendar");
            return new FoodCalendarDbContext(optionsBuilder.Options);
        }
    }
}