using System;
using System.Collections.Generic;
using System.Net.Http;
using Notes.Contracts.Dependency;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

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

        public IDependencyContainer RegisterType<TType>(object injectedObject)
        {
            _unityContainer.RegisterType<TType>(new InjectionConstructor(injectedObject));

            return this;
        }

        public IDependencyContainer RegisterHttpRequestMessage(Func<HttpRequestMessage> getHttpRequestMessageFunc)
        {
            _unityContainer.RegisterType<HttpRequestMessage>(new HierarchicalLifetimeManager(), new InjectionFactory(_ => getHttpRequestMessageFunc()));

            return this;
        }

        public IDependencyContainer RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            _unityContainer.RegisterType<TFrom, TTo>();

            return this;
        }

        public object Resolve(Type serviceType)
        {
            try
            {
                return _unityContainer.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            return _unityContainer.ResolveAll(serviceType);
        }

        public IDependencyContainer CreateChildContainer()
        {
            return new DependencyContainer(_unityContainer.CreateChildContainer());
        }

        public void Dispose()
        {
            _unityContainer.Dispose();
        }
    }
}
