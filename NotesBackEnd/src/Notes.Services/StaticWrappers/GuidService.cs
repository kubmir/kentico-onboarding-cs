using System;
using Notes.Contracts.Services.Utils;

namespace Notes.Services.StaticWrappers
{
    internal class GuidService : IGuidService
    {
        public Guid GetNew()
            => Guid.NewGuid();
    }
}
