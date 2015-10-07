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

namespace MyWebApi.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Attributes;
    using Builders.Contracts.Attributes;
    using Common.Extensions;

    public static class AttributesValidator
    {
        public static void ValidateNoAttributes(
            IEnumerable<object> attributes,
            Action<string, string> failedValidationAction)
        {
            if (attributes.Any())
            {
                failedValidationAction(
                    "not have any attributes",
                    "it had some");
            }
        }

        public static void ValidateNumberOfAttributes(
            IEnumerable<object> attributes,
            Action<string, string> failedValidationAction,
            int? withTotalNumberOf = null)
        {
            var attributesList = attributes.ToList();
            if (!attributesList.Any())
            {
                failedValidationAction(
                    "have at least 1 attribute",
                    "in fact none was found");
            }

            var actualNumberOfActionAttributes = attributesList.Count;
            if (withTotalNumberOf.HasValue && actualNumberOfActionAttributes != withTotalNumberOf)
            {
                failedValidationAction(
                    string.Format("have {0} {1}",
                        withTotalNumberOf,
                        withTotalNumberOf != 1 ? "attributes" : "attribute"),
                    string.Format("in fact found {0}", actualNumberOfActionAttributes));
            }
        }

        public static void ValidateAttributes(
            IEnumerable<object> attributes,
            BaseAttributesTestBuilder attributesTestBuilder,
            Action<string, string> failedValidationAction)
        {
            var attributesList = attributes.ToList();
            ValidateNumberOfAttributes(attributesList, failedValidationAction);
            var validations = attributesTestBuilder.GetAttributeValidations();
            validations.ForEach(v => v(attributesList));
        }
    }
}
