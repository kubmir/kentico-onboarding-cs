using System;
using Notes.Contracts.Services.Utils;

namespace Notes.Services.StaticWrappers
{
    internal class DateService : IDateService
    {
        public DateTime GetCurrentDateTime()
            => DateTime.Now;
    }
}
