using System;
using Notes.Contracts.Dependency;
using Unity.Lifetime;

namespace Notes.Dependency.LifeTimeManagers
{
    public static class LifetimeTypesExtensions
    {
        public static LifetimeManager GetUnityLifetimeManager(this LifetimeTypes type)
        {
            switch (type)
            {
                case LifetimeTypes.PerRequestSingleton:
                    return new HierarchicalLifetimeManager();
                case LifetimeTypes.PerInstanceSingleton: 
                    return new TransientLifetimeManager();
                case LifetimeTypes.PerApplicationSingleton:
                    return new ContainerControlledLifetimeManager();
                default:
                    throw new ArgumentException("Unknown lifetime type");
            }
        }
    }
}
