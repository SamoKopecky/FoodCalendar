using System;
using System.Collections.Generic;
using System.Text;
using FoodCalendar.DAL.Interfaces;

namespace FoodCalendar.ConsoleApp.ConsoleHandlers
{
    public abstract class ConsoleHandlerBase
    {
        protected readonly IDbContextFactory DbContextFactory;
        protected readonly int IdLength;

        protected ConsoleHandlerBase(IDbContextFactory dbContextFactory, int idLength)
        {
            DbContextFactory = dbContextFactory;
            IdLength = idLength;
        }
    }
}