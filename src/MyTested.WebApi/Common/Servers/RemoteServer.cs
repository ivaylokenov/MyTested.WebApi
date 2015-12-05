// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Common.Servers
{
    using System;
    using System.Net.Http;

    public static class RemoteServer
    {
        public static HttpClient GlobalClient { get; private set; }

        public static bool GlobalIsConfigured
        {
            get { return GlobalClient != null; }
        }

        public static HttpClient CreateNewClient(string baseAddress)
        {
            return new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public static void ConfigureGlobal(string baseAddress)
        {
            if (GlobalIsConfigured)
            {
                DisposeGlobal();
            }

            GlobalClient = CreateNewClient(baseAddress);
        }

        public static bool DisposeGlobal()
        {
            if (GlobalClient == null)
            {
                return false;
            }

            GlobalClient.Dispose();
            GlobalClient = null;

            return true;
        }
    }
}
