// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Utilities.Validators
{
    using System;

    /// <summary>
    /// Validator class containing Version validation logic.
    /// </summary>
    public static class VersionValidator
    {
        /// <summary>
        /// Tries to parse version from string.
        /// </summary>
        /// <param name="version">Provided version string.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <returns>Valid Version from the provided string.</returns>
        public static Version TryParse(string version, Action<string, string, string> failedValidationAction)
        {
            Version parsedVersion;
            if (!Version.TryParse(version, out parsedVersion))
            {
                failedValidationAction("version", "valid version string", "invalid one");
            }

            return parsedVersion;
        }
    }
}
