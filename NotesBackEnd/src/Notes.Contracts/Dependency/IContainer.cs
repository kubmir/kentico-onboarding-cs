using System;

namespace Notes.Contracts.Dependency
{
    public interface IContainer
    {
        IContainer RegisterType<TFrom, TTo>(LifetimeTypes lifetimeType) where TTo : TFrom;

        IContainer RegisterType<TType>(Func<TType> getObjectFunc, LifetimeTypes lifetimeType);
    }
}
