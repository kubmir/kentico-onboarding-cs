using Notes.Contracts.Dependency;
using Notes.Contracts.Repository;
using Notes.Repository.Repository;

namespace Notes.Repository.Dependency
{
    public class RepositoryTypesRegistration : IBootstrapper
    {
        public IDependencyContainer RegisterType(IDependencyContainer container)
            => container.RegisterType<INotesRepository, NotesRepository>();
    }
}
