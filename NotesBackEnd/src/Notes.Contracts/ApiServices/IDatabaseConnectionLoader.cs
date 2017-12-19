using System;

namespace Notes.Contracts.ApiServices
{
    public interface IDatabaseConnectionLoader
    {
        String GetNotesDatabaseConnectionString();
    }
}
