namespace MyWebApi.Common.Extensions
{
    using System;
    using System.Collections.Generic;

    internal static class EnumerableExtensions
    {
        internal static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}
