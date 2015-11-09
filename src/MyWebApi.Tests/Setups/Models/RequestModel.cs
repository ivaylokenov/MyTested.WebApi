// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.Setups.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RequestModel
    {
        [Range(1, int.MaxValue)]
        public int Integer { get; set; }

        [Required]
        public string RequiredString { get; set; }

        public string NonRequiredString { get; set; }

        public int NotValidateInteger { get; set; }
    }
}
