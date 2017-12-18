namespace Notes.Contracts.Dependency
{
    public interface IBootstrapper
    {
        IDependencyContainer RegisterType(IDependencyContainer container);
    }
}
