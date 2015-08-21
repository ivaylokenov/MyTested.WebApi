namespace MyWebApi.Utilities
{
    using System;
    using System.Linq;

    /// <summary>
    /// Class for validating reflection checks.
    /// </summary>
    public static class ReflectionChecker
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
        public static bool AreAssignableByGenericDefinition(Type baseType, Type inheritedType)
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
    }
}
