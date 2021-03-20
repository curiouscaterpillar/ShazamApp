using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShazamApp.Handlers;

namespace ShazamApp
{
    public static class SaveEndpoint
    {
        [FunctionName("Save")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "save")] HttpRequest req, ILogger log)
        {
            try
            {
                return await new SaveEndpointHandler().HandleAsync(req, log);
            }
            catch (System.Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
