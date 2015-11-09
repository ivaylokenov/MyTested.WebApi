// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.Setups.Models
{
    public class EqualsModel
    {
        public int Integer { get; set; }

        public string String { get; set; }

        public override bool Equals(object obj)
        {
            return this.Integer == ((EqualsModel)obj).Integer;
        }

        public override int GetHashCode()
        {
            return this.Integer.GetHashCode() ^ this.String.GetHashCode();
        }
    }
}
