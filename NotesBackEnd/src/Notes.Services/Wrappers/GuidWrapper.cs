using System;
using Notes.Contracts.Services.Wrappers;

namespace Notes.Services.Wrappers
{
    internal class GuidWrapper : IGuidWrapper
    {
        public Guid GetNew()
            => Guid.NewGuid();
    }
}
