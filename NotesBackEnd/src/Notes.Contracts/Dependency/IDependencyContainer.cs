using System;
using System.Collections.Generic;

namespace Notes.Contracts.Dependency
{
    public interface IDependencyContainer
    {
        IDependencyContainer RegisterType<TType>(object injectedObject);

        IDependencyContainer RegisterType<TFrom, TTo>() where TTo : TFrom;

        IDependencyContainer RegisterHttpRequestMessage<TType>();

        object Resolve(Type serviceType);

        IEnumerable<object> ResolveAll(Type serviceType);

        IDependencyContainer CreateChildContainer();

        void Dispose();
    }
}
