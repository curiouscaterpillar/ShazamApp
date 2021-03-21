using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShazamApp.Handlers;
using ShazamApp.Core.Models;

namespace ShazamApp
{
    public static class SaveRecordEndpoint
    {
        [FunctionName("Save")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "records")] HttpRequest req, ILogger log)
        {
            try
            {
                return await new SaveEndpointHandler().HandleAsync(req, log);
            }
            catch (System.Exception e)
            {
                return new ObjectResult(ErrorResponseModel.FromInternal(e.Message));
            }
        }
    }
}
