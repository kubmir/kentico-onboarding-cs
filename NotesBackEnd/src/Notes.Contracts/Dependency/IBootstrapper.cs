namespace Notes.Contracts.Dependency
{
    public interface IBootstrapper
    {
        IDependencyContainerRegister RegisterType(IDependencyContainerRegister container);
    }
}
