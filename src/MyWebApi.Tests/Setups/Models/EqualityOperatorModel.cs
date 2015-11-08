// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.Setups.Models
{
    public class EqualityOperatorModel
    {
        public int Integer { get; set; }

        public string String { get; set; }

        public static bool operator ==(EqualityOperatorModel first, EqualityOperatorModel second)
        {
            if (ReferenceEquals(first, null) && ReferenceEquals(second, null))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return first.Integer == second.Integer;
        }

        public static bool operator !=(EqualityOperatorModel first, EqualityOperatorModel second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj) || ReferenceEquals(this, obj) || obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((EqualityOperatorModel)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Integer * 397) ^ (this.String != null ? this.String.GetHashCode() : 0);
            }
        }

        protected bool Equals(EqualityOperatorModel other)
        {
            return this.Integer == other.Integer;
        }
    }
}
