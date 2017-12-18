using System;
using System.Collections.Generic;

namespace Notes.Contracts.Dependency
{
    public interface IDependencyContainer
    {
        IDependencyContainer RegisterType<TFrom>();

        IDependencyContainer RegisterType<TFrom, TTo>() where TTo : TFrom;

        object Resolve(Type serviceType);

        IEnumerable<object> ResolveAll(Type serviceType);

        IDependencyContainer CreateChildContainer();

        void Dispose();
    }
}
