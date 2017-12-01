using System.Collections.Generic;

namespace Notes.Api.Model
{
    public static class NotesList
    {
        public static List<Note> GetInitialNotes()
        {
            return new List<Note>
            {
                new Note("First note", "2c00d1c2-fd2b-4c06-8f2d-130e88f719c2", false),
                new Note("Second note", "ebcb3d81-af4e-428f-a22d-e7852d70d3a0", false),
                new Note("Third note", "599442c0-ae28-4157-9a3f-0491bb4ba6c1", false),
                new Note("Fourth note", "4785546e-824d-42a4-900b-e656f19ffb59", false)
            };

        }
    }
}