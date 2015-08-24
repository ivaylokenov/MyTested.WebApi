namespace MyWebApi.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Actions;
    using Common.Identity;
    using Contracts;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for building the action which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
    public class ControllerBuilder<TController> : IControllerBuilder<TController>
        where TController : ApiController
    {
        private readonly IDictionary<Type, object> dependencies;
        private bool isPreparedForTesting;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBuilder{TController}" /> class.
        /// </summary>
        /// <param name="controllerInstance">Instance of the tested ASP.NET Web API controller.</param>
        public ControllerBuilder(TController controllerInstance)
        {
            this.Controller = controllerInstance;
            this.dependencies = new Dictionary<Type, object>();
            this.isPreparedForTesting = false;
        }

        /// <summary>
        /// Gets the ASP.NET Web API controller instance to be tested.
        /// </summary>
        /// <value>Instance of the ASP.NET Web API controller.</value>
        public TController Controller { get; private set; }

        public IControllerBuilder<TController> WithResolvedDependencyFor<TDependency>(TDependency dependency)
        {
            var typeOfDependency = typeof(TDependency);
            if (this.dependencies.ContainsKey(typeOfDependency))
            {
                throw new InvalidOperationException(string.Format(
                    "Dependency {0} was already registered for {1} controller.",
                    typeOfDependency.ToFriendlyTypeName(),
                    typeof(TController).ToFriendlyTypeName()));
            }

            this.dependencies.Add(typeOfDependency, dependency);
            return this;
        }

        /// <summary>
        /// Sets default authenticated user to the built controller with "TestUser" username.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IControllerBuilder<TController> WithAuthenticatedUser()
        {
            this.TryInstantiateController();
            this.Controller.User = MockedIPrinciple.CreateDefaultAuthenticated();
            return this;
        }

        /// <summary>
        /// Sets custom authenticated user using provided user builder.
        /// </summary>
        /// <param name="userBuilder">User builder to create mocked user object.</param>
        /// <returns>The same controller builder.</returns>
        public IControllerBuilder<TController> WithAuthenticatedUser(Action<IUserBuilder> userBuilder)
        {
            this.TryInstantiateController();
            var newUserBuilder = new UserBuilder();
            userBuilder(newUserBuilder);
            this.Controller.User = newUserBuilder.GetUser();
            return this;
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            this.TryInstantiateController();
            var actionName = ExpressionParser.GetMethodName(actionCall);
            this.ValidateModelState(actionCall);
            var actionResult = actionCall.Compile().Invoke(this.Controller);
            return new ActionResultTestBuilder<TActionResult>(this.Controller, actionName, actionResult);
        }

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Asynchronous Task result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        public IActionResultTestBuilder<TActionResult> CallingAsync<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall)
        {
            this.TryInstantiateController();
            var actionName = ExpressionParser.GetMethodName(actionCall);
            this.ValidateModelState(actionCall);
            var actionResult = actionCall.Compile().Invoke(this.Controller).Result;
            return new ActionResultTestBuilder<TActionResult>(this.Controller, actionName, actionResult);
        }

        private void TryInstantiateController()
        {
            if (this.Controller == null)
            {
                this.Controller = Reflection.TryCreateInstance<TController>(this.dependencies);
                if (this.Controller == null)
                {
                    var friendlyDependanciesNames = this.dependencies
                        .Keys
                        .Select(k => k.ToFriendlyTypeName());

                    throw new UnresolvedDependenciesException(string.Format(
                        "{0} controller could not be instantiated because it contains no constructor taking {1} as parameters.",
                        typeof(TController).ToFriendlyTypeName(),
                        string.Join(", ", friendlyDependanciesNames)));
                }
            }

            this.PrepareController();
        }

        private void PrepareController()
        {
            if (this.isPreparedForTesting)
            {
                return;
            }

            this.isPreparedForTesting = true;
            this.Controller.Request = new HttpRequestMessage();
            this.Controller.Configuration = new HttpConfiguration();
            this.Controller.User = MockedIPrinciple.CreateUnauthenticated();
        }

        private void ValidateModelState<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
        {
            var arguments = ExpressionParser.ResolveMethodArguments(actionCall);
            foreach (var argument in arguments)
            {
                this.Controller.Validate(argument.Value);
            }
        }
    }
}
