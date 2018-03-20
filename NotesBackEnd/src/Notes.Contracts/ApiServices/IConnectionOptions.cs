using System;

namespace Notes.Contracts.ApiServices
{
    public interface IConnectionOptions
    {
        String GetNotesDatabaseConnectionString();
    }
}
