// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Utilities.RouteResolvers
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Http;
    using Common.Routes;

    /// <summary>
    /// Used for parsing lambda expression to route values.
    /// </summary>
    public static class RouteExpressionParser
    {
        /// <summary>
        /// Parses route values from a lambda expression.
        /// </summary>
        /// <typeparam name="TController">Type of API Controller.</typeparam>
        /// <param name="lambdaExpression">The lambda expression to parse.</param>
        /// <returns>Parsed route information.</returns>
        public static ExpressionParsedRouteInfo Parse<TController>(LambdaExpression lambdaExpression)
            where TController : ApiController
        {
            var methodCallExpression = ExpressionParser.GetMethodCallExpression(lambdaExpression);
            var controllerType = typeof(TController);
            var actionName = ParseActionName(methodCallExpression);
            var actionArgumentsInfo = ExpressionParser.ResolveMethodArguments(lambdaExpression);

            return new ExpressionParsedRouteInfo(
                controllerType,
                actionName,
                actionArgumentsInfo);
        }

        private static string ParseActionName(MethodCallExpression methodCallExpression)
        {
            var actionNameAttribute = methodCallExpression
                .Method
                .GetCustomAttributes(typeof(ActionNameAttribute), true)
                .FirstOrDefault();

            return actionNameAttribute == null 
                ? methodCallExpression.Method.Name 
                : ((ActionNameAttribute)actionNameAttribute).Name;
        }
    }
}
