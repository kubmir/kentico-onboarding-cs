namespace Notes.Contracts.Routes
{
    public static class RouteDictionary
    {
        public const string NotesRouteName = "Notes";

        public const string NotesRoute = "api/v{version:apiVersion}/notes/{id?}";
    }
}
