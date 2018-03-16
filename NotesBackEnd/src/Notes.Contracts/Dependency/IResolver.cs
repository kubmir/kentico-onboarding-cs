using System;
using System.Collections.Generic;

namespace Notes.Contracts.Dependency
{
    public interface IResolver : IDisposable
    {
        TType Resolve<TType>();

        object Resolve(Type serviceType);

        IEnumerable<object> ResolveAll(Type serviceType);

        IResolver CreateChildContainer();
    }
}
