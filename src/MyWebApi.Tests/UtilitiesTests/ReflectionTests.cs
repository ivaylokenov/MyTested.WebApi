// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Tests.UtilitiesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using NUnit.Framework;
    using Setups.Controllers;
    using Setups.Models;
    using Setups.Services;
    using Utilities;

    [TestFixture]
    public class ReflectionTests
    {
        [Test]
        public void AreSameTypesShouldReturnTrueWithObjectsOfSameTypes()
        {
            var first = "Test";
            var second = "Another Test";

            Assert.IsTrue(Reflection.AreSameTypes(first, second));
        }

        [Test]
        public void AreSameTypesShouldReturnFalseWithObjectsOfDifferentTypes()
        {
            var first = 1;
            var second = "Test";

            Assert.IsFalse(Reflection.AreSameTypes(first, second));
        }

        [Test]
        public void AreSameTypesShouldReturnTrueWithSameTypes()
        {
            var first = typeof(int);
            var second = typeof(int);

            Assert.IsTrue(Reflection.AreSameTypes(first, second));
        }

        [Test]
        public void AreSameTypesShouldReturnFalseWithDifferentTypes()
        {
            var first = typeof(List<>);
            var second = typeof(int);

            Assert.IsFalse(Reflection.AreSameTypes(first, second));
        }

        [Test]
        public void AreSameTypesShouldReturnFalseWithInheritedTypes()
        {
            var first = typeof(List<>);
            var second = typeof(IEnumerable<>);

            Assert.IsFalse(Reflection.AreSameTypes(first, second));
        }

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
        public void ContainsGenericTypeDefinitionInterfacesShouldReturnTrueWithValidInterfaces()
        {
            var result = Reflection.ContainsGenericTypeDefinitionInterface(typeof(IEnumerable<>), typeof(List<>));
            Assert.IsTrue(result);
        }

        [Test]
        public void ContainsGenericTypeDefinitionInterfacesShouldReturnFalseWithInvalidInterfaces()
        {
            var result = Reflection.ContainsGenericTypeDefinitionInterface(typeof(IEnumerable<>), typeof(Array));
            Assert.IsFalse(result);
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

        [Test]
        public void GetCustomAttributesShouldReturnProperAttributeTypes()
        {
            var attributes = Reflection.GetCustomAttributes(new WebApiController()).ToList();
            var attributeTypes = attributes.Select(a => a.GetType()).ToList();

            Assert.IsNotNull(attributes);
            Assert.AreEqual(2, attributes.Count());
            Assert.IsTrue(attributeTypes.Contains(typeof(AuthorizeAttribute)));
            Assert.IsTrue(attributeTypes.Contains(typeof(RoutePrefixAttribute)));
        }

        [Test]
        public void AreDeeplyEqualShouldWorkCorrectlyWithPrimitiveAndStructTypes()
        {
            Assert.IsTrue(Reflection.AreDeeplyEqual(1, 1));
            Assert.IsTrue(Reflection.AreDeeplyEqual(null, null));
            Assert.IsTrue(Reflection.AreDeeplyEqual("test", "test"));
            Assert.IsTrue(Reflection.AreDeeplyEqual('a', 'a'));
            Assert.IsTrue(Reflection.AreDeeplyEqual(1.1, 1.1));
            Assert.IsTrue(Reflection.AreDeeplyEqual(1.1m, 1.1m));
            Assert.IsTrue(Reflection.AreDeeplyEqual(true, true));
            Assert.IsTrue(Reflection.AreDeeplyEqual(new DateTime(2015, 10, 19), new DateTime(2015, 10, 19)));
            Assert.IsFalse(Reflection.AreDeeplyEqual(1, 0));
            Assert.IsFalse(Reflection.AreDeeplyEqual(1, null));
            Assert.IsFalse(Reflection.AreDeeplyEqual("test1", "test2"));
            Assert.IsFalse(Reflection.AreDeeplyEqual('a', 'b'));
            Assert.IsFalse(Reflection.AreDeeplyEqual(1.1, 1.2));
            Assert.IsFalse(Reflection.AreDeeplyEqual(1.1m, 1.2m));
            Assert.IsFalse(Reflection.AreDeeplyEqual(true, false));
            Assert.IsFalse(Reflection.AreDeeplyEqual(1, "1"));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new DateTime(2015, 10, 19), new DateTime(2015, 10, 20)));
        }

        [Test]
        public void AreDeeplyEqualsShouldWorkCorrectlyWithNormalObjects()
        {
            Assert.IsTrue(Reflection.AreDeeplyEqual(new object(), new object()));
            Assert.IsTrue(Reflection.AreDeeplyEqual((object)5, (object)5));
            Assert.IsTrue(Reflection.AreDeeplyEqual((object)5, 5));
            Assert.IsTrue(Reflection.AreDeeplyEqual(new RequestModel { Integer = 1 }, new RequestModel { Integer = 1 }));
            Assert.IsTrue(Reflection.AreDeeplyEqual(new RequestModel { Integer = 1, NonRequiredString = "test" }, new RequestModel { Integer = 1, NonRequiredString = "test" }));
            Assert.IsTrue(Reflection.AreDeeplyEqual(new GenericComparableModel { Integer = 1, String = "test" }, new GenericComparableModel { Integer = 1, String = "another" }));
            Assert.IsTrue(Reflection.AreDeeplyEqual(new ComparableModel { Integer = 1, String = "test" }, new ComparableModel { Integer = 1, String = "another" }));
            Assert.IsTrue(Reflection.AreDeeplyEqual(new EqualsModel { Integer = 1, String = "test" }, new EqualsModel { Integer = 1, String = "another" }));
            Assert.IsTrue(Reflection.AreDeeplyEqual(new EqualityOperatorModel { Integer = 1, String = "test" }, new EqualityOperatorModel { Integer = 1, String = "another" }));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new object(), "test"));
            Assert.IsFalse(Reflection.AreDeeplyEqual(DateTime.Now, "test"));
            Assert.IsFalse(Reflection.AreDeeplyEqual("test", DateTime.Now));
            Assert.IsFalse(Reflection.AreDeeplyEqual(true, new object()));
            Assert.IsFalse(Reflection.AreDeeplyEqual("test", new object()));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new object(), true));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new RequestModel { Integer = 2 }, new RequestModel { Integer = 1 }));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new object(), new RequestModel { Integer = 1 }));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new RequestModel { Integer = 2 }, new object()));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new RequestModel { Integer = 2, NonRequiredString = "test" }, new RequestModel { Integer = 1 }));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new GenericComparableModel { Integer = 1, String = "test" }, new GenericComparableModel { Integer = 2, String = "test" }));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new ComparableModel { Integer = 1, String = "test" }, new ComparableModel { Integer = 2, String = "test" }));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new EqualsModel { Integer = 1, String = "test" }, new EqualsModel { Integer = 2, String = "test" }));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new EqualityOperatorModel { Integer = 1, String = "test" }, new EqualityOperatorModel { Integer = 2, String = "test" }));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new ComparableModel { Integer = 1, String = "test" }, new RequestModel()));
        }

        [Test]
        public void AreDeeplyEqualsShouldWorkCorrectlyWithNestedObjects()
        {
            Assert.IsTrue(Reflection.AreDeeplyEqual(
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                }, 
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                }));

            Assert.IsFalse(Reflection.AreDeeplyEqual(
                new NestedModel
                {
                    Integer = 1,
                    String = "test",
                    Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test",
                    Nested = new NestedModel { Integer = 2, String = "test1", Nested = new NestedModel { Integer = 3, String = "test3" } }
                }));

            Assert.IsFalse(Reflection.AreDeeplyEqual(
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test2" } }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                }));
        }

        [Test]
        public void AreDeepEqualShouldWorkCorrectlyWithCollections()
        {
            Assert.IsTrue(Reflection.AreDeeplyEqual(
                new List<NestedModel>
                {
                    new NestedModel
                    {
                        Integer = 1, String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    },
                    new NestedModel
                    {
                        Integer = 1,
                        String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }
                },
                new List<NestedModel>
                {
                    new NestedModel
                    {
                        Integer = 1, String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    },
                    new NestedModel
                    {
                        Integer = 1,
                        String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }
                }));

            var listOfNestedModels = new List<NestedModel>
            {
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                }
            };

            var arrayOfNestedModels = new[]
            {
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                }
            };

            Assert.IsTrue(Reflection.AreDeeplyEqual(listOfNestedModels, arrayOfNestedModels));

            Assert.IsTrue(Reflection.AreDeeplyEqual(
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                },
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                }));

            Assert.IsTrue(Reflection.AreDeeplyEqual(
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                },
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new[]
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                }));

            Assert.IsFalse(Reflection.AreDeeplyEqual(
                new List<NestedModel>
                {
                    new NestedModel
                    {
                        Integer = 1, String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }, new NestedModel
                    {
                        Integer = 1,
                        String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }
                },
                new List<NestedModel>
                {
                    new NestedModel
                    {
                        Integer = 1, String = "test2",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }, new NestedModel
                    {
                        Integer = 1,
                        String = "test1",
                        Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                    }
                }));

            listOfNestedModels = new List<NestedModel>
            {
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                }
            };

            arrayOfNestedModels = new[]
            {
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 2,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                },
                new NestedModel
                {
                    Integer = 1,
                    String = "test1",
                    Nested =
                        new NestedModel
                        {
                            Integer = 4,
                            String = "test2",
                            Nested = new NestedModel { Integer = 3, String = "test3" }
                        }
                }
            };

            Assert.IsFalse(Reflection.AreDeeplyEqual(listOfNestedModels, arrayOfNestedModels));

            Assert.IsFalse(Reflection.AreDeeplyEqual(
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                },
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 5, String = "test3" } }
                        }
                    }
                }));

            Assert.IsFalse(Reflection.AreDeeplyEqual(
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new List<NestedModel>
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                },
                new NestedCollection
                {
                    Integer = 1,
                    String = "test",
                    Nested = new[]
                    {
                        new NestedModel
                        {
                            Integer = 1, String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test2", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }, new NestedModel
                        {
                            Integer = 1,
                            String = "test1",
                            Nested = new NestedModel { Integer = 2, String = "test3", Nested = new NestedModel { Integer = 3, String = "test3" } }
                        }
                    }
                }));

            Assert.IsTrue(Reflection.AreDeeplyEqual(new List<int> { 1, 2, 3 }, new[] { 1, 2, 3 }));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new List<int> { 1, 2, 3, 4 }, new[] { 1, 2, 3 }));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new List<int>(), new object()));
            Assert.IsFalse(Reflection.AreDeeplyEqual(new object(), new List<int>()));
        }
    }
}
