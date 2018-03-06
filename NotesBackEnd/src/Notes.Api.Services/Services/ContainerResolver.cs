using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;
using System.Web.Http.Metadata;
using Notes.Contracts.Dependency;

namespace Notes.Api.Services.Services
{
    internal class ContainerResolver : IDependencyResolver
    {
        private const string ModelValidatorCacheException = "System.Web.Http.Validation.IModelValidatorCache";

        private static readonly List<string> ExcludedTypes = new List<string>()
        {
            typeof(IHostBufferPolicySelector).FullName,
            typeof(IHttpControllerSelector).FullName,
            typeof(IHttpControllerActivator).FullName,
            typeof(IHttpActionSelector).FullName,
            typeof(IHttpActionInvoker).FullName,
            typeof(IExceptionHandler).FullName,
            typeof(IContentNegotiator).FullName,
            typeof(ModelMetadataProvider).FullName,
            ModelValidatorCacheException
        };

        protected IDependencyContainerResolver Container;

        public ContainerResolver(IDependencyContainerResolver container)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Resolve(serviceType);
            }
            catch (Exception)
                when (ExcludedTypes.Contains(serviceType?.FullName))
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType);
            }
            catch (Exception)
                when (ExcludedTypes.Contains(serviceType?.FullName))
            {
                return null;
            }
        }
        
        public IDependencyScope BeginScope()
            => new ContainerResolver(Container.CreateChildContainer());

        public void Dispose()
            => Dispose(true);

        protected virtual void Dispose(bool disposing)
            => Container.Dispose();
    }
}