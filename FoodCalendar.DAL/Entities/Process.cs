namespace FoodCalendar.DAL.Entities
{
    public class Process
    {
        public int TimeRequired { get; set; }
        public string Description { get; set; }

        public Process(int timeRequired, string description)
        {
            TimeRequired = timeRequired;
            Description = description;
        }
    }
}
