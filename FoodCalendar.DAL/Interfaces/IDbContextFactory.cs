namespace FoodCalendar.DAL.Interfaces
{
    public interface IDbContextFactory
    {
        FoodCalendarDbContext CreateDbContext();
    }
}