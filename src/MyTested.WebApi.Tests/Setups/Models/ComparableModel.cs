// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.Setups.Models
{
    using System;

    public class ComparableModel : IComparable
    {
        public int Integer { get; set; }

        public string String { get; set; }

        public int CompareTo(object obj)
        {
            var objAsComparableModel = (ComparableModel)obj;
            if (this.Integer < objAsComparableModel.Integer)
            {
                return -1;
            }
            else if (this.Integer > objAsComparableModel.Integer)
            {
                return 1;
            }

            return 0;
        }
    }
}
