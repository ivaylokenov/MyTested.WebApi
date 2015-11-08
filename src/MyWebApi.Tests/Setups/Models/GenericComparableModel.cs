// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.Setups.Models
{
    using System;

    public class GenericComparableModel : IComparable<GenericComparableModel>
    {
        public int Integer { get; set; }

        public string String { get; set; }

        public int CompareTo(GenericComparableModel other)
        {
            if (this.Integer < other.Integer)
            {
                return -1;
            }
            else if (this.Integer > other.Integer)
            {
                return 1;
            }

            return 0;
        }
    }
}
