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

namespace MyWebApi.Utilities
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Http;

    public static class RouteValuesParser
    {
        public static void Parse<TController>(LambdaExpression lambdaExpression)
            where TController : ApiController
        {
            var methodCallExpression = ExpressionParser.GetMethodCallExpression(lambdaExpression);
            var controllerName = ParseControllerName(typeof(TController));
            var actionName = ParseActionName(methodCallExpression);
        }

        private static string ParseControllerName(Type controllerType)
        {
            const string controllerSuffix = "Controller";
            var controllerName = controllerType.Name;
            if ((controllerName.Length > controllerSuffix.Length) && controllerName.EndsWith("Controller"))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - controllerSuffix.Length);
            }

            return controllerName;
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
