using System;

namespace Notes.Contracts.Dependency
{
    public interface IDependencyContainerRegister
    {
        IDependencyContainerRegister RegisterType<TFrom, TTo>(LifetimeTypes lifetimeType) where TTo : TFrom;

        IDependencyContainerRegister RegisterType<TType>(Func<TType> getObjectFunc, LifetimeTypes lifetimeType);
    }
}
