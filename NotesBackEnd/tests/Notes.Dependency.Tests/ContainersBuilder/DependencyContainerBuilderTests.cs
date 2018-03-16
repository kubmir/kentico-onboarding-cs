using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Dependencies;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Dependency;
using Notes.Dependency.Containers;
using Notes.Dependency.ContainersBuilders;
using NSubstitute;
using NUnit.Framework;

namespace Notes.Dependency.Tests.ContainersBuilder
{
    [TestFixture]
    class DependencyContainerBuilderTests
    {
        private static readonly List<Type> ExpectedTypes;
        private IEnumerable<Type> _actualTypes;
        private MockedIContainer _container;

        static DependencyContainerBuilderTests()
        {
            ExpectedTypes = typeof(IUrlLocationHelper)
                .Assembly
                .ExportedTypes
                .Where(type => type.IsInterface && type != typeof(IContainer) && type != typeof(IBootstrapper))
                .ToList();

            ExpectedTypes.Add(typeof(HttpRequestMessage));
            ExpectedTypes.Add(typeof(IDependencyResolver));
        }

        [SetUp]
        public void SetUp()
        {
            var routeManager = Substitute.For<IRouteOptions>();

            IRouteOptions GetRouteOptions()
                => routeManager;

            _container = MockContainer();

            DependencyContainerBuilder.RegisterApiDependencies(GetRouteOptions, _container);
            _actualTypes = _container.RegisteredTypes;
        }

        [TestCaseSource(nameof(ExpectedTypes))]
        public void RegisterApiDependencies_AllInterfacesAreRegistered(Type requestedType)
            => Assert.That(_actualTypes, Contains.Item(requestedType));

        private MockedIContainer MockContainer()
            => new MockedIContainer();
    }

    internal class MockedIContainer : IDependencyContainer
    {
        public List<Type> RegisteredTypes { get; }

        public MockedIContainer()
        {
            RegisteredTypes = new List<Type>();
        }

        public IContainer RegisterType<TFrom, TTo>(LifetimeTypes lifetimeType) where TTo : TFrom
        {
            RegisteredTypes.Add(typeof(TFrom));
            return this;
        }

        public IContainer RegisterType<TType>(Func<TType> getObjectFunc, LifetimeTypes lifetimeType)
        {
            RegisteredTypes.Add(typeof(TType));
            return this;
        }

        public void Dispose() { }

        public TType Resolve<TType>()
            => default(TType);

        public object Resolve(Type serviceType)
            => null;

        public IEnumerable<object> ResolveAll(Type serviceType)
            => null;

        public IResolver CreateChildContainer()
            => null;
    }
}
