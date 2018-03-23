using System;
using Notes.Contracts.Services.Wrappers;

namespace Notes.Services.Wrappers
{
    internal class DateWrapper : IDateWrapper
    {
        public DateTime GetCurrentDateTime()
            // ReSharper disable once NoDateTimeNow
            => DateTime.Now;
    }
}
