namespace MyWebApi.Utilities
{
    using System.Linq.Expressions;

    /// <summary>
    /// Utility class helping parsing expression trees.
    /// </summary>
    internal static class ExpressionParser
    {
        /// <summary>
        /// Parses method name from method call lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Method name as string.</returns>
        internal static string GetMethodName(LambdaExpression expression)
        {
            return ((MethodCallExpression)expression.Body).Method.Name;
        }
    }
}
