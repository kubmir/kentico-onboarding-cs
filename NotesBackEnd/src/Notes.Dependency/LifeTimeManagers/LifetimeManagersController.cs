using Notes.Contracts.Dependency;
using Unity.Lifetime;

namespace Notes.Dependency.LifeTimeManagers
{
    public static class LifetimeManagersController
    {
        public static LifetimeManager GetLifetimeManager(LifetimeTypes type)
        {
            switch (type)
            {
                case LifetimeTypes.HierarchicalSingleton:
                    return new HierarchicalLifetimeManager();
                case LifetimeTypes.ThreadSingleton:
                    return new PerThreadLifetimeManager();
                case LifetimeTypes.Transient: 
                    return new TransientLifetimeManager();
                default:
                    return new ContainerControlledLifetimeManager();
            }
        }
    }
}
