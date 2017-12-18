using Notes.Contracts.Dependency;
using Notes.Contracts.Repository;
using Notes.Repository.Repository;

namespace Notes.Repository.Dependency
{
    public class RepositoryTypesRegistration : IBootstrapper
    {
        public IMyContainer RegisterType(IMyContainer container)
            => container.RegisterType<INotesRepository, NotesRepository>();
    }
}
