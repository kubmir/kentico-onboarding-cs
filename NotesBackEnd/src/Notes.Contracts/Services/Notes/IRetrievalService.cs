using System;
using System.Threading.Tasks;
using Notes.Contracts.Model;

namespace Notes.Contracts.Services.Notes
{
    public interface IRetrievalService
    {
        Task<Note> GetByIdAsync(Guid id);

        Task<Boolean> Exists(Guid id);
    }
}