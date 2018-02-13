using System;
using Notes.Contracts.Services.Utils;

namespace Notes.Services.Utils
{
    class GuidService : IGuidService
    {
        public Guid GetNewGuid()
        {
            return Guid.NewGuid();
        }
    }
}
