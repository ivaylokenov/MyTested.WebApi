<h1><img src="https://raw.githubusercontent.com/ivaylokenov/MyWebApi/master/documentation/logo.png" align="left" alt="MyWebApi" width="100">&nbsp;&nbsp;&nbsp; MyWebApi - ASP.NET Web API <br />&nbsp;&nbsp;&nbsp; Fluent Testing Framework</h1>
====================================

## Full list of available features

### Table of contents

 - Global configuration
  - [Using custom HttpConfiguration](#using-custom-httpconfiguration)
 - Route validations
  - [Building route request](#building-route-request)
  - [Testing routes](#testing-routes)
  - [Testing resolved route values](#testing-resolved-route-values)
 - HTTP message handler validations
  - [Handler configuration](#handler-configuration)
  - [Handler response validation](#handler-response-validation)
 - Controller test case configuration
  - [Controller instantiation](#controller-instantiation)
  - [HTTP request message] (#http-request-message)
  - [Authenticated user](#authenticated-user)
  - [Calling actions](#calling-actions)
 - Controller validations
  - [Controller attributes testing](#controller-attributes-testing)
 - Action call validations
  - [Action attributes testing](#action-attributes-testing)
  - [Model state validation](#model-state-validation)
  - [Catching thrown exceptions](#catching-thrown-exceptions)
 - Action result validations
  - [Any result](#any-result)
  - [Generic result](#generic-result)
  - [HTTP response message result](#http-response-message-result)
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
 - Integration testing of the full server pipeline
  - [HTTP server](#http-server)
  - [OWIN pipeline](#owin-pipeline)
 - Additional methods
  - [AndProvide... methods](#andprovide-methods)

### Using custom HttpConfiguration

You have the option to configure global HttpConfiguration to be used across all test cases. These call are not necessary but can be helpful for route tests for example where all registered routes are the same throughout the whole application:

```c#
// register configuration by providing instance of HttpConfiguration
MyWebApi.IsUsing(httpConfiguration);

// or by providing registration action
MyWebApi.IsRegisteredWith(WebApiConfig.Register);

// or by setting the default HttpConfiguration
// * this call is not necessary for tests to run
// * it is useful if you want to reset the global
// * configuration used in other tests
MyWebApi.IsUsingDefaultHttpConfiguration();
```

[To top](#table-of-contents)
  
### Building route request

You can test routes using the internal ASP.NET Web API
route selecting algorithm but first you need to configure the request:

```c#
// starts route testing by
// using global HTTP configuration
MyWebApi
	.Routes();
	
// configures the HTTP configuration 
// for a specific test case by passing it to the method
MyWebApi
	.Routes(httpConfiguration);

// sets the HTTP request message to test
// by providing instance of HttpRequestMessage
MyWebApi
	.Routes()
	.ShouldMap(httpRequestMessage);
	
// sets the HTTP request message to test
// by using request builder
// * see controller request builder for all the available options
MyWebApi
	.Routes()
	.ShouldMap(request => request
		.WithMethod(HttpMethod.Get)
		.AndAlso() // AndAlso is not necessary
		.WithRequestUri("api/Route/To/Test"));
	
// sets the URI location to test as string
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test");
	
// sets the URI location to test as Uri class
MyWebApi
	.Routes()
	.ShouldMap(uriLocation);

// sets the URI location to test as string
// and the HTTP method (default is GET) to the request
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithHttpMethod("POST");
	
// sets the URI location to test as string
// and the HTTP method as HttpMethod class to the request
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithHttpMethod(HttpMethod.Post);
	
// sets the URI location to test as string
// and adds custom HTTP request header to the request
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithRequestHeader("SomeHeader", "SomeHeaderValue");
	
// sets the URI location to test as string
// and adds custom HTTP request header with multiple values to the request
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithRequestHeader("SomeHeader", new[] { "SomeHeaderValue", "AnotherHeaderValue" });
	
// sets the URI location to test as string
// and adds custom HTTP headers as dictionary to the request
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithRequestHeaders(someDictionaryWithHeaders);
	
// sets the URI location to test as string
// and adds custom HTTP content header to the request
// * adding content headers requires content to be initialized and set
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithContentHeader("SomeContentHeader", "SomeContentHeaderValue");
	
// sets the URI location to test as string
// and adds custom HTTP content header with multiple values to the request
// * adding content headers requires content to be initialized and set
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithContentHeader("SomeContentHeader", new[] { "SomeContentHeaderValue", "AnotherContentHeaderValue" });
	
// sets the URI location to test as string
// and adds custom HTTP content headers as dictionary to the request
// * adding content headers requires content to be initialized and set
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithContentHeaders(someDictionaryWithContentHeaders);
	
// sets the URI location to test as string
// and adds form URL encoded content to the request
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithFormUrlEncodedContent("First=FirstValue&Second=SecondValue");
	
// sets the URI location to test as string
// and adds JSON content to the request
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithJsonContent(someJsonContent);
	
// sets the URI location to test as string
// and adds custom content to the request
// with the provided media type
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithContent(someContent, MediaType.ApplicationXml);
	
// sets the URI location to test as string
// and adds custom content to the request
// with the provided media type as MediaTypeHeaderValue
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithContent(someContent, new MediaTypeHeaderValue("text/plain"));
	
// combining them with And() method
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithHttpMethod(HttpMethod.Post)
	.WithJsonContent(someJsonContent);
```

[To top](#table-of-contents)
  
### Testing routes

```c#
// tests whether the route does not exist
MyWebApi
	.Routes()
	.ShouldMap("api/NonExisting/Route")
	.ToNonExistingRoute();
	
// tests whether the route is ignored with StopRoutingHandler
MyWebApi
	.Routes()
	.ShouldMap("api/Ignored/Route")
	.ToIgnoredRoute();

// tests whether the route is not resolved
// because of not allowed method
MyWebApi
	.Routes()
	.ShouldMap("api/Route/OnlyGetMethod")
	.WithHttpMethod(HttpMethod.Post)
	.ToNotAllowedMethod();
	
// tests whether the route is resolved
// by custom handler
MyWebApi
	.Routes()
	.ShouldMap("api/Custom/Handler/Route")
	.ToHandlerOfType<CustomHttpHandler>();
	
// tests whether the route is not resolved
// by custom handler
MyWebApi
	.Routes()
	.ShouldMap("api/No/Custom/Handler/Route")
	.ToNoHandlerOfType<CustomHttpHandler>();
	
// tests whether the route is not resolved
// by any custom handler
MyWebApi
	.Routes()
	.ShouldMap("api/No/Handler/Route")
	.ToNoHandler();

// tests whether the route is resolved
// with valid model state
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithHttpMethod(HttpMethod.Post)
	.WithJsonContent(someJsonContent)
	.ToValidModelState();
	
// tests whether the route is resolved
// with invalid model state
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithHttpMethod(HttpMethod.Post)
	.WithJsonContent(someJsonContent)
	.InvalidModelState();
	
// tests whether the route is resolved
// with invalid model state and specific number of errors
MyWebApi
	.Routes()
	.ShouldMap("api/Route/To/Test")
	.WithHttpMethod(HttpMethod.Post)
	.WithJsonContent(someJsonContent)
	.InvalidModelState(withNumberOfErrors: 5);
```

[To top](#table-of-contents)

### Testing resolved route values

```c#
// tests whether the controller and action are correctly resolved
// * ActionName, RoutePrefix and Route attributes are taken into account
MyWebApi
	.Routes()
	.ShouldMap("api/WebApiController/SomeAction")
	.To<WebApiController>(c => c.SomeAction());
	
// tests whether the controller and action are correctly resolved
// with all the route parameters
MyWebApi
	.Routes()
	.ShouldMap("api/WebApiController/SomeAction/5")
	.To<WebApiController>(c => c.SomeAction(5));
	
// tests whether the controller and action are correctly resolved
// with all the route values and a query string
MyWebApi
	.Routes()
	.ShouldMap("api/WebApiController/SomeAction/5?Value=Test")
	.To<WebApiController>(c => c.SomeAction(5, "Test"));
	
// tests whether the controller and action are correctly resolved
// with model deeply equal to the provided one
// * model equality resolves successfully value and reference types,
// * overridden Equals method, custom == operator, IComparable, nested objects and collection properties
MyWebApi
	.Routes()
	.ShouldMap("api/WebApiController/SomeAction")
	.WithHttpMethod(HttpMethod.Post)
	.WithJsonContent(@"{""SomeInt"": 1, ""SomeString"": ""Test""}")
	.To<WebApiController>(c => c.SomeAction(new RequestModel
	{
		SomeInt = 1,
		SomeString = "Test"
	}));
	
// tests whether the controller and action are correctly resolved
// with parameter and model deeply equal to the provided one
MyWebApi
	.Routes()
	.ShouldMap("api/WebApiController/SomeAction/5")
	.WithHttpMethod(HttpMethod.Post)
	.And() // And is not necessary
	.WithJsonContent(@"{""SomeInt"": 1, ""SomeString"": ""Test""}")
	.To<WebApiController>(c => c.SomeAction(5, new RequestModel
	{
		SomeInt = 1,
		SomeString = "Test"
	}));
	
// combining tests with AndAlso()
MyWebApi
	.Routes()
	.ShouldMap("api/WebApiController/SomeAction")
	.To<WebApiController>(c => c.SomeAction());
	.AndAlso() // AndAlso is not necessary
	.ToNoHandler();
```

[To top](#table-of-contents)
  
### Handler configuration

You can test whether HTTP message handler returns correct response for a particular request.
See the [request message builder](#http-request-message) for all available options:

```c#
// instantiates handler with parameterless constructor
MyWebApi
	.Handler<MyHttpMessageHandler>();
	
// instantiates handler with constructor function to resolve dependencies
MyWebApi
	.Handler(() => new MyHttpMessageHandler(mockedInjectedService));
	
// or provide already instantiated handler
MyWebApi
	.Handler(myHttpMessageHandlerInstance);
	
// attaches inner handler to the current one
MyWebApi
	.Handler<MyDelegatingHandler>()
	.WithInnerHandler<AnotherHttpMessageHandler>();
	
// or provide the inner handler as an instance
MyWebApi
	.Handler<MyDelegatingHandler>()
	.WithInnerHandler(myInnerHandler);
	
// attaches inner handler by using construction function
var handler = MyWebApi
	.Handler<MyDelegatingHandler>()
	.WithInnerHandler(() => new AnotherHttpMessageHandler());
	
// attaches chain of handlers
MyWebApi
	.Handler<MyDelegatingHandler>()
	.WithInnerHandler<AnotherDelegatingHandler>(
		firstInnerHandler => firstInnerHandler.WithInnerHandler<YetAnotherDelegatingHandler>(
			secondInnerHandler => secondInnerHandler.WithInnerHandler<AndAnotherDelegatingHandler>));
			
// sets the HTTP request to the handler
MyWebApi
	.Handler<MyHttpMessageHandler>()
	.WithHttpRequestMessage(someHttpRequestMessage);
	
// sets the HTTP request to the handler
// by using builder 
MyWebApi
	.Handler<MyHttpMessageHandler>()
	.WithHttpRequestMessage(request => request.WithMethod(HttpMethod.Get));
	
// adds HTTP configuration for the particular test case
MyWebApi
	.Handler(myHttpMessageHandlerInstance);
	.WithHttpConfiguration(config);
```

[To top](#table-of-contents)

### Handler response validation

See [HTTP response validations](#http-response-message-result) for all available options:

```c#
// tests whether the handler returns response message successfully
MyWebApi
	.Handler<MyHttpMessageHandler>()
	.WithHttpRequestMessage(request => request.WithMethod(HttpMethod.Get))
	.ShouldReturnHttpResponseMessage();

// tests whether the handler returns response message successfully
// with specific status code
MyWebApi
	.Handler<MyHttpMessageHandler>()
	.WithHttpRequestMessage(request => request.WithMethod(HttpMethod.Get))
	.ShouldReturnHttpResponseMessage()
	.WithStatusCode(HttpStatusCode.OK);
	
// tests whether the handler returns response message successfully
// with specific content model
MyWebApi
	.Handler<MyHttpMessageHandler>()
	.WithHttpRequestMessage(request => request.WithMethod(HttpMethod.Get))
	.ShouldReturnHttpResponseMessage()
	.WithResponseModelOfType<ResponseModel>()
	.Passing(m => m.Id == 1);
	
// tests whether the handler returns response message successfully
// with specific response header
MyWebApi
	.Handler<MyHttpMessageHandler>()
	.WithHttpRequestMessage(request => request.WithMethod(HttpMethod.Get))
	.ShouldReturnHttpResponseMessage()
	.ContainingHeader("SomeHeader");
```

[To top](#table-of-contents)
  
### Controller instantiation

You have a couple of options from which you can setup the controller you want to test:

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
	
// adds HTTP configuration for the particular test case
MyWebApi
	.Controller(myWebApiControllerInstance)
	.WithHttpConfiguration(config);
```

[To top](#table-of-contents)

### HTTP request message

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
// by collection of key-value pairs
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithFormUrlEncodedContent(someKeyValuePairCollection));
		
// adding form URL encoded content to the request message
// by query string
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithFormUrlEncodedContent("First=FirstValue&Second=SecondValue"));
		
// adding JSON content to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithJsonContent(someJsonString));
		
// adding StringContent to the request message
// with text/plain media type
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithStringContent(someStringContent));
		
// adding StringContent to the request message
// with custom media type
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithStringContent(someStringContent, MediaType.ApplicationXml));
		
// adding StringContent to the request message
// with custom encoding
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithStringContent(someStringContent, Encoding.UTF8));
		
// adding StringContent to the request message
// with custom encoding and media type
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithStringContent(someStringContent, Encoding.UTF8, MediaType.ApplicationXml));
		
// adding custom header to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithHeader("SomeHeader", "SomeHeaderValue"));
		
// adding custom header with multiple values to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithHeader("SomeHeader", new[] { "SomeHeaderValue", "AnotherHeaderValue" }));
		
// adding custom headers provided as dictionary to the request message
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithHeaders(someDictionaryWithHeaders));
		
// adding custom content header to the request message
// * adding content headers requires content to be initialized and set
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithContentHeader("SomeContentHeader", "SomeContentHeaderValue"));
		
// adding custom content header with multiple values to the request message
// * adding content headers requires content to be initialized and set
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithContentHeader("SomeContentHeader", new[] { "SomeContentHeaderValue", "AnotherContentHeaderValue" }));
		
// adding custom content headers provided as dictionary to the request message
// * adding content headers requires content to be initialized and set
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(request => request
		.WithContentHeaders(someDictionaryWithContentHeaders));
		
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
		
// adding request message by providing HttpRequestMessage instance
MyWebApi
	.Controller<WebApiController>()
	.WithHttpRequestMessage(httpRequestMessage);
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

### Controller attributes testing

You can test for specific or custom controller attributes:

```c#
// tests whether controller has no attributes
MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.NoAttributes();
	
// tests whether controller has at least 1 attribute of any type
MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.Attributes();
	
// tests whether controller has specific number of attributes
MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.Attributes(withTotalNumberOf: 2);
	
// tests whether controller has specific attribute type
MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.Attributes(attributes => attributes
		.ContainingAttributeOfType<AuthorizeAttribute>());
		
// tests whether controller has RouteAttribute
// with specific expected route template
MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.Attributes(attributes => attributes
		.ChangingRouteTo("/api/anotheraction"));
		
// tests whether controller has RouteAttribute
// with specific expected route template
// and optional expected name and order 
MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.Attributes(attributes => attributes
		.ChangingRouteTo(
			"/api/anotheraction",
			withName: "SomeRoute",
			withOrder: 1));
			
// tests whether controller has RoutePrefixAttribute
// with specific expected route prefix
MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.Attributes(attributes => attributes
		.ChangingRoutePrefixTo("/api/anothercontroller"));
			
// tests whether controller has AllowAnonymousAttribute
MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.Attributes(attributes => attributes
		.AllowingAnonymousRequests());
		
// tests whether controller has AuthorizeAttribute
MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.Attributes(attributes => attributes
		.RestrictingForAuthorizedRequests());
		
// tests whether controller has AuthorizeAttribute
// with expected authorized roles
// and/or expected authorized users
MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.Attributes(attributes => attributes
		.RestrictingForAuthorizedRequests(
			withAllowedRoles: "Admin,Moderator",
			withAllowedUsers: "John,George"));
	
// combining tests with AndAlso
MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.Attributes(attributes =>
		attributes
			.AllowingAnonymousRequests()
			.AndAlso() // AndAlso is not necessary
			.ChangingRoutePrefixTo("/api/anothercontroller"));
```

[To top](#table-of-contents)

### Action attributes testing

You can test for specific or custom action attributes decorated on the called action:

```c#
// tests whether action has no attributes
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.NoActionAttributes();
	
// tests whether action has at least 1 attribute of any type
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes();
	
// tests whether action has specific number of attributes
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(withTotalNumberOf: 2);
	
// tests whether action has specific attribute type
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.ContainingAttributeOfType<HttpGetAttribute>());
		
// tests whether action has ActionNameAttribute
// with specific expected name
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.ChangingActionNameTo("AnotherAction"));
		
// tests whether action has RouteAttribute
// with specific expected route template
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.ChangingRouteTo("/api/anotheraction"));
		
// tests whether action has RouteAttribute
// with specific expected route template
// and optional expected name and order 
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.ChangingRouteTo(
			"/api/anotheraction",
			withName: "SomeRoute",
			withOrder: 1));
			
// tests whether action has AllowAnonymousAttribute
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.AllowingAnonymousRequests());
		
// tests whether action has AuthorizeAttribute
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.RestrictingForAuthorizedRequests());
		
// tests whether action has AuthorizeAttribute
// with expected authorized roles
// and/or expected authorized users
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.RestrictingForAuthorizedRequests(
			withAllowedRoles: "Admin,Moderator",
			withAllowedUsers: "John,George"));
			
// tests whether action has NonActionAttribute
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.DisablingActionCall());
		
// tests whether action has restriction for HTTP method
// by providing type of the attribute
// * tests for AcceptsVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.RestrictingForRequestsWithMethod<HttpGetAttribute>());
		
// tests whether action has restriction for HTTP method
// by providing the expected method as string
// *tests for AcceptsVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.RestrictingForRequestsWithMethod("GET"));
		
// tests whether action has restriction for HTTP method
// by providing the expected method as HttpMethod class
// *tests for AcceptsVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.RestrictingForRequestsWithMethod(HttpMethod.Get));
		
// tests whether action has restriction for HTTP methods
// by providing IEnumerable of strings
// *tests for AcceptsVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.RestrictingForRequestsWithMethod(new List<string> { "GET", "HEAD" }));
		
// tests whether action has restriction for HTTP methods
// by providing strings parameters
// *tests for AcceptsVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.RestrictingForRequestsWithMethod("GET", "HEAD"));
		
// tests whether action has restriction for HTTP methods
// by providing IEnumerable of HttpMethod class
// *tests for AcceptsVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.RestrictingForRequestsWithMethod(new List<HttpMethod>
		{
			HttpMethod.Get,
			HttpMethod.Head 
		}));

// tests whether action has restriction for HTTP methods
// by providing HttpMethod parameters
// *tests for AcceptsVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes => attributes
		.RestrictingForRequestsWithMethod(HttpMethod.Get, HttpMethod.Head));
		
// combining tests with AndAlso
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes =>
		attributes
			.AllowingAnonymousRequests()
			.AndAlso() // AndAlso is not necessary
			.DisablingActionCall()
			.AndAlso()
			.RestrictingForRequestsWithMethod<HttpGetAttribute>());
			
// continuing testing other aspects of the action
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes(attributes =>
		attributes
			.AllowingAnonymousRequests()
			.AndAlso()
			.DisablingActionCall())
	.AndAlso()
	.ShouldHave()
	.ValidModelState()
	.AndAlso()
	.ShouldReturn()
	.Ok();
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
	
// tests whether the action throws AggregateException
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldThrow()
	.AggregateException();
	
// tests whether the action throws AggregateException
// with specific total number of inner exceptions
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldThrow()
	.AggregateException(withNumberOfInnerExceptions: 2);
	
// tests whether the action throws AggregateException
// containing specific inner exceptions
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldThrow()
	.AggregateException()
	.ContainingInnerExceptionOfType<NullReferenceException>()
	.AndAlso() // AndAlso is not necessary
	.ContainingInnerExceptionOfType<InvalidOperationException>();

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
	
// tests whether the action throws HttpResponseException
// with specific HttpResponseMessage
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldThrow()
	.HttpResponseException()
	.WithHttpResponseMessage()
	.WithVersion(1, 1)
	.AndAlso() // AndAlso is not necessary
	.WithStatusCode(HttpStatusCode.InternalServerError);
```

[To top](#table-of-contents)

### Action results

You can test for specific return values or the default IHttpActionResult types:

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

[To top](#table-of-contents)

#### HTTP response message result

```c#
// tests whether the action returns HttpResponseMessage
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage();
	
// tests whether the action returns HttpResponseMessage
// with content response model of type
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithResponseModelOfType<ResponseModel>();
	
// tests whether the action returns HttpResponseMessage
// with content response model object deeply equal
// to the provided one
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithResponseModel(someResponseModelObject);
	
// tests whether the action returns HttpResponseMessage
// with HttpContent of type
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithContentOfType<ObjectContent>();
	
// tests whether the action returns HttpResponseMessage
// with StringContent and expected string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithStringContent("SomeString");
	
// tests whether the action returns HttpResponseMessage
// with specific media type formatter
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithMediaTypeFormatter(new JsonMediaTypeFormatter());
	
// tests whether the action returns HttpResponseMessage
// with specific media type formatter
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
	
// tests whether the action returns HttpResponseMessage
// with the default media type formatter for the framework
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithDefaultMediaTypeFormatter();
	
// tests whether the action returns HttpResponseMessage
// containing header with specific name
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.ContainingHeader("SomeHeader");
	
// tests whether the action returns HttpResponseMessage
// containing header with specific name and value
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.ContainingHeader("SomeHeader", "SomeValue");
	
// tests whether the action returns HttpResponseMessage
// containing header with specific name and values
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.ContainingHeader("SomeHeader", new[] { "SomeHeaderValue", "AnotherHeaderValue" });
	
// tests whether the action returns HttpResponseMessage
// containing headers provided as dictionary
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.ContainingHeaders(someDictionaryWithHeaders);
	
// tests whether the action returns HttpResponseMessage
// containing content header with specific name
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.ContainingContentHeader("SomeContentHeader");
	
// tests whether the action returns HttpResponseMessage
// containing content header with specific name and value
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.ContainingContentHeader("SomeContentHeader", "SomeContentValue");
	
// tests whether the action returns HttpResponseMessage
// containing content header with specific name and values
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.ContainingContentHeader("SomeContentHeader", new[] { "SomeContentHeaderValue", "AnotherContentHeaderValue" });
	
// tests whether the action returns HttpResponseMessage
// containing content headers provided as dictionary
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.ContainingContentHeaders(someDictionaryWithContentHeaders);
	
// tests whether the action returns HttpResponseMessage
// with specific status code
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithStatusCode(HttpStatusCode.OK);
	
// tests whether the action returns HttpResponseMessage
// with success status code (from 200 to 299)
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithSuccessStatusCode();
	
// tests whether the action returns HttpResponseMessage
// with HTTP version as string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithVersion("1.1");
	
// tests whether the action returns HttpResponseMessage
// with HTTP version as numbers
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithVersion(1, 1);
	
// tests whether the action returns HttpResponseMessage
// with HTTP version as Version class
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithVersion(someVersion);
	
// tests whether the action returns HttpResponseMessage
// with different type of properties by using AndAlso()
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.WithSuccessStatusCode()
	.AndAlso() // AndAlso is not necessary
	.ContainingHeader("SomeHeader")
	.AndAlso()
	.WithResponseModelOfType<ResponseModel>();
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
	
// tests whether the action returns OkResult with object
// deeply equal to the provided one
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
// with different type of properties by using AndAlso()
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
	
// tests whether the action returns
// RedirectToRouteResult to specific route values
// provided by lambda expression
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Redirect()
	.To<AnotherController>(c => c.AnotherAction("ParameterRouteValue"));

// tests whether the action returns RedirectResult
// with location provided as string
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Redirect()
	.AtLocation("http://somehost.com/someuri/1?query=someQuery");

// tests whether the action returns RedirectResult
// with location provided as URI
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Redirect()
	.AtLocation(someUri);

// tests whether the action returns RedirectResult
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
	
// tests whether the action returns
// CreatedAtRouteNegotiatedContentResult<T> to specific route values
// provided by lambda expression
MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Created()
	.At<AnotherController>(c => c.AnotherAction("ParameterRouteValue"));
	
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
// with different type of properties by using AndAlso()
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

### HTTP server

You can test the full pipeline by providing HTTP configuration. You can start global HTTP server in your test/class/assembly initialize method and set test cases with different requests or just instantiate separate server for each test:

```c#
// starts HTTP server with global configuration
// set with MyWebApi.IsUsing
// * the server is disposed after the test
// * HTTP request can be set just like in the controller unit tests
// * HTTP response can be tested just like in the controller unit tests
MyWebApi.IsUsing(config);

MyWebApi
	.Server()
	.Working() // working will instantiate new HTTP server with the global configuration
	.WithHttpRequestMessage(
		request => request
			.WithMethod(HttpMethod.Post)
			.WithRequestUri("api/MyController/MyAction/5"))
	.ShouldReturnHttpResponseMessage()
	.WithStatusCode(HttpStatusCode.OK);

// starts HTTP server with specific for the test
// HTTP configuration
// * the server is disposed after the test
MyWebApi
	.Server()
	.Working(config) // working will instantiate new HTTP server with the provided specific configuration
	.WithHttpRequestMessage(httpRequestMessage)
	.ShouldReturnHttpResponseMessage()
	.WithStatusCode(HttpStatusCode.OK);

// starts global HTTP server,
// which is not disposed after the test
// and must be stopped manually
// * can be set in test/class/assembly initialize method
MyWebApi.Server().Starts(); // uses global configuration
// or the equivalent MyWebApi.IsUsing(config).AndStartsServer();
// or pass specific configuration MyWebApi.Server().Starts(config);

MyWebApi
	.Server()
	.Working() // working will use the global server started above
	.WithHttpRequestMessage(httpRequestMessage)
	.ShouldReturnHttpResponseMessage()
	.WithStatusCode(HttpStatusCode.OK);

// more test cases on the same global server

// stops the global HTTP server
MyWebApi.Server().Stops();
```

[To top](#table-of-contents)

### OWIN pipeline

You can test over the full pipeline by providing OWIN start up class and optional network host and post. You can start global HTTP server in your test/class/assembly initialize method and set test cases with different requests or just instantiate separate server for each test:

```c#
// starts OWIN web server with the provided host and port. If such are not provided, default is "http://localhost:80"
// * the server is disposed after the test
// * HTTP request can be set just like in the controller unit tests
// * HTTP response can be tested just like in the controller unit tests
MyWebApi
	.Server()
	.Working<Startup>(host: "https://localhost", port: 9876) // 
	.WithHttpRequestMessage(
		request => request
			.WithMethod(HttpMethod.Post)
			.WithRequestUri("api/MyController/MyAction/5"))
	.ShouldReturnHttpResponseMessage()
	.WithStatusCode(HttpStatusCode.OK);

// starts OWIN server with specific 
// for the test Startup class 
// * the server is disposed after the test
// * since host and port are not provided, the default "http://localhost:80" is used
MyWebApi
	.Server()
	.Working<Startup>() // working will instantiate new OWIN server with the specified Startup class
	.WithHttpRequestMessage(httpRequestMessage)
	.ShouldReturnHttpResponseMessage()
	.WithStatusCode(HttpStatusCode.OK);

// starts global OWIN server,
// which is not disposed after the test
// and must be stopped manually
// * can be set in test/class/assembly initialize method
MyWebApi.Server().Starts<Startup>(); // host and port can be optionally specified

MyWebApi
	.Server()
	.Working() // working will use the global OWIN server started above
	.WithHttpRequestMessage(httpRequestMessage)
	.ShouldReturnHttpResponseMessage()
	.WithStatusCode(HttpStatusCode.OK);

// more test cases on the same global server

// stops the global OWIN server
MyWebApi.Server().Stops();

// saving the server builder instance for later usage
// * can be done with the normal HTTP server too
var server = MyWebApi.Server().Starts<Startup>();

server
	.WithHttpRequestMessage(httpRequestMessage)
	.ShouldReturnHttpResponseMessage()
	.WithStatusCode(HttpStatusCode.OK);
	
// more test cases on the same global server
	
MyWebApi.Server().Stops();
```

Summary - the **".Working()"** method without parameters will check if the global OWIN server is started. If not, it will check whether a global HTTP server is started. If not, it will instantiate new HTTP server using the global HTTP configuration. The first match will process the request and test over the response. If no server can be started, exception will be thrown. Using **".Working(config)"** will start new HTTP server with the provided configuration and dispose it after the test. Using **".Working<Startup>()"** will start new OWIN server with the provided start up class and dispose it after the test. Global server can be started with **"MyWebApi.Server().Starts()"** and it will be HTTP or OWIN dependending on the parameters. Global servers can be stopped with **"MyWebApi.Server().Stops()"**, no matter HTTP or OWIN.

[To top](#table-of-contents)

### AndProvide... methods

You can get different Web API specific objects used in the test case where applicable by using AndProvide... methods.
Useful for additional custom test assertions:

```c#
// get HTTP handler instance
// * method is available wherever HTTP handlers are tested
var handler = MyWebApi
	.Handler<MyHttpMessageHandler>()
	.WithHttpRequestMessage(request => request.WithMethod(HttpMethod.Get))
	.ShouldReturnHttpResponseMessage()
	.WithSuccessStatusCode()
	.AndProvideTheHandler();
	
// get controller instance
// * method is available almost everywhere throughout the API
var controller = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.AndProvideTheController();
	
// get the HTTP configuration
// * method is available almost everywhere throughout the API
var config = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.AndProvideTheHttpConfiguration();
	
// get controller attributes
// * currently method returns correct attributes
// * only after ShouldHave().Attributes() call
var attributes = MyWebApi
	.Controller<WebApiController>()
	.ShouldHave()
	.Attributes()
	.AndProvideTheControllerAttributes();
	
// get the HTTP request message
// * method is available almost everywhere throughout the API
var request = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.AndProvideTheHttpRequestMessage();
	
// get the HTTP response message
// * method is available wherever HTTP response message is tested
var response = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.HttpResponseMessage()
	.AndProvideTheHttpResponseMessage();
	
// get action name
// * method is available almost everywhere throughout the API
var actionName = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldReturn()
	.Ok()
	.AndProvideTheActionName();

// get action attributes
// * currently method returns correct attributes
// * only after ShouldHave().ActionAttributes() call
var attributes = MyWebApi
	.Controller<WebApiController>()
	.Calling(c => c.SomeAction())
	.ShouldHave()
	.ActionAttributes()
	.AndProvideTheActionAttributes();
	
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