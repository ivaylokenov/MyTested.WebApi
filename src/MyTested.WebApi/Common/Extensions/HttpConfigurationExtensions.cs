namespace MyTested.WebApi.Common.Extensions
{
    using System.Web.Http;

    /// <summary>
    /// Provides extension methods to HttpConfiguration.
    /// </summary>
    public static class HttpConfigurationExtensions
    {
        /// <summary>
        /// Tries to resolve a type by using the configuration's IDependencyResolver. 
        /// </summary>
        /// <typeparam name="T">Type to resolve.</typeparam>
        /// <param name="configuration">Instance of HttpConfiguration.</param>
        /// <returns>Resolved instance of T or null, if unsuccessful.</returns>
        public static T TryResolve<T>(this HttpConfiguration configuration)
            where T : class 
        {
            if (configuration != null && configuration.DependencyResolver != null)
            {
                return configuration
                    .DependencyResolver
                    .BeginScope()
                    .GetService(typeof(T)) as T;
            }

            return null;
        }
    }
}
