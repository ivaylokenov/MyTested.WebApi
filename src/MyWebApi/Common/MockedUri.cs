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

namespace MyWebApi.Common
{
    /// <summary>
    /// Mocked URI object.
    /// </summary>
    public class MockedUri
    {
        /// <summary>
        /// Gets or sets the host of the mocked URI.
        /// </summary>
        /// <value>Host as string.</value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port of the mocked URI.
        /// </summary>
        /// <value>Port as integer.</value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the absolute path of the mocked URI.
        /// </summary>
        /// <value>Absolute path as string.</value>
        public string AbsolutePath { get; set; }

        /// <summary>
        /// Gets or sets the scheme of the mocked URI.
        /// </summary>
        /// <value>Scheme as string.</value>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or sets the query of the mocked URI.
        /// </summary>
        /// <value>Query as string.</value>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the document fragment of the mocked URI.
        /// </summary>
        /// <value>Document fragment as string.</value>
        public string Fragment { get; set; }
    }
}
