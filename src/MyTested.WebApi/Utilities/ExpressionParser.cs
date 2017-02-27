// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Utilities
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
        internal const string IgnoredArgument = "!__Ignored_Value__!";

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
            if (!methodCallExpression.Arguments.Any())
            {
                return Enumerable.Empty<MethodArgumentInfo>();
            }

            return methodCallExpression.Arguments
                .Zip(
                    methodCallExpression.Method.GetParameters(), 
                    (m, a) => new
                    {
                        a.Name,
                        Value = ResolveExpressionValue(m)
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
        /// Tries to resolve expression value depending on the expression type.
        /// </summary>
        /// <param name="expression">Expression to resolve.</param>
        /// <returns>Value extracted from the provided expression.</returns>
        public static object ResolveExpressionValue(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Convert)
            {
                // Expression which contains converting from type to type
                var expressionArgumentAsUnary = (UnaryExpression)expression;
                expression = expressionArgumentAsUnary.Operand;
            }

            if (expression.NodeType == ExpressionType.Call)
            {
                // Expression of type c => c.Action(With.No<int>()) - value should be ignored and can be skipped.
                var expressionArgumentAsMethodCall = (MethodCallExpression)expression;
                if (expressionArgumentAsMethodCall.Object == null
                    && expressionArgumentAsMethodCall.Method.DeclaringType == typeof(With))
                {
                    return IgnoredArgument;
                }
            }

            object value;
            if (expression.NodeType == ExpressionType.Constant)
            {
                // Expression of type c => c.Action({const}) - value can be extracted without compiling.
                value = ((ConstantExpression)expression).Value;
            }
            else
            {
                // Expresion needs compiling because it is not of constant type.
                var convertExpression = Expression.Convert(expression, typeof(object));
                value = Expression.Lambda<Func<object>>(convertExpression).Compile().Invoke();
            }

            return value;
        }

        /// <summary>
        /// Retrieves custom attributes on a method from method call lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Collection of attributes as objects.</returns>
        public static IEnumerable<object> GetMethodAttributes(LambdaExpression expression)
        {
            var methodCallExpression = GetMethodCallExpression(expression);
            return Reflection.GetCustomAttributes(methodCallExpression.Method);
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
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    methodCallExpression = unaryExpression.Operand as MethodCallExpression;
                }
            }

            if (methodCallExpression == null)
            {
                throw new ArgumentException("Provided expression is not a valid method call.");
            } 
            
            var objectInstance = methodCallExpression.Object;
            if (objectInstance == null)
            {
                throw new InvalidOperationException("Provided expression is not valid - expected instance method call but instead received static method call.");
            }

            return methodCallExpression;
        }
    }
}
