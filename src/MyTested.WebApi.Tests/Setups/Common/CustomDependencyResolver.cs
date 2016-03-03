namespace MyTested.WebApi.Tests.Setups.Common
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;
    using Controllers;
    using Services;

    public class CustomDependencyResolver : IDependencyResolver
    {
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(NoParameterlessConstructorController))
            {
                return new NoParameterlessConstructorController(new InjectedService());
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return null;
        }

        public void Dispose()
        {
        }
    }
}
