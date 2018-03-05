using Notes.Contracts.Dependency;
using Notes.Contracts.Services.Date;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Utils;
using Notes.Services.Notes;
using Notes.Services.Utils;

namespace Notes.Services.Dependency
{
    public class ServicesTypesBootstrapper : IBootstrapper
    {
        public IDependencyContainerRegister RegisterType(IDependencyContainerRegister container)
            => container
                .RegisterType<IAddNoteService, AddNoteService>(LifetimeTypes.PerRequestSingleton)
                .RegisterType<IUpdateNoteService, UpdateNoteService>(LifetimeTypes.PerRequestSingleton)
                .RegisterType<IGetNoteService, GetNoteService>(LifetimeTypes.PerRequestSingleton)
                .RegisterType<IDateService, DateService>(LifetimeTypes.PerApplicationSingleton)
                .RegisterType<IGuidService, GuidService>(LifetimeTypes.PerApplicationSingleton);
    }
}
