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
            var methodCallExpression = GetMethodCallExpresstion(expression);
            return methodCallExpression.Method.Name;
        }

        /// <summary>
        /// Resolves arguments from method in method call lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Collection of type-value pairs.</returns>
        public static IEnumerable<TypeValuePair> ResolveMethodArguments(LambdaExpression expression)
        {
            var methodCallExpression = GetMethodCallExpresstion(expression);
            return methodCallExpression.Arguments
                .Select(argument => Expression.Lambda(argument).Compile().DynamicInvoke())
                .Select(value => new TypeValuePair
                {
                    Type = value.GetType(),
                    Value = value
                })
                .ToList();
        }

        private static MethodCallExpression GetMethodCallExpresstion(LambdaExpression expression)
        {
            var methodCallExpression = expression.Body as MethodCallExpression;
            if (methodCallExpression == null)
            {
                throw new ArgumentException("Provided expression is not a valid method call");
            }

            return methodCallExpression;
        }
    }
}
