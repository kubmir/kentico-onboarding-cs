namespace Notes.Contracts.Dependency
{
    public enum LifetimeTypes
    {
        GlobalSingleton, 
        ThreadSingleton,
        HierarchicalSingleton,
        Transient,
    }
}
