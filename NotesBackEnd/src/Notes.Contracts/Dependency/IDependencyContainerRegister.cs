using System;
using System.Net.Http;

namespace Notes.Contracts.Dependency
{
    public interface IDependencyContainerRegister
    {
        IDependencyContainerRegister RegisterType<TFrom, TTo>(LifetimeTypes lifetimeType) where TTo : TFrom;

        IDependencyContainerRegister RegisterType<TTo>(Func<TTo> getObjectFunc, LifetimeTypes lifetimeType);
    }
}
