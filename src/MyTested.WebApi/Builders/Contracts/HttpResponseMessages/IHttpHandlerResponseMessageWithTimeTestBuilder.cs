// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.HttpResponseMessages
{
    using System;

    public interface IHttpHandlerResponseMessageWithTimeTestBuilder : IHttpHandlerResponseMessageTestBuilder
    {
        IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Action<TimeSpan> assertions);

        IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Func<TimeSpan, bool> predicate);

        TimeSpan AndProvideTheResponseTime();
    }
}
