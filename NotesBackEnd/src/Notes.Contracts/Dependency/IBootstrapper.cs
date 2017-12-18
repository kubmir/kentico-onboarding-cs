namespace Notes.Contracts.Dependency
{
    public interface IBootstrapper
    {
        IMyContainer RegisterType(IMyContainer container);
    }
}
