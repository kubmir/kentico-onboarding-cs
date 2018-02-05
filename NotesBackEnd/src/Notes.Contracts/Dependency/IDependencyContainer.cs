using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Notes.Contracts.Dependency
{
    public interface IDependencyContainer : IDisposable
    {
        IDependencyContainer RegisterType<TType>(object injectedObject);

        IDependencyContainer RegisterType<TFrom, TTo>() where TTo : TFrom;

        IDependencyContainer RegisterHttpRequestMessage(Func<HttpRequestMessage> getHttpRequestMessageFunc);

        object Resolve(Type serviceType);

        IEnumerable<object> ResolveAll(Type serviceType);

        IDependencyContainer CreateChildContainer();
    }
}
