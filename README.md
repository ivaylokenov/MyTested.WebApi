<h1><img src="https://raw.githubusercontent.com/ivaylokenov/MyWebApi/master/documentation/logo.png" align="left" alt="MyWebApi" width="100">&nbsp;&nbsp;&nbsp; MyWebApi - ASP.NET Web API <br />&nbsp;&nbsp;&nbsp; Fluent Testing Framework</h1>
====================================

MyWebApi is unit testing library providing easy fluent interface to test the ASP.NET Web API 2 framework. Inspired by [TestStack.FluentMVCTesting](https://github.com/TestStack/TestStack.FluentMVCTesting) and [ChaiJS](https://github.com/chaijs/chai).

[![Build status](https://ci.appveyor.com/api/projects/status/738pm1kuuv7yw1t5?svg=true)](https://ci.appveyor.com/project/ivaylokenov/mywebapi)

## Documentation

Please see the [documentation](https://github.com/ivaylokenov/MyWebApi/tree/master/documentation) for full list of available features.

## Installation

You can install this library using NuGet into your Test class library. It will automatically reference the needed dependency of Microsoft.AspNet.WebApi.Core (≥ 5.1.0) for you. .NET 4.5+ is needed.

    Install-Package MyWebApi

## How to use

Make sure to check out [the documentation](https://github.com/ivaylokenov/MyWebApi/tree/master/documentation) for full list of available features.
You can also check out [the provided samples](https://github.com/ivaylokenov/MyWebApi/tree/master/samples) for real-life ASP.NET Web API application testing.

Basically you can create a test case by using the fluent API the library provides.

```c#
namespace MyApp.Tests.Controllers
{
    using MyApp.Controllers;
	using MyWebApi;
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

Here are some random examples of what the fluent testing API is capable of:

```c#
// injects dependencies into controller
// and mocks authenticated user
// and tests for valid model state
// and tests response model from Ok result with certain assertions
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
	
// tests whether the action returns internal server error
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
```

## Any questions, comments or additions?

Leave an issue on the [issues page](https://github.com/ivaylokenov/MyWebApi/issues) or send a [pull request](https://github.com/ivaylokenov/MyWebApi/pulls).