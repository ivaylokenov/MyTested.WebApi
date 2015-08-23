# MyWebApi - ASP.NET Web API Fluent Testing Framework
====================================

MyWebApi is unit testing framework providing easy fluent interface to test the ASP.NET Web API framework. Inspired by [TestStack.FluentMVCTesting](https://github.com/TestStack/TestStack.FluentMVCTesting) and [ChaiJS](https://github.com/chaijs/chai)

[![Build status](https://ci.appveyor.com/api/projects/status/738pm1kuuv7yw1t5?svg=true)](https://ci.appveyor.com/project/ivaylokenov/mywebapi)

## How to use

### Controller instantiation

You have a couple of options from which you can setup the controller you want to test. The framework gives you static `MyWebApi` class from which the test builder starts.

```c#
// instantiates controller with parameterless constructor
MyWebApi
	.Controller<WebApiController>();
	
// instantiates controller with constructor function to resolve dependencies
MyWebApi
	.Controller(() => new WebApiController(mockedInjectedService));
```

### Calling actions

You can call any action using lambda expression. All parameter values will be resolved and model state validation will be performed on them.

```c#
// calls action with no parameters
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction());
	
// calls action with parameters
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel));

// calls async action
MyWebApi
	.Controller<WebApiController>()
	.CallingAsync(c => c.SomeActionAsync());
```

## Any questions, comments or additions?

Leave an issue on the [issues page](https://github.com/ivaylokenov/MyWebApi/issues) or send a [pull request](https://github.com/ivaylokenov/MyWebApi/pulls).