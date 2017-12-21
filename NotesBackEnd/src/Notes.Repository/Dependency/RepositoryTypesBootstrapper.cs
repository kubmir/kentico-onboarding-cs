using Notes.Contracts.Dependency;
using Notes.Contracts.Repository;
using Notes.Repository.Repository;

namespace Notes.Repository.Dependency
{
    public class RepositoryTypesBootstrapper : IBootstrapper
    {
        public IDependencyContainer RegisterType(IDependencyContainer container)
            => container.RegisterType<INotesRepository, NotesRepository>();
    }
}
