using System;
using Notes.Contracts.Services.Utils;

namespace Notes.Services.Utils
{
    internal class DateService : IDateService
    {
        public DateTime GetCurrentDateTime()
            => DateTime.Now;
    }
}
