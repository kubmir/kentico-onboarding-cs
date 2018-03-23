using System;
using System.Collections.Generic;

namespace Notes.Contracts.Dependency
{
    public interface IResolver : IDisposable
    {
        TType Resolve<TType>();

        Object Resolve(Type serviceType);

        IEnumerable<Object> ResolveAll(Type serviceType);

        IResolver CreateChildContainer();
    }
}
