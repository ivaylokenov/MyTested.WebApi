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

namespace MyWebApi
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Contains common MIME type values.
    /// </summary>
    public static class MediaType
    {
        /// <summary>
        /// Represents text/plain (txt).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string TextPlain = "text/plain";

        /// <summary>
        /// Represents text/html (html).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string TextHtml = "text/html";

        /// <summary>
        /// Represents text/css (css).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string TextCss = "text/css";

        /// <summary>
        /// Represents image/bmp (bmp).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string ImageBmp = "image/bmp";

        /// <summary>
        /// Represents image/jpeg (jpeg or jpg).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string ImageJpeg = "image/jpeg";

        /// <summary>
        /// Represents image/png (png).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string ImagePng = "image/png";

        /// <summary>
        /// Represents image/gif (gif).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string ImageGif = "image/gif";

        /// <summary>
        /// Represents "application/x-javascript" (js).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string ApplicationJavaScript = "application/x-javascript";

        /// <summary>
        /// Represents application/json (json).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string ApplicationJson = "application/json";

        /// <summary>
        /// Represents application/xml (xml).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string ApplicationXml = "application/xml";

        /// <summary>
        /// Represents application/x-www-form-urlencoded.
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string FormUrlEncoded = "application/x-www-form-urlencoded";

        /// <summary>
        /// Represents application/octet-stream (exe).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string ApplicationOctetStream = "application/octet-stream";

        /// <summary>
        /// Represents application/zip (zip).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string ApplicationZip = "application/zip";

        /// <summary>
        /// Represents audio/mpeg (mp3).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string AudioMpeg = "audio/mpeg";

        /// <summary>
        /// Represents audio/vorbis (ogg).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string AudioVorbis = "audio/vorbis";

        /// <summary>
        /// Represents video/avi (avi).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string VideoAvi = "video/avi";

        /// <summary>
        /// Represents video/mpeg (mpeg).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string VideoMpeg = "video/mpeg";

        /// <summary>
        /// Represents video/quicktime (qt).
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "*", Justification = "MIME types are not recognised by the spelling checker.")]
        public const string VideoQuicktime = "video/quicktime";
    }
}
