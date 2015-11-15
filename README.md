<h1><img src="https://raw.githubusercontent.com/ivaylokenov/MyTested.WebApi/master/documentation/logo.png" align="left" alt="MyTested.WebApi" width="100">&nbsp; MyTested.WebApi - ASP.NET<br />&nbsp; Web API Fluent Testing Framework</h1>
====================================

MyTested.WebApi is unit testing library providing easy fluent interface to test the ASP.NET Web API 2 framework. It is testing framework agnostic, so you can combine it with the testing library of your choice (e.g. NUnit, xUnit, etc.). Inspired by [ChaiJS](https://github.com/chaijs/chai).

[![Build status](https://ci.appveyor.com/api/projects/status/738pm1kuuv7yw1t5?svg=true)](https://ci.appveyor.com/project/ivaylokenov/mywebapi) [![NuGet Version](http://img.shields.io/nuget/v/MyTested.WebApi.svg?style=flat)](https://www.nuget.org/packages/MyTested.WebApi/) [![NuGet Downloads](http://img.shields.io/nuget/dt/MyTested.WebApi.svg?style=flat)](https://www.nuget.org/packages/MyTested.WebApi/) [![Coverage Status](https://coveralls.io/repos/ivaylokenov/MyTested.WebApi/badge.svg?branch=master&service=github&v=2)](https://coveralls.io/github/ivaylokenov/MyTested.WebApi?branch=master)

## Documentation

Please see the [documentation](https://github.com/ivaylokenov/MyTested.WebApi/tree/master/documentation) for full list of available features. Everything listed in the documentation is 100% covered by [more than 800 unit tests](https://github.com/ivaylokenov/MyTested.WebApi/tree/master/src/MyTested.WebApi.Tests) and should work correctly. Almost all items in the [issues page](https://github.com/ivaylokenov/MyTested.WebApi/issues) are expected future features and enhancements.

## Installation

You can install this library using NuGet into your Test class project. It will automatically reference the needed dependencies of Microsoft.AspNet.WebApi.Core (≥ 5.1.0) and Microsoft.Owin.Testing (≥ 3.0.1) for you. .NET 4.5+ is needed. Make sure your solution has the same versions of the mentioned dependencies in all projects where you are using them. For example, if you are using Microsoft.AspNet.WebApi.Core 5.2.3 in your Web project, the same version should be used after installing MyTested.WebApi in your Tests project.

    Install-Package MyTested.WebApi

After the downloading is complete, just add `using MyTested.WebApi;` and you are ready to test in the most elegant and developer friendly way.
	
    using MyTested.WebApi;
	
## How to use

Make sure to check out [the documentation](https://github.com/ivaylokenov/MyTested.WebApi/tree/master/documentation) for full list of available features.
You can also check out [the provided samples](https://github.com/ivaylokenov/MyTested.WebApi/tree/master/samples) for real-life ASP.NET Web API application testing.

Basically you can create a test case by using the fluent API the library provides. You are given a static `MyWebApi` class from which all assertions can be easily configured.

```c#
namespace MyApp.Tests.Controllers
{
	using MyTested.WebApi;
	
    using MyApp.Controllers;
	using NUnit.Framework;

    [TestFixture]
    public class HomeControllerShould
    {
        [Test]
        public void ReturnOkWhenCallingGetAction()
        {
            MyWebApi
                .Controller<HomeController>()
                .Calling(c => c.Get())
                .ShouldReturn()
				.Ok();
        }
	}
}
```

The example uses NUnit but you can use whatever testing framework you want.
Basically, MyTested.WebApi throws an unhandled exception if the assertion does not pass and the test fails.

Here are some random examples of what the fluent testing API is capable of:

```c#
// tests a route for correct controller, action and resolved route values
MyWebApi
	.Routes()
	.ShouldMap("api/WebApiController/SomeAction/5")
	.WithJsonContent(@"{""SomeInt"": 1, ""SomeString"": ""Test""}")
	.And()
	.WithHttpMethod(HttpMethod.Post)
	.To<WebApiController>(c => c.SomeAction(5, new RequestModel
	{
		SomeInt = 1,
		SomeString = "Test"
	}))
	.AndAlso()
	.ToNoHandler()
	.AndAlso()
	.ToValidModelState();

// injects dependencies into controller
// and mocks authenticated user
// and tests for valid model state
// and tests response model from Ok result with specific assertions
MyWebApi
	.Controller<WebApiController>()
	.WithResolvedDependencyFor<IInjectedService>(mockedInjectedService)
	.WithResolvedDependencyFor<IAnotherInjectedService>(anotherMockedInjectedService);
	.WithAuthenticatedUser(user => user.WithUsername("NewUserName"))
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHave()
	.ValidModelState()
	.AndAlso()
	.ShouldReturn()
	.Ok()
	.WithResponseModelOfType<ResponseModel>()
	.Passing(m =>
	{
		Assert.AreEqual(1, m.Id);
		Assert.AreEqual("Some property value", m.SomeProperty);
	});;

// tests whether model state error exists by using lambda expression
// and specific tests for the error messages
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHave()
	.ModelStateFor<RequestModel>()
	.ContainingModelStateErrorFor(m => m.SomeProperty).ThatEquals("Error message") 
	.AndAlso()
	.ContainingModelStateErrorFor(m => m.SecondProperty).BeginningWith("Error") 
	.AndAlso()
	.ContainingModelStateErrorFor(m => m.ThirdProperty).EndingWith("message") 
	.AndAlso()
	.ContainingModelStateErrorFor(m => m.SecondProperty).Containing("ror mes"); 
	
// tests whether the action throws internal server error
// with exception of certain type and with certain message
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.InternalServerError()
	.WithException()
	.OfType<SomeException>()
	.AndAlso()
	.WithMessage("Some exception message");
	
// run full pipeline integration test
MyWebApi
	.Server()
	.Working(httpConfiguration)
	.WithHttpRequestMessage(
		request => request
			.WithMethod(HttpMethod.Post)
			.WithRequestUri("api/WebApiController/SomeAction/1"))
	.ShouldReturnHttpResponseMessage()
	.WithStatusCode(HttpStatusCode.OK)
	.AndAlso()
	.ContainingHeader("MyCustomHeader");
```

## License

Code by Ivaylo Kenov. Copyright 2015 Ivaylo Kenov.

This library is intended to be used in both open-source and commercial environments. To allow its use in as many
situations as possible, MyTested.WebApi is dual-licensed. You may choose to use MyTested.WebApi under either the Apache License,
Version 2.0, or the Microsoft Public License (Ms-PL). These licenses are essentially identical, but you are
encouraged to evaluate both to determine which best fits your intended use.

Refer to [LICENSE](https://github.com/ivaylokenov/MyTested.WebApi/blob/master/LICENSE) for detailed information.
 
## Any questions, comments or additions?

If you have a feature request or bug report, leave an issue on the [issues page](https://github.com/ivaylokenov/MyTested.WebApi/issues) or send a [pull request](https://github.com/ivaylokenov/MyTested.WebApi/pulls). For general questions and comments, use the [StackOverflow](http://stackoverflow.com/) forum.
