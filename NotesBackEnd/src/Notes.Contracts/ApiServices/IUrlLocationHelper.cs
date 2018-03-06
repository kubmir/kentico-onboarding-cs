using System;

namespace Notes.Contracts.ApiServices
{
    public interface IUrlLocationHelper
    {
        String GetNotesUrlWithId(Guid id);
    }
}
