<h1><img src="https://raw.githubusercontent.com/ivaylokenov/MyTested.WebApi/master/documentation/logo.png" align="left" alt="MyTested.WebApi" width="100">&nbsp; MyTested.WebApi - Fluent testing<br />&nbsp; framework for ASP.NET Web API 2</h1>

## Gold Sponsors

<table>
  <tbody>
    <tr>
      <td align="center" valign="middle">
        <a href="https://softuni.org/" target="_blank">
          <img width="148px" src="https://softuni.org/platform/assets/icons/logo.svg">
        </a>
      </td>
	    <td align="center" valign="middle">
        <a href="http://bit.ly/30xsnsC" target="_blank">
          <img width="148px" src="https://user-images.githubusercontent.com/3391906/65251792-dd848800-daef-11e9-8857-637a48048cda.png">
        </a>
      </td>
      <td align="center" valign="middle">
          <a href="http://noblehire.io?utm_medium=social&utm_source=projects&utm_campaign=platform-leads-knv" target="_blank">
          <img width="148px" src="https://user-images.githubusercontent.com/3391906/66911480-4260ce80-f019-11e9-9048-9aef112a1512.png">
        </a>
      </td>
    </tr>
  </tbody>
</table>

## Project Description

**MyTested.WebApi** is a unit testing library providing easy fluent interface to test the ASP.NET Web API 2 framework. It is testing framework agnostic, so you can combine it with a test runner of your choice (e.g. NUnit, xUnit, etc.). Inspired by [ChaiJS](https://github.com/chaijs/chai).

[![Build status](https://ci.appveyor.com/api/projects/status/738pm1kuuv7yw1t5?svg=true)](https://ci.appveyor.com/project/ivaylokenov/mywebapi) [![NuGet Version](http://img.shields.io/nuget/v/MyTested.WebApi.svg?style=flat)](https://www.nuget.org/packages/MyTested.WebApi/)  [![Coverage Status](https://coveralls.io/repos/ivaylokenov/MyTested.WebApi/badge.svg?branch=master&service=github&v=2)](https://coveralls.io/github/ivaylokenov/MyTested.WebApi?branch=master) [![License](https://img.shields.io/badge/license-apache-blue.svg)](http://www.apache.org/licenses/LICENSE-2.0)

## Sponsors &amp; Backers

**MyTested.WebApi** is a community-driven open source library. It's an independent project with its ongoing development made possible thanks to the support by these awesome [backers](https://github.com/ivaylokenov/MyTested.WebApi/blob/development/BACKERS.md). If you'd like to join them, please consider:

- [Become a backer or sponsor on Patreon](https://www.patreon.com/ivaylokenov)
- [Become a backer or sponsor on OpenCollective](https://opencollective.com/mytestedaspnet)
- [One-time donation via PayPal](http://paypal.me/ivaylokenov)
- [One-time donation via Buy Me A Coffee](http://buymeacoff.ee/ivaylokenov)
- One-time donation via cryptocurrencies:
  - BTC (Bitcoin) - 3P49XMiGXxqR2Dq1HdqHpkCa6UD848rpBU 
  - BCH (Bitcoin Cash) - qqgyjlvmuydf6gtfhfdypyw2u8utmc3uqg4nwma3y4
  - ETC (Ethereum) - 0x2bc55e4b1B9b296B751738631CD24b2f701E588F
  - LTC (Litecoin) - MQ1GJum1QuqAuUsc6LarE3Z6TQQJ3rJwsA

#### What's the difference between Patreon and OpenCollective?

Funds donated via both platforms are used for development and marketing purposes. Funds donated via OpenCollective are managed with transparent expenses. Your name/logo will receive proper recognition and exposure by donating on either platform.

## Documentation

Please see the [documentation](https://github.com/ivaylokenov/MyTested.WebApi/tree/master/documentation) for full list of available features. Everything listed in the documentation is 100% covered by [more than 800 unit tests](https://github.com/ivaylokenov/MyTested.WebApi/tree/master/src/MyTested.WebApi.Tests) and should work correctly. Almost all items in the [issues page](https://github.com/ivaylokenov/MyTested.WebApi/issues) are expected future features and enhancements.

## Installation

You can install this library using NuGet into your Test class project. It will automatically reference the needed dependencies of Microsoft.AspNet.WebApi.Core (≥ 5.1.0) and Microsoft.Owin.Testing (≥ 3.0.1) for you. .NET 4.5+ is needed. Make sure your solution has the same versions of the mentioned dependencies in all projects where you are using them. For example, if you are using Microsoft.AspNet.WebApi.Core 5.2.3 in your Web project, the same version should be used after installing MyTested.WebApi in your Tests project.

    Install-Package MyTested.WebApi

After the downloading is complete, just add `using MyTested.WebApi;` and you are ready to test in the most elegant and developer friendly way.
	
    using MyTested.WebApi;
	
For other interesting packages check out:

 - [MyTested.AspNetCore.Mvc](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc) - fluent testing framework for ASP.NET Core MVC
 - [MyTested.HttpServer](https://github.com/ivaylokenov/MyTested.HttpServer) - fluent testing framework for remote HTTP servers
 - [AspNet.Mvc.TypedRouting](https://github.com/ivaylokenov/AspNet.Mvc.TypedRouting) - typed routing and link generation for ASP.NET Core MVC
 - [ASP.NET MVC 5 Lambda Expression Helpers](https://github.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers) - typed expression based link generation for ASP.NET MVC 5
	
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
    });

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

Refer to the [LICENSE](https://github.com/ivaylokenov/MyTested.WebApi/blob/master/LICENSE) for detailed information.
 
## Any questions, comments or additions?

If you have a feature request or bug report, leave an issue on the [issues page](https://github.com/ivaylokenov/MyTested.WebApi/issues) or send a [pull request](https://github.com/ivaylokenov/MyTested.WebApi/pulls). For general questions and comments, use the [StackOverflow](http://stackoverflow.com/) forum.
