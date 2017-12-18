using System;

namespace Notes.Contracts.ApiHelpers
{
    public interface IUrlLocationHelper
    {
        String GetUrl(Guid id);
    }
}
