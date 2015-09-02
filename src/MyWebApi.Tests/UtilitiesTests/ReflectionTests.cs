namespace MyWebApi.Tests.UtilitiesTests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Setups.Services;
    using Utilities;

    [TestFixture]
    public class ReflectionTests
    {
        [Test]
        public void AreDifferentTypesShouldReturnTrueWithObjectsOfDifferentTypes()
        {
            var first = 0;
            var second = "Test";

            Assert.IsTrue(Reflection.AreDifferentTypes(first, second));
        }

        [Test]
        public void AreDifferentTypesShouldReturnFalseWithObjectsOfSameTypes()
        {
            var first = 1;
            var second = 2;

            Assert.IsFalse(Reflection.AreDifferentTypes(first, second));
        }

        [Test]
        public void AreDifferentTypesShouldReturnTrueWithDifferentTypes()
        {
            var first = typeof(int);
            var second = typeof(string);

            Assert.IsTrue(Reflection.AreDifferentTypes(first, second));
        }

        [Test]
        public void AreDifferentTypesShouldReturnFalseWithSameTypes()
        {
            var first = typeof(List<>);
            var second = typeof(List<>);

            Assert.IsFalse(Reflection.AreDifferentTypes(first, second));
        }

        [Test]
        public void AreDifferentTypesShouldReturnFalseWithInheritedTypes()
        {
            var first = typeof(List<>);
            var second = typeof(IEnumerable<>);

            Assert.IsTrue(Reflection.AreDifferentTypes(first, second));
        }

        [Test]
        public void AreAssignableShouldReturnTrueWithTheSameTypes()
        {
            var first = typeof(int);
            var second = typeof(int);

            Assert.IsTrue(Reflection.AreAssignable(first, second));
            Assert.IsFalse(Reflection.AreNotAssignable(first, second));
        }

        [Test]
        public void AreAssignableShouldReturnTrueWithInheritedTypes()
        {
            var baseType = typeof(IEnumerable<int>);
            var inheritedType = typeof(IList<int>);

            Assert.IsTrue(Reflection.AreAssignable(baseType, inheritedType));
            Assert.IsFalse(Reflection.AreNotAssignable(baseType, inheritedType));
        }

        [Test]
        public void AreAssignableShouldReturnTrueWithInvertedInheritedTypes()
        {
            var baseType = typeof(IList<int>);
            var inheritedType = typeof(IEnumerable<int>);

            Assert.IsFalse(Reflection.AreAssignable(baseType, inheritedType));
            Assert.IsTrue(Reflection.AreNotAssignable(baseType, inheritedType));
        }

        [Test]
        public void AreAssignableShouldReturnFalseWithGenericTypeDefinitions()
        {
            var baseType = typeof(IEnumerable<>);
            var inheritedType = typeof(IList<>);

            Assert.IsFalse(Reflection.AreAssignable(baseType, inheritedType));
            Assert.IsTrue(Reflection.AreNotAssignable(baseType, inheritedType));
        }

        [Test]
        public void AreAssignableShouldReturnFalseWithOneGenericTypeDefinition()
        {
            var baseType = typeof(IEnumerable<>);
            var inheritedType = typeof(IList<int>);

            Assert.IsFalse(Reflection.AreAssignable(baseType, inheritedType));
            Assert.IsTrue(Reflection.AreNotAssignable(baseType, inheritedType));
        }

        [Test]
        public void IsGenericShouldReturnTrueWithGenericType()
        {
            var type = typeof(IEnumerable<int>);

            Assert.IsTrue(Reflection.IsGeneric(type));
            Assert.IsFalse(Reflection.IsNotGeneric(type));
        }

        [Test]
        public void IsGenericShouldReturnTrueWithGenericTypeDefinition()
        {
            var type = typeof(IEnumerable<>);

            Assert.IsTrue(Reflection.IsGeneric(type));
            Assert.IsFalse(Reflection.IsNotGeneric(type));
        }

        [Test]
        public void IsGenericShouldReturnFalseWithNonGenericType()
        {
            var type = typeof(object);

            Assert.IsFalse(Reflection.IsGeneric(type));
            Assert.IsTrue(Reflection.IsNotGeneric(type));
        }

        [Test]
        public void IsGenericTypeDefinitionShouldReturnFalseWithGenericType()
        {
            var type = typeof(IEnumerable<int>);

            Assert.IsFalse(Reflection.IsGenericTypeDefinition(type));
        }

        [Test]
        public void IsGenericTypeDefinitionShouldReturnTrueWithGenericTypeDefinition()
        {
            var type = typeof(IEnumerable<>);

            Assert.IsTrue(Reflection.IsGenericTypeDefinition(type));
        }

        [Test]
        public void IsGenericTypeDefinitionShouldReturnFalseWithNonGenericType()
        {
            var type = typeof(object);

            Assert.IsFalse(Reflection.IsGenericTypeDefinition(type));
        }

        [Test]
        public void HaveDifferentGenericArgumentsShouldReturnFalseWithSameGenericArguments()
        {
            var first = typeof(IEnumerable<int>);
            var second = typeof(IEnumerable<int>);

            Assert.IsFalse(Reflection.HaveDifferentGenericArguments(first, second));
        }

        [Test]
        public void HaveDifferentGenericArgumentsShouldReturnTrueWithSameDifferentArguments()
        {
            var first = typeof(IEnumerable<int>);
            var second = typeof(IEnumerable<string>);

            Assert.IsTrue(Reflection.HaveDifferentGenericArguments(first, second));
        }

        [Test]
        public void HaveDifferentGenericArgumentsShouldReturnTrueWithNoGenericArguments()
        {
            var first = typeof(object);
            var second = typeof(int);

            Assert.IsTrue(Reflection.HaveDifferentGenericArguments(first, second));
        }

        [Test]
        public void HaveDifferentGenericArgumentsShouldReturnTrueWithOneTypeHavingGenericArguments()
        {
            var first = typeof(IEnumerable<int>);
            var second = typeof(int);

            Assert.IsTrue(Reflection.HaveDifferentGenericArguments(first, second));
        }

        [Test]
        public void HaveDifferentGenericArgumentsShouldReturnTrueWithDifferentNumberOfGenericArguments()
        {
            var first = typeof(IEnumerable<int>);
            var second = typeof(IDictionary<int, string>);

            Assert.IsTrue(Reflection.HaveDifferentGenericArguments(first, second));
        }

        [Test]
        public void CastToShouldReturnCorrectCastWhenCastIsPossible()
        {
            IEnumerable<int> original = new List<int>();
            var cast = typeof(IEnumerable<int>).CastTo<List<int>>(original);

            Assert.AreEqual(typeof(List<int>), cast.GetType());
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void CastToShouldThrowExceptionWhenCastIsNotPossible()
        {
            IEnumerable<int> original = new List<int>();
            typeof(IEnumerable<int>).CastTo<int>(original);
        }

        [Test]
        public void ToFriendlyTypeNameShouldReturnTheOriginalNameWhenTypeIsNotGeneric()
        {
            var name = typeof(object).ToFriendlyTypeName();
            Assert.AreEqual("Object", name);
        }

        [Test]
        public void ToFriendlyTypeNameShouldReturnProperNameWhenTypeIsGenericWithoutArguments()
        {
            var name = typeof(List<>).ToFriendlyTypeName();
            Assert.AreEqual("List<T>", name);
        }

        [Test]
        public void ToFriendlyTypeNameShouldReturnProperNameWhenTypeIsGenericWithoutMoreThanOneArguments()
        {
            var name = typeof(Dictionary<,>).ToFriendlyTypeName();
            Assert.AreEqual("Dictionary<TKey, TValue>", name);
        }

        [Test]
        public void ToFriendlyTypeNameShouldReturnProperNameWhenTypeIsGenericWithOneArgument()
        {
            var name = typeof(List<int>).ToFriendlyTypeName();
            Assert.AreEqual("List<Int32>", name);
        }

        [Test]
        public void ToFriendlyTypeNameShouldReturnProperNameWhenTypeIsGenericWithMoreThanOneArguments()
        {
            var name = typeof(Dictionary<string, int>).ToFriendlyTypeName();
            Assert.AreEqual("Dictionary<String, Int32>", name);
        }

        [Test]
        public void TryGetInstanceShouldReturnObjectWithDefaultConstructorWhenNoParametersAreProvided()
        {
            var instance = Reflection.TryCreateInstance<WebApiController>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(WebApiController), instance.GetType());
            Assert.IsNull(instance.InjectedRequestModel);
            Assert.IsNotNull(instance.InjectedService);
        }

        [Test]
        public void TryGetInstanceShouldReturnCorrectInitializationWithPartOfAllParameters()
        {
            var instance = Reflection.TryCreateInstance<WebApiController>(new InjectedService());

            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(WebApiController), instance.GetType());
            Assert.IsNull(instance.InjectedRequestModel);
            Assert.IsNotNull(instance.InjectedService);
        }

        [Test]
        public void TryGetInstanceShouldReturnInitializedObjectWhenCorrectOrderOfParametersAreProvided()
        {
            var instance = Reflection.TryCreateInstance<WebApiController>(new InjectedService(), new RequestModel());

            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(WebApiController), instance.GetType());
            Assert.IsNotNull(instance.InjectedRequestModel);
            Assert.IsNotNull(instance.InjectedService);
        }

        [Test]
        public void TryCreateInstanceShouldReturnInitializedObjectWhenIncorrectOrderOfParametersAreProvided()
        {
            var instance = Reflection.TryCreateInstance<WebApiController>(new RequestModel(), new InjectedService());

            Assert.IsNotNull(instance);
            Assert.AreEqual(typeof(WebApiController), instance.GetType());
            Assert.IsNotNull(instance.InjectedRequestModel);
            Assert.IsNotNull(instance.InjectedService);
        }

        [Test]
        public void TryCreateInstanceShouldReturnNullWhenConstructorArgumentsDoNotMatch()
        {
            var instance = Reflection.TryCreateInstance<WebApiController>(new ResponseModel());

            Assert.IsNull(instance);
        }

        [Test]
        public void TryCreateInstanceShouldReturnNullWhenConstructorArgumentsDoNotMatchAndAreTooMany()
        {
            var instance = Reflection.TryCreateInstance<WebApiController>(new RequestModel(), new InjectedService(), new ResponseModel());

            Assert.IsNull(instance);
        }
    }
}
