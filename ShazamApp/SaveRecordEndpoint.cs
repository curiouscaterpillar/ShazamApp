namespace ShazamApp
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using ShazamApp.Core.Models;
    using ShazamApp.Handlers;
    using ShazamApp.Startup;

    public static class SaveRecordEndpoint
    {
        [FunctionName("Save")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "records")] HttpRequest req, ILogger log)
        {
            try
            {
                EndpointHandler handler = DependencyBootstrapper.Container.Resolve(typeof(SaveEndpointHandler)) as EndpointHandler;
                return await handler.HandleAsync(req, log);
            }
            catch (System.Exception e)
            {
                return new ObjectResult(ErrorResponseModel.FromInternal(e.Message));
            }
        }
    }
}
