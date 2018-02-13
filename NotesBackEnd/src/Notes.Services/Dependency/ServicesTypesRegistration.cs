using Notes.Contracts.Dependency;
using Notes.Contracts.Services.Date;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Utils;
using Notes.Services.Notes;
using Notes.Services.Utils;

namespace Notes.Services.Dependency
{
    public class ServicesTypesRegistration : IBootstrapper
    {
        public IDependencyContainerRegister RegisterType(IDependencyContainerRegister container)
            => container
                .RegisterType<INotesServices, NotesServices>(LifetimeTypes.PerApplicationSingleton)
                .RegisterType<IDateService, DateService>(LifetimeTypes.PerApplicationSingleton)
                .RegisterType<IGuidService, GuidService>(LifetimeTypes.PerApplicationSingleton);
    }
}
