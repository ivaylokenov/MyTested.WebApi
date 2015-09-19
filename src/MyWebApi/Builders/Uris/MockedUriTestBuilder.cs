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

namespace MyWebApi.Builders.Uris
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Contracts.Uris;

    /// <summary>
    /// Used for testing URI location in a created result.
    /// </summary>
    public class MockedUriTestBuilder : MockedUriBuilder
    {
        private readonly ICollection<Func<MockedUri, Uri, bool>> validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedUriTestBuilder" /> class.
        /// </summary>
        public MockedUriTestBuilder()
        {
            this.validations = new List<Func<MockedUri, Uri, bool>>();
        }

        /// <summary>
        /// Tests whether the URI has the same host as the provided one.
        /// </summary>
        /// <param name="host">Host part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public override IAndUriTestBuilder WithHost(string host)
        {
            this.validations.Add((expected, actual) => expected.Host == actual.Host);
            return base.WithHost(host);
        }

        /// <summary>
        /// Tests whether the URI has the same port as the provided one.
        /// </summary>
        /// <param name="port">Port part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public override IAndUriTestBuilder WithPort(int port)
        {
            this.validations.Add((expected, actual) => expected.Port == actual.Port);
            return base.WithPort(port);
        }

        /// <summary>
        /// Tests whether the URI has the same absolute path as the provided one.
        /// </summary>
        /// <param name="absolutePath">Absolute path part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public override IAndUriTestBuilder WithAbsolutePath(string absolutePath)
        {
            this.validations.Add((expected, actual) => expected.AbsolutePath == actual.AbsolutePath);
            return base.WithAbsolutePath(absolutePath);
        }

        /// <summary>
        /// Tests whether the URI has the same scheme as the provided one.
        /// </summary>
        /// <param name="scheme">Scheme part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public override IAndUriTestBuilder WithScheme(string scheme)
        {
            this.validations.Add((expected, actual) => expected.Scheme == actual.Scheme);
            return base.WithScheme(scheme);
        }

        /// <summary>
        /// Tests whether the URI has the same query as the provided one.
        /// </summary>
        /// <param name="query">Query part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public override IAndUriTestBuilder WithQuery(string query)
        {
            this.validations.Add((expected, actual) => expected.Query == actual.Query);
            return base.WithQuery(query);
        }

        /// <summary>
        /// Tests whether the URI has the same fragment as the provided one.
        /// </summary>
        /// <param name="fragment">Document fragment part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public override IAndUriTestBuilder WithFragment(string fragment)
        {
            this.validations.Add((expected, actual) => expected.Fragment == actual.Fragment);
            return base.WithFragment(fragment);
        }

        internal ICollection<Func<MockedUri, Uri, bool>> GetMockedUriValidations()
        {
            return this.validations;
        }
    }
}
