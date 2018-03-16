using System.Threading.Tasks;
using Notes.Contracts.Model;

namespace Notes.Contracts.Services.Notes
{
    public interface ICreationService
    {
        Task<Note> CreateAsync(Note note);
    }
}