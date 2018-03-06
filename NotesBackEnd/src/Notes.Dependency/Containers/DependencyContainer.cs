using System;
using System.Collections.Generic;
using Notes.Contracts.Dependency;
using Notes.Dependency.LifeTimeManagers;
using Unity;
using Unity.Injection;

namespace Notes.Dependency.Containers
{
    public class DependencyContainer : IDependencyContainer
    {
        private readonly IUnityContainer _unityContainer;

        public DependencyContainer()
        {
            _unityContainer = new UnityContainer();
        }

        private DependencyContainer(IUnityContainer container)
        {
            _unityContainer = container;
        }

        public IDependencyContainerRegister RegisterType<TType>(Func<TType> getObjectFunc, LifetimeTypes lifetimeType)
        {
            var lifetimeManager = lifetimeType.GetUnityLifetimeManager();

            _unityContainer.RegisterType<TType>(lifetimeManager, new InjectionFactory(_ => getObjectFunc()));
            
            return this;
        }

        public IDependencyContainerRegister RegisterType<TFrom, TTo>(LifetimeTypes lifetimeType) where TTo : TFrom
        {
            var lifetimeManager = lifetimeType.GetUnityLifetimeManager();

            _unityContainer.RegisterType<TFrom, TTo>(lifetimeManager);

            return this;
        }

        public object Resolve(Type serviceType)
            => _unityContainer.Resolve(serviceType);


        public IEnumerable<object> ResolveAll(Type serviceType)
            => _unityContainer.ResolveAll(serviceType);

        public IDependencyContainerResolver CreateChildContainer()
            => new DependencyContainer(_unityContainer.CreateChildContainer());
        
        public void Dispose()
            => _unityContainer.Dispose();
    }
}
