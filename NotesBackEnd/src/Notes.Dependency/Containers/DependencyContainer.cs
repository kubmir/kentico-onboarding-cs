﻿using System;
using System.Collections.Generic;
using Notes.Contracts.Dependency;
using Notes.Dependency.LifeTimeManagers;
using Unity;
using Unity.Injection;

namespace Notes.Dependency.Containers
{
    internal class DependencyContainer : IDependencyContainer
    {
        private readonly IUnityContainer _unityContainer;

        public DependencyContainer() 
            => _unityContainer = new UnityContainer();

        private DependencyContainer(IUnityContainer container) 
            => _unityContainer = container;

        public IContainer RegisterType<TFrom, TTo>(LifetimeTypes lifetimeType) 
            where TTo : TFrom
        {
            var lifetimeManager = lifetimeType.GetUnityLifetimeManager();

            _unityContainer.RegisterType<TFrom, TTo>(lifetimeManager);

            return this;
        }

        public IContainer RegisterType<TTo>(Func<TTo> getObjectFunc, LifetimeTypes lifetimeType)
        {
            var lifetimeManager = lifetimeType.GetUnityLifetimeManager();

            _unityContainer.RegisterType<TTo>(lifetimeManager, new InjectionFactory(_ => getObjectFunc()));

            return this;
        }

        public Object Resolve(Type serviceType)
            => _unityContainer.Resolve(serviceType);

        public TType Resolve<TType>()
            => _unityContainer.Resolve<TType>();

        public IEnumerable<Object> ResolveAll(Type serviceType)
            => _unityContainer.ResolveAll(serviceType);

        public IResolver CreateChildContainer()
            => new DependencyContainer(_unityContainer.CreateChildContainer());
        
        public void Dispose()
            => _unityContainer.Dispose();
    }
}
