using System;

namespace Notes.Contracts.ApiServices
{
    public interface IRouteOptions
    {
        String GetNotesRoute();

        String GetNotesRouteName();
    }
}
