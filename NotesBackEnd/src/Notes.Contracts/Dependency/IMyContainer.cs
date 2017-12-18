using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Notes.Contracts.Dependency
{
    public interface IMyContainer
    {
        IMyContainer RegisterType<TFrom>();

        IMyContainer RegisterType<TFrom, TTo>() where TTo : TFrom;

        object Resolve(Type serviceType);

        IEnumerable<object> ResolveAll(Type serviceType);

        IMyContainer CreateChildContainer();

        void Dispose();
    }
}
