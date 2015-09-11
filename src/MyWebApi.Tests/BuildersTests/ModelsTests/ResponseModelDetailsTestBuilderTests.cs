// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Tests.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class ResponseModelDetailsTestBuilderTests
    {
        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m =>
                {
                    Assert.AreEqual(2, m.Count);
                    Assert.AreEqual(1, m.First().IntegerValue);
                });
        }

        [Test]
        [ExpectedException(
            typeof(AssertionException))]
        public void WithResponseModelShouldThrowExceptionWithIncorrectAssertions()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m =>
                {
                    Assert.AreEqual(1, m.First().IntegerValue);
                    Assert.AreEqual(3, m.Count);
                });
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m => m.First().IntegerValue == 1);
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultWithResponse action in WebApiController expected response model IList<ResponseModel> to pass the given condition, but it failed.")]
        public void WithResponseModelShouldThrowExceptionWithWrongPredicate()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<IList<ResponseModel>>()
                .Passing(m => m.First().IntegerValue == 2);
        }
    }
}
