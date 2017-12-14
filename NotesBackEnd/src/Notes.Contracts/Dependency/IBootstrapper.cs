using Unity;

namespace Notes.Contracts.Dependency
{
    interface IBootstrapper
    {
        IUnityContainer RegisterType(IUnityContainer container);
    }
}
