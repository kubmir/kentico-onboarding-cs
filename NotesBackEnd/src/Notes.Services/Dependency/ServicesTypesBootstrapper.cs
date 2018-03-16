using Notes.Contracts.Dependency;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Wrappers;
using Notes.Services.Notes;
using Notes.Services.Wrappers;

namespace Notes.Services.Dependency
{
    public class ServicesTypesBootstrapper : IBootstrapper
    {
        public IContainer RegisterType(IContainer container)
            => container
                .RegisterType<ICreationService, CreationService>(LifetimeTypes.PerRequestSingleton)
                .RegisterType<IUpdateService, UpdateService>(LifetimeTypes.PerRequestSingleton)
                .RegisterType<IRetrievalService, RetrievalService>(LifetimeTypes.PerRequestSingleton)
                .RegisterType<IDateWrapper, DateWrapper>(LifetimeTypes.PerApplicationSingleton)
                .RegisterType<IGuidWrapper, GuidWrapper>(LifetimeTypes.PerApplicationSingleton);
    }
}
