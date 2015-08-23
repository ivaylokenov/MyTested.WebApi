namespace MyWebApi.Utilities
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Class for validating reflection checks.
    /// </summary>
    public static class Reflection
    {
        /// <summary>
        /// Checks whether two types are different.
        /// </summary>
        /// <param name="firstType">First type to be checked.</param>
        /// <param name="secondType">Second type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool AreDifferentTypes(Type firstType, Type secondType)
        {
            return firstType != secondType;
        }

        /// <summary>
        /// Checks whether two types are assignable.
        /// </summary>
        /// <param name="baseType">Base type to be checked.</param>
        /// <param name="inheritedType">Inherited type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool AreAssignable(Type baseType, Type inheritedType)
        {
            return baseType.IsAssignableFrom(inheritedType);
        }

        /// <summary>
        /// Checks whether two types are not assignable.
        /// </summary>
        /// <param name="baseType">Base type to be checked.</param>
        /// <param name="inheritedType">Inherited type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool AreNotAssignable(Type baseType, Type inheritedType)
        {
            return !AreAssignable(baseType, inheritedType);
        }

        /// <summary>
        /// Checks whether a type is generic.
        /// </summary>
        /// <param name="type">Type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool IsGeneric(Type type)
        {
            return type.IsGenericType;
        }

        /// <summary>
        /// Checks whether a type is not generic.
        /// </summary>
        /// <param name="type">Type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool IsNotGeneric(Type type)
        {
            return !IsGeneric(type);
        }

        /// <summary>
        /// Checks whether a type is generic definition.
        /// </summary>
        /// <param name="type">Type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool IsGenericTypeDefinition(Type type)
        {
            return type.IsGenericTypeDefinition;
        }

        /// <summary>
        /// Checks whether two types are assignable by generic definition.
        /// </summary>
        /// <param name="baseType">Base type to be checked.</param>
        /// <param name="inheritedType">Inherited type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool AreAssignableByGeneric(Type baseType, Type inheritedType)
        {
            return IsGeneric(inheritedType) && IsGeneric(baseType) &&
                   baseType.IsAssignableFrom(inheritedType.GetGenericTypeDefinition());
        }

        /// <summary>
        /// Checks whether two generic types have different generic arguments.
        /// </summary>
        /// <param name="baseType">Base type to be checked.</param>
        /// <param name="inheritedType">Inherited type to be checked.</param>
        /// <returns>True or false.</returns>
        public static bool HaveDifferentGenericArguments(Type baseType, Type inheritedType)
        {
            if (IsNotGeneric(baseType) || IsNotGeneric(inheritedType))
            {
                return true;
            }

            var baseTypeGenericArguments = baseType.GetGenericArguments();
            var inheritedTypeGenericArguments = inheritedType.GetGenericArguments();

            if (baseTypeGenericArguments.Length != inheritedTypeGenericArguments.Length)
            {
                return true;
            }

            return baseTypeGenericArguments.Where((t, i) => AreNotAssignable(t, inheritedTypeGenericArguments[i])).Any();
        }

        /// <summary>
        /// Performs dynamic casting from type to generic result.
        /// </summary>
        /// <typeparam name="TResult">Result type from casting.</typeparam>
        /// <param name="type">Type from which the casting should be done.</param>
        /// <param name="data">Object from which the casting should be done.</param>
        /// <returns>Casted object of type TResult.</returns>
        public static TResult CastTo<TResult>(this Type type, object data)
        {
            var dataParam = Expression.Parameter(typeof(object), "data");
            var firstConvert = Expression.Convert(dataParam, data.GetType());
            var secondConvert = Expression.Convert(firstConvert, type);
            var body = Expression.Block(new Expression[] { secondConvert });

            var run = Expression.Lambda(body, dataParam).Compile();
            var ret = run.DynamicInvoke(data);
            return (TResult)ret;
        }

        public static string ToFriendlyGenericTypeName(this Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            var genericArgumentNames = type.GetGenericArguments().Select(ga => ga.Name);
            var friendlyGenericName = type.Name.Split('`')[0];
            var joinedGenericArgumentNames = string.Join(", ", genericArgumentNames);

            return string.Format("{0}<{1}>", friendlyGenericName, joinedGenericArgumentNames);
        }
    }
}
