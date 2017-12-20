using Notes.Contracts.Dependency;
using Notes.Contracts.Services.Date;
using Notes.Contracts.Services.Notes;
using Notes.Services.Date;
using Notes.Services.Notes;

namespace Notes.Services.Dependency
{
    public class ServicesTypesRegistration : IBootstrapper
    {
        public IDependencyContainer RegisterType(IDependencyContainer container)
            => container
                .RegisterType<INotesServices, NotesServices>()
                .RegisterType<IDateService, DateService>();
    }
}
