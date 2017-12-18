using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using Notes.Contracts.Dependency;
using Unity;
using Unity.Injection;

namespace Notes.Dependency
{
    public class MyContainer : IMyContainer
    {
        private IUnityContainer _unityContainer;

        public MyContainer()
        {
            _unityContainer = new UnityContainer();
        }

        private MyContainer(IUnityContainer container)
        {
            _unityContainer = container;
        }

        public IMyContainer RegisterType<TFrom>()
        {
            _unityContainer.RegisterType<TFrom>(new InjectionFactory(GetHttpRequestMessage));

            return this;
        }

        public IMyContainer RegisterType<TFrom, TTo>() where TTo : TFrom
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

        public IMyContainer CreateChildContainer()
        {
            return new MyContainer(_unityContainer.CreateChildContainer());
        }

        public void Dispose()
        {
            _unityContainer.Dispose();
        }

        private HttpRequestMessage GetHttpRequestMessage(IUnityContainer container)
            => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
    }
}
