using System;
using Notes.Contracts.ApiServices;

namespace Notes.Api.Routes
{
    internal class RouteOptions : IRouteOptions
    {
        internal const String NotesRouteName = "Notes";
        internal const String NotesRoute = "api/v{version:apiVersion}/notes/{id?}";

        public String GetNotesRoute()
            => NotesRoute;

        public String GetNotesRouteName()
            => NotesRouteName;
    }
}
