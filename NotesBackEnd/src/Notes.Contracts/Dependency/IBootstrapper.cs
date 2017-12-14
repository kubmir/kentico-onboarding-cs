using Unity;

namespace Notes.Contracts.Dependency
{
    public interface IBootstrapper
    {
        IUnityContainer RegisterType(IUnityContainer container);
    }
}
