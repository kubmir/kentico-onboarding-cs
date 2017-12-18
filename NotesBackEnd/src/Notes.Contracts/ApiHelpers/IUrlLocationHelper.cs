using System;

namespace Notes.Contracts.ApiHelpers
{
    public interface IUrlLocationHelper
    {
        String GetUrlWithId(string routeName, Guid id);
    }
}
