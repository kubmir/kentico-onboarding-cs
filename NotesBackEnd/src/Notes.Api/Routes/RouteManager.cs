﻿using Notes.Contracts.ApiServices;

namespace Notes.Api.Routes
{
    internal class RouteManager : IRouteManager
    {
        internal const string NotesRouteName = "Notes";
        internal const string NotesRoute = "api/v{version:apiVersion}/notes/{id?}";

        public string GetNotesRoute()
            => NotesRoute;

        public string GetNotesRouteName()
            => NotesRouteName;
    }
}
