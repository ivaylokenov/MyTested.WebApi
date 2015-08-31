# MyWebApi - ASP.NET Web API Fluent Testing Framework
===

MyWebApi is unit testing framework providing easy fluent interface to test the ASP.NET Web API framework. Inspired by [TestStack.FluentMVCTesting](https://github.com/TestStack/TestStack.FluentMVCTesting) and [ChaiJS](https://github.com/chaijs/chai).

[![Build status](https://ci.appveyor.com/api/projects/status/738pm1kuuv7yw1t5?svg=true)](https://ci.appveyor.com/project/ivaylokenov/mywebapi)

## How to use

### Controller instantiation

You have a couple of options from which you can setup the controller you want to test. The framework gives you static `MyWebApi` class from which the test builder starts:

```c#
// instantiates controller with parameterless constructor
MyWebApi
	.Controller<WebApiController>();
	
// instantiates controller with provided resolved dependencies one by one
// * dependencies do not need to be in any particular order
// * works even without the explicit generic interface specification
MyWebApi
	.Controller<WebApiController>()
	.WithResolvedDependencyFor<IInjectedService>(mockedInjectedService)
	.WithResolvedDependencyFor<IAnotherInjectedService>(anotherMockedInjectedService);
	
// instantiates controller with provided dependencies all at once
// * dependencies do not need to be in any particular order
MyWebApi
	.Controller<WebApiController>()
	.WithResolvedDependencies(mockedInjectedService, anotherMockedInjectedService);
	
// instantiates controller with provided collection of dependencies
// * dependencies do not need to be in any particular order
MyWebApi
	.Controller<WebApiController>()
	.WithResolvedDependencies(new List<object> { mockedInjectedService, anotherMockedInjectedService });
	
// instantiates controller with constructor function to resolve dependencies
MyWebApi
	.Controller(() => new WebApiController(mockedInjectedService));
	
// or provide already instantiated controller
MyWebApi
	.Controller(myWebApiControllerInstance);
```

### Authenticated user

```c#
// by default Controller.User will be unauthenticated 
// * User.Identity.Name will be null and User.Identity.IsAuthenticated will be false
MyWebApi
	.Controller<WebApiController>();
	
// sets default authenticated user - with username "TestUser" and no user roles
MyWebApi
	.Controller<WebApiController>()
	.WithAuthenticatedUser();
	
// sets custom authenticated user using delegate action
MyWebApi
	.Controller<WebApiController>()
	.WithAuthenticatedUser(user => user.WithUsername("NewUserName"));
		
// sets custom authenticated user in user roles using delegate action
MyWebApi
	.Controller<WebApiController>()
	.WithAuthenticatedUser(user => user
		.WithUsername("NewUserName")
		.InRoles("Moderator", "Administrator")); // or InRole("Moderator")
```

### Calling actions

You can call any action using lambda expression. All parameter values will be resolved and model state validation will be performed on them:

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

### Model state validation

You can test whether model state is valid/invalid or contains any specific error:

```c#
// tests whether model state is valid
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHaveValidModelState();
	
// tests whether model state is not valid
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHaveInvalidModelState();
	
// tests whether model state is valid and returns some action result
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHaveInvalidModelState()
	.AndAlso()
	.ShouldReturnOk();;
	
// tests whether model state error exists (or does not exist) for specific key 
// * not recommended because of magic string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHaveModelStateFor<RequestModel>()
	.ContainingModelStateError("propertyName")
	.AndAlso() // AndAlso method is not necessary but provides better readability (and it's cool)
	.ContainingNoModelStateError("anotherPropertyName");
	
// tests whether model state error exists by using lambda expression
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHaveModelStateFor<RequestModel>()
	.ContainingModelStateErrorFor(m => m.SomeProperty)
	.AndAlso()
	.ContainingNoModelStateErrorFor(m => m.AnotherProperty);
	
// tests the error message for specific property
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHaveModelStateFor<RequestModel>() // error must be equal to the provided string
	.ContainingModelStateErrorFor(m => m.SomeProperty).ThatEquals("Error message") 
	.AndAlso() // error must begin with the provided string
	.ContainingModelStateErrorFor(m => m.SecondProperty).BeginningWith("Error") 
	.AndAlso() // error must end with the provided string
	.ContainingModelStateErrorFor(m => m.ThirdProperty).EndingWith("message") 
	.AndAlso() // error must contain the provided string
	.ContainingModelStateErrorFor(m => m.SecondProperty).Containing("ror mes"); 
```

### Action results

You can test for specific return values or the default IHttpActionResult types:

#### Generic result

Useful where the action does not return IHttpActionResult

```c#
// tests whether the action returns certain type by providing generic parameter
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn<ResponseModel>();
	
// tests whether the action returns certain type by using typeof
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn(typeof(ResponseModel));
	
// tests whether the action returns generic model
// * works with IEnumerable<> (or IList<ResponseModel>) too by using polymorphism
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn(typeof(IList<>)); 
```

#### Ok result

```c#
// tests whether the action returns OkResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnOk();
	
// tests whether the action returns OkResult with no response model
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnOk()
	.WithNoResponseModel();
	
// tests whether the action returns OkResult with specific object
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnOk()
	.WithResponseModel(someResponseModelObject);
	
// tests whether the action returns OkResult with specific response model type
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnOk()
	.WithResponseModelOfType<ResponseModel>();

// tests whether the action returns OkResult 
// with specific response model passing certain assertions
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnOk()
	.WithResponseModelOfType<ResponseModel>()
	.Passing(m =>
	{
		Assert.AreEqual(1, m.Id);
		Assert.AreEqual("Some property value", m.SomeProperty);
	});
	
// tests whether the action returns OkResult 
// with specific response model passing a predicate
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnOk()
	.WithResponseModelOfType<ResponseModel>()
	.Passing(m => m.Id == 1);
	
// tests for model state errors for the response model 
// * not very useful in practice
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnOk()
	.WithResponseModelOfType<ResponseModel>()
	.ContainingModelStateErrorFor(m => m.SomeProperty).ThatEquals("Error message")
	.AndAlso()
	.ContainingNoModelStateErrorFor(m => m.AnotherProperty);
```

#### Unauthorized result

```c#
// tests whether the action returns UnauthorizedResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnUnauthorized();
	
// tests whether the action returns UnauthorizedResult
// and result contains authentication header value with certain scheme
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnUnauthorized()
	.ContainingAuthenticationHeaderChallenge(AuthenticationScheme.Basic);
	
// tests whether the action returns UnauthorizedResult
// and result contains authentication header value with certain scheme as string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnUnauthorized()
	.ContainingAuthenticationHeaderChallenge("Basic");
	
// tests whether the action returns UnauthorizedResult
// and result contains authentication header value with certain scheme and parameter
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnUnauthorized()
	.ContainingAuthenticationHeaderChallenge("Basic", "Value");
	
// tests whether the action returns UnauthorizedResult
// and result contains authentication header value
// equal to the provided AuthenticationHeaderValue
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnUnauthorized()	
	.ContainingAuthenticationHeaderChallenge(new AuthenticationHeaderValue("Basic", "Value"));
	
// tests whether the action returns UnauthorizedResult
// and result contains authentication header by using AuthenticationHeaderValue builder
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnUnauthorized()	
	.ContainingAuthenticationHeaderChallenge(
		authHeader =>
			authHeader
				.WithScheme(AuthenticationScheme.Basic)
				.WithParameter("TestParameter"));
				
// tests whether the action returns UnauthorizedResult
// and result has exactly the provided authentication header values as collection
// * order of header values does not matter
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnUnauthorized()	
	.WithAuthenticationHeaderChallenges(new[]
	{
		new AuthenticationHeaderValue("Basic", "Value"),
		new AuthenticationHeaderValue("Basic", "AnotherValue")
	});

// tests whether the action returns UnauthorizedResult
// and result has exactly the provided authentication header values as params
// * order of header values does not matter
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnUnauthorized()	
	.WithAuthenticationHeaderChallenges(
		new AuthenticationHeaderValue("Basic", "Value"),
		new AuthenticationHeaderValue("Basic", "AnotherValue"));

// tests whether the action returns UnauthorizedResult
// and result has exactly the provided authentication header values using a builder
// * order of header values does not matter
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnUnauthorized()
	.WithAuthenticationHeaderChallenges(
		authHeaders =>
			authHeaders
				.ContainingHeader(header => header.WithScheme("Basic").WithParameter("Value"))
				.AndAlso() // AndAlso() is not necessary
				.ContainingHeader(header => header.WithScheme(AuthenticationScheme.Basic)));
```

#### BadRequest result

```c#
// tests whether the action returns BadRequestResult,
// InvalidModelStateResult or BadRequestErrorMessageResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnBadRequest();
	
// tests whether the action returns bad request with specific error
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnBadRequest()
	.WithErrorMessage("Undefined is not a function");	

// tests whether the action returns bad request with specific error
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnBadRequest()
	.WithErrorMessage()
	.ThatEquals("Undefined is not a function");	

// tests whether the action returns bad request with error
// beginning with provided string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnBadRequest()
	.WithErrorMessage()
	.BeginningWith("Undefined");	

// tests whether the action returns bad request with error
// ending with provided string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnBadRequest()
	.WithErrorMessage()
	.EndingWith("function");	

// tests whether the action returns bad request with error
// containing the provided string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnBadRequest()
	.WithErrorMessage()
	.Containing("is not");	

// tests whether the action returns bad request
// with model state deeply equal to the provided one
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnBadRequest()
	.WithModelState(modelState);

// tests whether the action returns bad request
// with model errors built by test builder
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnBadRequest()
	.WithModelStateFor<RequestModel>()
		.ContainingModelStateErrorFor(m => m.Integer).ThatEquals("The field Integer must be stopped!")
		.AndAlso()
		.ContainingModelStateErrorFor(m => m.RequiredString).BeginningWith("The RequiredString")
		.AndAlso()
		.ContainingModelStateErrorFor(m => m.RequiredString).EndingWith("required.")
		.AndAlso()
		.ContainingModelStateErrorFor(m => m.RequiredString).Containing("field")
		.AndAlso()
		.ContainingNoModelStateErrorFor(m => m.NonRequiredString);
```

#### StatusCode result

```c#
// tests whether the action returns StatusCodeResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnStatusCode();
	
// tests whether the action returns StatusCodeResult
// with status code equal to the provided one
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnStatusCode(HttpStatusCode.Created);
```

#### NotFound result
```c#
// tests whether the action returns NotFoundResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnNotFound();
```

#### Conflict result
```c#
// tests whether the action returns ConflictResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnConflict();
```

### AndProvide... methods

You can get controller, action, action result and response model information where applicable by using AndProvide... methods.
Useful for integration tests where current action result model is needed for the next action assertion.

```c#
// get controller instance
// * method is available almost everywhere throughout the API
var controller = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnOk()
	.AndProvideTheController();
	
// get action name
// * method is available almost everywhere throughout the API
var actionName = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnOk()
	.AndProvideTheActionName();
	
// get the action result
// * method is available on most methods which assert the action result
var actionResult = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnStatusCode()
	.AndProvideTheActionResult();
	
// get the response model
// * method is available wherever there is response model assertion
var responseModel = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnOk()
	.WithResponseModelOfType<ResponseModel>()
	.AndProvideTheModel();
```

## Any questions, comments or additions?

Leave an issue on the [issues page](https://github.com/ivaylokenov/MyWebApi/issues) or send a [pull request](https://github.com/ivaylokenov/MyWebApi/pulls).