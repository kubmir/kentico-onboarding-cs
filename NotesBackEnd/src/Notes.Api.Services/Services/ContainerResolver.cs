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
        private const String ModelValidatorCacheException = "System.Web.Http.Validation.IModelValidatorCache";

        private static readonly List<String> ExcludedTypes = new List<String>
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

        protected IResolver Resolver;

        public ContainerResolver(IResolver resolver) 
            => Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));

        public Object GetService(Type serviceType)
        {
            try
            {
                return Resolver.Resolve(serviceType);
            }
            catch (Exception)
                when (ExcludedTypes.Contains(serviceType?.FullName))
            {
                return null;
            }
        }

        public IEnumerable<Object> GetServices(Type serviceType)
        {
            try
            {
                return Resolver.ResolveAll(serviceType);
            }
            catch (Exception)
                when (ExcludedTypes.Contains(serviceType?.FullName))
            {
                return null;
            }
        }

        public IDependencyScope BeginScope()
            => new ContainerResolver(Resolver.CreateChildContainer());

        public void Dispose()
            => Dispose(disposing: true);

        protected virtual void Dispose(Boolean disposing)
            => Resolver.Dispose();
    }
}