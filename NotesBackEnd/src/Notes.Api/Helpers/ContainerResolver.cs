using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Notes.Contracts.Dependency;

namespace Notes.Api.Helpers
{
    public class ContainerResolver : IDependencyResolver
    {
        protected IMyContainer Container;

        public ContainerResolver(IMyContainer container)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public object GetService(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.ResolveAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            var child = Container.CreateChildContainer();
            return new ContainerResolver(child);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            Container.Dispose();
        }
    }
}