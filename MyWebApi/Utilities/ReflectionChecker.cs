namespace MyWebApi.Utilities
{
    using System;
    using System.Linq;

    public static class ReflectionChecker
    {
        public static bool AreDifferentTypes(Type firstType, Type secondType)
        {
            return firstType != secondType;
        }

        public static bool AreAssignable(Type baseType, Type inheritedType)
        {
            return baseType.IsAssignableFrom(inheritedType);
        }

        public static bool AreNotAssignable(Type baseType, Type inheritedType)
        {
            return !AreAssignable(baseType, inheritedType);
        }

        public static bool IsGeneric(Type type)
        {
            return type.IsGenericType;
        }

        public static bool IsNotGeneric(Type type)
        {
            return !IsGeneric(type);
        }

        public static bool IsGenericTypeDefinition(Type type)
        {
            return type.IsGenericTypeDefinition;
        }

        public static bool AreAssignableByGenericDefinition(Type baseType, Type inheritedType)
        {
            return IsGeneric(inheritedType) && IsGeneric(baseType) &&
                   baseType.IsAssignableFrom(inheritedType.GetGenericTypeDefinition());
        }

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
