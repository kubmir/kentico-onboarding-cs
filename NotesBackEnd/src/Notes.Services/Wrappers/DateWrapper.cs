﻿using System;
using Notes.Contracts.Services.Wrappers;

namespace Notes.Services.Wrappers
{
    internal class DateWrapper : IDateWrapper
    {
        public DateTime GetCurrentDateTime()
            => DateTime.Now;
    }
}
