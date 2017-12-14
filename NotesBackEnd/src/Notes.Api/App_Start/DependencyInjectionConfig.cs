using System.Web.Http;
using Notes.Contracts.Repository;
using Notes.Repository;
using Unity;

namespace Notes.Api
{
    public class DependencyInjectionConfig
    {
        public static void Register(HttpConfiguration config)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<INotesRepository, NotesRepository>();

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}