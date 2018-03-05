using System.Web.Configuration;
using Notes.Contracts.Dependency;
using Notes.Contracts.Repository;
using Notes.Repository.Repository;

namespace Notes.Repository.Dependency
{
    public class RepositoryTypesBootstrapper : IBootstrapper
    {
        public IDependencyContainerRegister RegisterType(IDependencyContainerRegister container)
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["MongoDb_Notes_Connection"].ConnectionString;

            return container
                .RegisterType<INotesRepository, NotesRepository>(LifetimeTypes.PerApplicationSingleton, connectionString);
        }
    }
}