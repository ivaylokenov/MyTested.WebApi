<h1><img src="https://raw.githubusercontent.com/ivaylokenov/MyWebApi/master/documentation/logo.png" align="left" alt="MyWebApi" width="100">&nbsp;&nbsp;&nbsp; MyWebApi - ASP.NET Web API <br />&nbsp;&nbsp;&nbsp; Fluent Testing Framework</h1>
====================================

## Full list of available features

### Table of contents

 - Action call configuration
  - [Controller instantiation](#controller-instantiation)
  - [Http request message] (#http-request-message)
  - [Authenticated user](#authenticated-user)
  - [Calling actions](#calling-actions)
 - Action call validations
  - [Model state validation](#model-state-validation)
  - [Catching thrown exceptions](#catching-thrown-exceptions)
 - Action result validations
  - [Generic result](#generic-result)
  - [Any result](#any-result)
  - [Ok result](#ok-result)
  - [Unauthorized result](#unauthorized-result)
  - [BadRequest result](#badrequest-result)
  - [JSON result](#json-result)
  - [StatusCode result](#statuscode-result)
  - [Redirect result](#redirect-result)
  - [Content result](#content-result)
  - [Created result](#created-result)
  - [NotFound result](#notfound-result)
  - [Conflict result](#conflict-result)
  - [EmptyContent (void) result](#emptycontent-void-result)
  - [Null or Default result](#null-or-default-result)
 - Additional methods
  - [AndProvide... methods](#andprovide-methods)

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
	.AndAlso() // AndAlso method is not necessary but provides better readability (and it's cool)
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

[To top](#table-of-contents)

### Http request message

You can mock the HttpRequestMessage class to suit your testing needs:

```c#
// adding HttpContent to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithContent(someContent));
		
// adding StreamContent to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithStreamContent(someStreamContent));
		
// adding ByteArrayContent to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithByteArrayContent(someByteArrayContent));
		
// adding form URL encoded content to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithFormUrlEncodedContent(someKeyValuePairCollection));
		
// adding StringContent to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithStringContent(someStringContent));
		
// adding custom header to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithHeader("TestHeader", "TestHeaderValue"));
		
// adding custom header with multiple values to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithHeader("TestHeader", new[] { "TestHeaderValue", "AnotherTestHeaderValue" }));
		
// adding custom headers provided as dictionary to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithHeader(someDictionaryWithHeaders));
		
// adding HTTP method as string to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithMethod("GET"));
		
// adding strongly-typed HTTP method to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithMethod(HttpMethod.Get));
		
// adding request URI as string to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithRequestUri("RequestUri"));
		
// adding request URI as Uri class to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithRequestUri(someUri));
		
// adding request URI with Uri builder to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithRequestUri(uri => uri
			.WithHost("somehost.com")
			.AndAlso() // AndAlso is not necessary
			.WithAbsolutePath("/someuri/1")
			.AndAlso()
			.WithPort(80)
			.AndAlso()
			.WithScheme("http")
			.AndAlso()
			.WithFragment(string.Empty)
			.AndAlso()
			.WithQuery("?query=Test")));
			
// adding request version as string to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithVersion("1.1"));
		
// adding request version using version numbers to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithVersion(1, 1));
		
// adding request version using Version class to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithVersion(someVersion));
		
// adding different options to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithMethod(HttpMethod.Get)
		.AndAlso() // AndAlso is not necessary
		.WithRequestUri(someUri)
		.AndAlso()
		.WithVersion(someVersion)
		.AndAlso()
		.WithStringContent(someStringContent));
```

[To top](#table-of-contents)

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

[To top](#table-of-contents)

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

[To top](#table-of-contents)

### Model state validation

You can test whether model state is valid/invalid or contains any specific error:

```c#
// tests whether model state is valid
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHave()
	.ValidModelState();
	
// tests whether model state is not valid
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHave()
	.InvalidModelState();
	
// tests whether model state is not valid
// with specific number of errors
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHave()
	.InvalidModelState(withNumberOfErrors: 5);
	
// tests whether model state is valid and returns some action result
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHave()
	.InvalidModelState()
	.AndAlso()
	.ShouldReturn()
	.Ok();
	
// tests whether model state error exists (or does not exist) for specific key 
// * not recommended because of magic string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHave()
	.ModelStateFor<RequestModel>()
	.ContainingModelStateError("propertyName")
	.AndAlso() // AndAlso method is not necessary
	.ContainingNoModelStateError("anotherPropertyName");
	
// tests whether model state error exists by using lambda expression
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHave()
	.ModelStateFor<RequestModel>()
	.ContainingModelStateErrorFor(m => m.SomeProperty)
	.AndAlso()
	.ContainingNoModelStateErrorFor(m => m.AnotherProperty);
	
// tests the error message for specific property
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction(requestModel))
	.ShouldHave()
	.ModelStateFor<RequestModel>() // error must be equal to the provided string
	.ContainingModelStateErrorFor(m => m.SomeProperty).ThatEquals("Error message") 
	.AndAlso() // error must begin with the provided string
	.ContainingModelStateErrorFor(m => m.SecondProperty).BeginningWith("Error") 
	.AndAlso() // error must end with the provided string
	.ContainingModelStateErrorFor(m => m.ThirdProperty).EndingWith("message") 
	.AndAlso() // error must contain the provided string
	.ContainingModelStateErrorFor(m => m.SecondProperty).Containing("ror mes"); 
	
// disable validation if needed
MyWebApi
	.Controller<WebApiController>()
	.WithoutValidation()
	.Calling(c => c.SomeAction());
```

[To top](#table-of-contents)

### Catching thrown exceptions

You can test whether action throws exception:

```c#
// tests whether the action throws any exception
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldThrow()
	.Exception();
	
// tests whether the action throws exception of specific type
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldThrow()
	.Exception()
	.OfType<NullReferenceException>();
	
// tests whether the action throws exception with specific error message
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldThrow()
	.Exception()
	.WithMessage("Some exception message");
	
// tests whether the action throws exception
// of specific type and specific error message
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldThrow()
	.Exception()
	.OfType<NullReferenceException>()
	.AndAlso() // AndAlso() is not necessary
	.WithMessage("Some exception message");
	
// tests whether the action throws HttpResponseException
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldThrow()
	.HttpResponseException();
	
// tests whether the action throws HttpResponseException
// with specific HttpStatusCode
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldThrow()
	.HttpResponseException()
	.WithStatusCode(HttpStatusCode.NotFound);
```

[To top](#table-of-contents)

### Action results

You can test for specific return values or the default IHttpActionResult types:

#### Generic result

Useful where the action does not return IHttpActionResult

```c#
// tests whether the action returns specific type by providing generic parameter
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.ResultOfType<ResponseModel>();
	
// tests whether the action returns specific type by using typeof
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.ResultOfType(typeof(ResponseModel));
	
// tests whether the action returns generic model
// * works with IEnumerable<> (or IList<ResponseModel>) too by using polymorphism
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.ResultOfType(typeof(IList<>)); 
	
// tests whether the action returns model
// passing specific assertions
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.ResultOfType<ResponseModel>();
	.Passing(m =>
	{
		Assert.AreEqual(1, m.Id);
		Assert.AreEqual("Some property value", m.SomeProperty);
	});
	
// tests whether the action returns model
// passing a predicate
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.WithResponseModelOfType<ResponseModel>()
	.Passing(m => m.Id == 1);
```

#### Any result
```c#
// tests whether the action returns any result
// and does not throw an exception
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn();
```

[To top](#table-of-contents)

#### Ok result

```c#
// tests whether the action returns OkResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok();
	
// tests whether the action returns OkResult with no response model
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.WithNoResponseModel();
	
// tests whether the action returns OkNegotiatedContentResult<T>
// with DefaultContentNegotiator
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.WithDefaultContentNegotiator();
	
// tests whether the action returns OkNegotiatedContentResult<T>
// with custom IContentNegotiator provided by instance
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.WithContentNegotiator(customContentNegotiator);

// tests whether the action returns OkNegotiatedContentResult<T>
// with custom IContentNegotiator provided by generic definition
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.WithContentNegotiatorOfType<CustomContentNegotiator>();

// tests whether the action returns OkNegotiatedContentResult<T>
// with exactly the default media type formatters
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.ContainingDefaultFormatters();

// tests whether the action returns OkNegotiatedContentResult<T>
// containing media type formatter provided by instance
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.ContainingMediaTypeFormatter(someMediaTypeFormatter);
	
// tests whether the action returns OkNegotiatedContentResult<T>
// containing media type formatter provided by generic definition
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
	
// tests whether the action returns OkNegotiatedContentResult<T>
// with exactly the provided media type formatters
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.ContainingMediaTypeFormatters(someCollectionOfMediaTypeFormatters);

// tests whether the action returns OkNegotiatedContentResult<T>
// with exactly the provided media type formatters
// by using params
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.ContainingMediaTypeFormatters(
		someMediaTypeFormatter,
		anotherMediaTypeFormatter,
		yetAnotherMediaTypeFormatter);

// tests whether the action returns OkNegotiatedContentResult<T>
// with exactly the provided media type formatters
// by using formatters builder
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.ContainingMediaTypeFormatters(
		formatters => 
			formatters
				.ContainingMediaTypeFormatter(someMediaTypeFormatter)
				.AndAlso()
				.ContainingMediaTypeFormatterOfType<SomeMediaTypeFormatter>());
	
// tests whether the action returns OkResult with specific object
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.WithResponseModel(someResponseModelObject);
	
// tests whether the action returns OkResult with specific response model type
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.WithResponseModelOfType<ResponseModel>();

// tests whether the action returns OkResult 
// with specific response model passing specific assertions
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
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
	.ShouldReturn()
	.Ok()
	.WithResponseModelOfType<ResponseModel>()
	.Passing(m => m.Id == 1);
	
// tests whether the action returns OkNegotiatedContentResult<T>
// with different types of properties by using AndAlso()
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.WithDefaultContentNegotiator()
	.AndAlso() // AndAlso is not necessary
	.WithResponseModelOfType<ResponseModel>();
	
// tests for model state errors for the response model 
// * not very useful in practice
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.WithResponseModelOfType<ResponseModel>()
	.ContainingModelStateErrorFor(m => m.SomeProperty).ThatEquals("Error message")
	.AndAlso()
	.ContainingNoModelStateErrorFor(m => m.AnotherProperty);
```

[To top](#table-of-contents)

#### Unauthorized result

```c#
// tests whether the action returns UnauthorizedResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Unauthorized();
	
// tests whether the action returns UnauthorizedResult
// and result contains authentication header value with specific scheme
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Unauthorized()
	.ContainingAuthenticationHeaderChallenge(AuthenticationScheme.Basic);
	
// tests whether the action returns UnauthorizedResult
// and result contains authentication header value with specific scheme as string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Unauthorized()
	.ContainingAuthenticationHeaderChallenge("Basic");
	
// tests whether the action returns UnauthorizedResult
// and result contains authentication header value with specific scheme and parameter
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Unauthorized()
	.ContainingAuthenticationHeaderChallenge("Basic", "Value");
	
// tests whether the action returns UnauthorizedResult
// and result contains authentication header value
// equal to the provided AuthenticationHeaderValue
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Unauthorized()	
	.ContainingAuthenticationHeaderChallenge(new AuthenticationHeaderValue("Basic", "Value"));
	
// tests whether the action returns UnauthorizedResult
// and result contains authentication header by using AuthenticationHeaderValue builder
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Unauthorized()	
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
	.ShouldReturn()
	.Unauthorized()	
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
	.ShouldReturn()
	.Unauthorized()	
	.WithAuthenticationHeaderChallenges(
		new AuthenticationHeaderValue("Basic", "Value"),
		new AuthenticationHeaderValue("Basic", "AnotherValue"));

// tests whether the action returns UnauthorizedResult
// and result has exactly the provided authentication header values using a builder
// * order of header values does not matter
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Unauthorized()
	.WithAuthenticationHeaderChallenges(
		authHeaders =>
			authHeaders
				.ContainingHeader(header => header.WithScheme("Basic").WithParameter("Value"))
				.AndAlso() // AndAlso() is not necessary
				.ContainingHeader(header => header.WithScheme(AuthenticationScheme.Basic)));
```

[To top](#table-of-contents)

#### BadRequest result

```c#
// tests whether the action returns BadRequestResult,
// InvalidModelStateResult or BadRequestErrorMessageResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.BadRequest();
	
// tests whether the action returns bad request with specific error
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.BadRequest()
	.WithErrorMessage("Undefined is not a function");	

// tests whether the action returns bad request with specific error
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.BadRequest()
	.WithErrorMessage()
	.ThatEquals("Undefined is not a function");	

// tests whether the action returns bad request with error
// beginning with provided string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.BadRequest()
	.WithErrorMessage()
	.BeginningWith("Undefined");	

// tests whether the action returns bad request with error
// ending with provided string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.BadRequest()
	.WithErrorMessage()
	.EndingWith("function");	

// tests whether the action returns bad request with error
// containing the provided string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.BadRequest()
	.WithErrorMessage()
	.Containing("is not");	

// tests whether the action returns bad request
// with model state deeply equal to the provided one
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.BadRequest()
	.WithModelState(modelState);

// tests whether the action returns bad request
// with model errors built by test builder
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.BadRequest()
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

[To top](#table-of-contents)

#### JSON result
```c#
// tests whether the action returns JSON
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Json();

// tests whether the action returns JSON
// with default encoding and settings
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Json()
	.WithDefaultEncoding()
	.AndAlso() // AndAlso is not necessary
	.WithDefaulJsonSerializerSettings();
	
// tests whether the action returns JSON
// with expected encoding
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Json()
	.WithEncoding(Encoding.ASCII);
	
// tests whether the action returns JSON
// with expected serializer settings
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Json()
	.WithJsonSerializerSettings(new MyCustomSerializerSettings());
	
// tests whether the action returns JSON
// with expected serializer settings constructed by builder
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.JsonWithSettingsAction())
	.ShouldReturn()
	.Json()
	.WithJsonSerializerSettings(s => s
		.WithFormatting(Formatting.Indented)
		.AndAlso() // AndAlso is not necessary
		.WithMaxDepth(2)
		.AndAlso()
		.WithConstructorHandling(ConstructorHandling.Default));
```

[To top](#table-of-contents)

#### StatusCode result

```c#
// tests whether the action returns StatusCodeResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.StatusCode();
	
// tests whether the action returns StatusCodeResult
// with status code equal to the provided one
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.StatusCode(HttpStatusCode.Created);
```

[To top](#table-of-contents)

#### Redirect result

```c#
// tests whether the action returns
// RedirectResult or RedirectToRouteResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Redirect();

// tests whether the action returns redirect result
// with location provided as string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Redirect()
	.AtLocation("http://somehost.com/someuri/1?query=someQuery");

// tests whether the action returns redirect result
// with location provided as URI
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Redirect()
	.AtLocation(someUri);

// tests whether the action returns redirect result
// with location provided as URI builder
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Redirect()
	.AtLocation(
		location =>
			location
				.WithHost("somehost.com")
				.AndAlso() // AndAlso is not necessary
				.WithAbsolutePath("/someuri/1")
				.AndAlso()
				.WithPort(80)
				.AndAlso()
				.WithScheme("http")
				.AndAlso()
				.WithFragment(string.Empty)
				.AndAlso()
				.WithQuery("?query=Test"));
```

[To top](#table-of-contents)

#### Content result

```c#
// tests whether the action returns
// NegotiatedContentResult<T>
// or FormattedContentResult<T>
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content();
	
// tests whether the action returns
// content with specific response model
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content()
	.WithResponseModelOfType<ResponseModel>();
	
// tests whether the action returns
// content with specific status code result
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content()
	.WithStatusCode(HttpStatusCode.OK);

// tests whether the action returns
// content with specific media type
// provided by string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content()
	.WithMediaType("application/json");

// tests whether the action returns
// content with specific media type
// provided by predefined in the library constants
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content()
	.WithMediaType(MediaType.ApplicationJson);
	
// tests whether the action returns
// content with specific media type
// provided by MediaTypeHeaderValue class
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content()
	.WithMediaType(new MediaTypeHeaderValue("application/json"));
	
// tests whether the action returns
// content with DefaultContentNegotiator
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content()
	.WithDefaultContentNegotiator();
	
// tests whether the action returns
// content with custom content negotiator
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content()
	.WithContentNegotiator(customContentNegotiator);
	
// tests whether the action returns
// content with custom content negotiator
// provided by generic definition
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content()
	.WithContentNegotiatorOfType<CustomContentNegotiator>();
	
// tests whether the action returns
// content containing media type formatter
// provided by an instance
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content()
	.ContainingMediaTypeFormatter(customMediaTypeFormatter);
	
// tests whether the action returns
// content containing media type formatter
// provided by an generic definition
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content()
	.ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
	
// tests whether the action returns
// content containing the default media type formatter
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Content()
	.ContainingDefaultFormatters();
	
// tests whether the action returns
// content containing the media type formatters
// provided in a collection
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.ContentActionWithCustomFormatters())
	.ShouldReturn()
	.Content()
	.ContainingMediaTypeFormatters(collectionOfMediaTypeFormatters);
	
// tests whether the action returns
// content containing the media type formatters
// provided by a builder
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.ContentAction())
	.ShouldReturn()
	.Content()
	.ContainingMediaTypeFormatters(
		formatters => formatters
			.ContainingMediaTypeFormatter(new JsonMediaTypeFormatter)
			.AndAlso()
			.ContainingMediaTypeFormatterOfType<FormUrlEncodedMediaTypeFormatter>());
			
// tests whether the action returns
// content with status code and response model
// combined by AndAlso
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.ContentAction())
	.ShouldReturn()
	.Content()
	.WithStatusCode(HttpStatusCode.OK)
	.AndAlso() // AndAlso is not necessary
	.WithResponseModelOfType<ResponseModel>();
```

[To top](#table-of-contents)

#### Created result

```c#
// tests whether the action returns
// CreatedNegotiatedContentResult<T>
// or CreatedAtRouteNegotiatedContentResult<T>
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created();
	
// tests whether the action returns
// CreatedNegotiatedContentResult<T>
// or CreatedAtRouteNegotiatedContentResult<T>
// with specific response model type
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.WithResponseModelOfType<ResponseModel>();
	
// tests whether the action returns created result
// with DefaultContentNegotiator
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.WithDefaultContentNegotiator();
	
// tests whether the action returns created result
// with custom IContentNegotiator provided by instance
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.WithContentNegotiator(customContentNegotiator);

// tests whether the action returns created result
// with custom IContentNegotiator provided by generic definition
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.WithContentNegotiatorOfType<CustomContentNegotiator>();

// tests whether the action returns created result
// with location provided as string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.AtLocation("http://somehost.com/someuri/1?query=someQuery");

// tests whether the action returns created result
// with location provided as URI
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.AtLocation(someUri);

// tests whether the action returns created result
// with location provided as URI builder
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.AtLocation(
		location =>
			location
				.WithHost("somehost.com")
				.AndAlso() // AndAlso is not necessary
				.WithAbsolutePath("/someuri/1")
				.AndAlso()
				.WithPort(80)
				.AndAlso()
				.WithScheme("http")
				.AndAlso()
				.WithFragment(string.Empty)
				.AndAlso()
				.WithQuery("?query=Test"));

// tests whether the action returns created result
// with exactly the default media type formatters
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.ContainingDefaultFormatters();

// tests whether the action returns created result
// containing media type formatter provided by instance
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.ContainingMediaTypeFormatter(someMediaTypeFormatter);
	
// tests whether the action returns created result
// containing media type formatter provided by generic definition
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
	
// tests whether the action returns created result
// with exactly the provided media type formatters
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.ContainingMediaTypeFormatters(someCollectionOfMediaTypeFormatters);

// tests whether the action returns created result
// with exactly the provided media type formatters
// by using params
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.ContainingMediaTypeFormatters(
		someMediaTypeFormatter,
		anotherMediaTypeFormatter,
		yetAnotherMediaTypeFormatter);

// tests whether the action returns created result
// with exactly the provided media type formatters
// by using formatters builder
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.ContainingMediaTypeFormatters(
		formatters => 
			formatters
				.ContainingMediaTypeFormatter(someMediaTypeFormatter)
				.AndAlso()
				.ContainingMediaTypeFormatterOfType<SomeMediaTypeFormatter>());

// tests whether the action returns created result
// with different types of properties by using AndAlso()
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.WithDefaultContentNegotiator()
	.AndAlso() // AndAlso is not necessary
	.AtLocation(someUri)
	.AndAlso()
	.ContainingMediaTypeFormatterOfType<SomeMediaTypeFormatter>();
```

[To top](#table-of-contents)

#### NotFound result
```c#
// tests whether the action returns NotFoundResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.NotFound();
```

[To top](#table-of-contents)

#### Conflict result
```c#
// tests whether the action returns ConflictResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Conflict();
```

[To top](#table-of-contents)

#### InternalServerError result
```c#
// tests whether the action returns 
// InternalServerErrorResult or ExceptionResult
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.InternalServerError();
	
// tests whether the action returns internal server error with exception
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.InternalServerError()
	.WithException();
	
// tests whether the action returns internal server error
// equal to the provided exception's type and message
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.InternalServerError()
	.WithException(new SomeException("Some exception message"));
	
// tests whether the action returns internal server error
// with exception of a specific type
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.InternalServerError()
	.WithException()
	.OfType<SomeException>();
	
// tests whether the action returns internal server error
// with exception with specific message
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.InternalServerError()
	.WithException()
	.WithMessage("Some exception message");
	
// tests whether the action returns internal server error
// with exception of specific type and with specific message
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.InternalServerError()
	.WithException()
	.OfType<SomeException>()
	.AndAlso() // AndAlso() is not necessary
	.WithMessage("Some exception message");
	
// tests whether the action returns internal server error
// with specific exception message tests
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.InternalServerError()
	.WithException()
	.OfType<SomeException>()
	.AndAlso()
	.WithMessage().ThatEquals("Some exception message")
	.AndAlso()
	.WithMessage().BeginningWith("Test ")
	.AndAlso()
	.WithMessage().EndingWith("message")
	.AndAlso()
	.WithMessage().Containing("n m");
```

[To top](#table-of-contents)

#### EmptyContent (void) result
```c#
// tests whether the action does not return anything (204 No Content)
// and does not throw exception
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturnEmpty();
```

[To top](#table-of-contents)

#### Null or Default result
```c#
// tests whether the action returns the default value of the return type
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.DefaultValue();
	
// tests whether the action returns null
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Null();
```

[To top](#table-of-contents)

### AndProvide... methods

You can get controller, action, action result and response model information where applicable by using AndProvide... methods.
Useful for integration tests where current action result model is needed for the next action assertion.

```c#
// get controller instance
// * method is available almost everywhere throughout the API
var controller = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.AndProvideTheController();
	
// get action name
// * method is available almost everywhere throughout the API
var actionName = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.AndProvideTheActionName();
	
// get the action result
// * method is available on most methods which assert the action result
var actionResult = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.StatusCode()
	.AndProvideTheActionResult();
	
// get the response model
// * method is available wherever there is response model assertion
var responseModel = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.WithResponseModelOfType<ResponseModel>()
	.AndProvideTheModel();
	
// get the caught exception 
// * returns null if the action does not throw exception
// * method is available almost everywhere throughout the API
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldThrow()
	.Exception()
	.AndProvideTheCaughtException();
```

[To top](#table-of-contents)