// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Builders.Contracts.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Actions;
    using HttpRequests;

    /// <summary>
    /// Used for building the action which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
    public interface IControllerBuilder<TController>
        where TController : ApiController
    {
        /// <summary>
        /// Gets ASP.NET Web API controller instance to be tested.
        /// </summary>
        /// <value>Instance of the ASP.NET Web API controller.</value>
        TController Controller { get; }

        /// <summary>
        /// Adds HTTP request message to the tested controller.
        /// </summary>
        /// <param name="httpRequestBuilder">Builder for HTTP request message.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithHttpRequestMessage(Action<IHttpRequestMessageBuilder> httpRequestBuilder);

        /// <summary>
        /// Tries to resolve constructor dependency of given type.
        /// </summary>
        /// <typeparam name="TDependency">Type of dependency to resolve.</typeparam>
        /// <param name="dependency">Instance of dependency to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithResolvedDependencyFor<TDependency>(TDependency dependency);

        /// <summary>
        /// Tries to resolve constructor dependencies by the provided collection of dependencies.
        /// </summary>
        /// <param name="dependencies">Collection of dependencies to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithResolvedDependencies(IEnumerable<object> dependencies);

        /// <summary>
        /// Tries to resolve constructor dependencies by the provided dependencies.
        /// </summary>
        /// <param name="dependencies">Dependencies to inject into constructor.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithResolvedDependencies(params object[] dependencies);

        /// <summary>
        /// Disables ModelState validation for the action call.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithoutValidation();

        /// <summary>
        /// Sets default authenticated user to the built controller with "TestUser" username.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithAuthenticatedUser();

        /// <summary>
        /// Sets custom authenticated user using provided user builder.
        /// </summary>
        /// <param name="userBuilder">User builder to create mocked user object.</param>
        /// <returns>The same controller builder.</returns>
        IAndControllerBuilder<TController> WithAuthenticatedUser(Action<IUserBuilder> userBuilder);
            
        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <typeparam name="TActionResult">Asynchronous Task result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing the action result.</returns>
        IActionResultTestBuilder<TActionResult> CallingAsync<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing void actions.</returns>
        IVoidActionResultTestBuilder Calling(Expression<Action<TController>> actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Builder for testing void actions.</returns>
        IVoidActionResultTestBuilder CallingAsync(Expression<Func<TController, Task>> actionCall);
    }
}
