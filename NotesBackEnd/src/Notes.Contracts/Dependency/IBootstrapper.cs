namespace Notes.Contracts.Dependency
{
    public interface IBootstrapper
    {
        IContainer RegisterType(IContainer container);
    }
}
