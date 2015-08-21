namespace MyWebApi.Utilities
{
    using System.Linq.Expressions;

    internal static class ExpressionParser
    {
        internal static string GetMethodName(LambdaExpression expression)
        {
            return ((MethodCallExpression)expression.Body).Method.Name;
        }
    }
}
