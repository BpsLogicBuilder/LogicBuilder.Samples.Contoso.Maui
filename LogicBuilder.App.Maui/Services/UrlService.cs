using Microsoft.Extensions.Configuration;
using System;

namespace LogicBuilder.App.Maui.Services
{
    public class UrlService(IConfiguration configuration) : IUrlService
    {
        private readonly IConfiguration configuration = configuration;

        public string GetBaseUrl()
        {
            return configuration["BASE_URL"]! ?? throw new InvalidOperationException("BASE_URL is not configured");
        }
    }
}
