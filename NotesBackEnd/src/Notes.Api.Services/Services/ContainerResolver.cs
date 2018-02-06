using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Notes.Contracts.Dependency;

namespace Notes.Api.Services.Services
{
    public class ContainerResolver : IDependencyResolver
    {
        protected IDependencyContainerResolver Container;

        public ContainerResolver(IDependencyContainerResolver container)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public object GetService(Type serviceType)
            => Container.Resolve(serviceType);

        public IEnumerable<object> GetServices(Type serviceType)
            => Container.ResolveAll(serviceType);
        
        public IDependencyScope BeginScope()
            => new ContainerResolver(Container.CreateChildContainer());

        public void Dispose()
            => Dispose(true);

        protected virtual void Dispose(bool disposing)
            => Container.Dispose();
    }
}