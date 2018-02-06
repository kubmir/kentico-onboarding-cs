using Notes.Contracts.Dependency;
using Notes.Contracts.Services.Date;
using Notes.Contracts.Services.Notes;
using Notes.Services.Date;
using Notes.Services.Notes;

namespace Notes.Services.Dependency
{
    public class ServicesTypesRegistration : IBootstrapper
    {
        public IDependencyContainerRegister RegisterType(IDependencyContainerRegister container)
            => container
                .RegisterType<INotesServices, NotesServices>(LifetimeTypes.PerApplicationSingleton)
                .RegisterType<IDateService, DateService>(LifetimeTypes.PerApplicationSingleton);
    }
}
