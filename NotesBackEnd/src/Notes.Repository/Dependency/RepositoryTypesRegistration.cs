using Unity;
using Notes.Contracts.Dependency;
using Notes.Contracts.Repository;

namespace Notes.Repository.Dependency
{
    public class RepositoryTypesRegistration : IBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container)
            => container.RegisterType<INotesRepository, NotesRepository>();
    }
}
