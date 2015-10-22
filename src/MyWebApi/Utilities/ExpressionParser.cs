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
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Common;

    /// <summary>
    /// Utility class helping parsing expression trees.
    /// </summary>
    public static class ExpressionParser
    {
        /// <summary>
        /// Parses method name from method call lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Method name as string.</returns>
        public static string GetMethodName(LambdaExpression expression)
        {
            var methodCallExpression = GetMethodCallExpression(expression);
            return methodCallExpression.Method.Name;
        }

        /// <summary>
        /// Resolves arguments from method in method call lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Collection of method argument information.</returns>
        public static IEnumerable<MethodArgumentInfo> ResolveMethodArguments(LambdaExpression expression)
        {
            var methodCallExpression = GetMethodCallExpression(expression);
            return methodCallExpression.Arguments
                .Zip(methodCallExpression.Method.GetParameters(), 
                    (m, a) => new
                    {
                        a.Name,
                        Value = Expression.Lambda(m).Compile().DynamicInvoke()
                    })
                .Select(ma => new MethodArgumentInfo
                {
                    Name = ma.Name,
                    Type = ma.Value != null ? ma.Value.GetType() : null,
                    Value = ma.Value
                })
                .ToList();
        }

        /// <summary>
        /// Retrieves custom attributes on a method from method call lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Collection of attributes as objects.</returns>
        public static IEnumerable<object> GetMethodAttributes(LambdaExpression expression)
        {
            var methodCallExpression = GetMethodCallExpression(expression);
            return methodCallExpression.Method.GetCustomAttributes(true);
        }

        /// <summary>
        /// Parses member name from member lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Member name as string.</returns>
        public static string GetPropertyName(LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Provided expression is not a valid member expression.");
            }

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Gets method call expression from a lambda expression.
        /// </summary>
        /// <param name="expression">The lambda expression as MethodCallExpression.</param>
        /// <returns>Method call expression.</returns>
        public static MethodCallExpression GetMethodCallExpression(LambdaExpression expression)
        {
            var methodCallExpression = expression.Body as MethodCallExpression;
            if (methodCallExpression == null)
            {
                throw new ArgumentException("Provided expression is not a valid method call.");
            }

            return methodCallExpression;
        }
    }
}
