using System.Threading.Tasks;
using Notes.Contracts.Model;

namespace Notes.Contracts.Services.Notes
{
    public interface IAddNoteService
    {
        Task<Note> CreateNoteAsync(Note note);
    }
}
