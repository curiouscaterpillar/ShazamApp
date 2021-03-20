using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShazamApp.Handlers
{
    public abstract class EndpointHandler
    {
        protected IConfiguration configuration;

        public EndpointHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public EndpointHandler()
        {
            this.configuration = CreateDefaultConfiguration();
        }

        public abstract Task<IActionResult> HandleAsync(HttpRequest req, ILogger log);

        private IConfiguration CreateDefaultConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
