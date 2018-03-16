using Notes.Contracts.Dependency;
using Notes.Contracts.Repository;
using Notes.Repository.Repository;

namespace Notes.Repository.Dependency
{
    public class RepositoryTypesBootstrapper : IBootstrapper
    {
        public IContainer RegisterType(IContainer container)
            => container
                .RegisterType<INotesRepository, NotesRepository>(LifetimeTypes.PerApplicationSingleton);
    }
}