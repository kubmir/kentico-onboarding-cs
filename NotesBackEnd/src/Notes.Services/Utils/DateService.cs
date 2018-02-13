using System;
using Notes.Contracts.Services.Date;

namespace Notes.Services.Utils
{
    class DateService : IDateService
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
