using System;
using Notes.Contracts.Services.Date;

namespace Notes.Services.Date
{
    class DateService : IDateService
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
