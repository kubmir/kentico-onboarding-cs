using System;
using Notes.Contracts.Services.Utils;

namespace Notes.Services.Utils
{
    class GuidService : IGuidService
    {
        public Guid GetNew()
        {
            return Guid.NewGuid();
        }
    }
}
