using System;

namespace Notes.Contracts.ApiServices
{
    public interface IUrlLocationHelper
    {
        String GetUrlWithId(string routeName, Guid id);
    }
}
