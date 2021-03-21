using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShazamApp.Core.Models;
using ShazamApp.Handlers;

namespace ShazamApp
{
    public static class GetRecordsEndpoint
    {
        [FunctionName("GetRecords")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "records")] HttpRequest req,
            ILogger log)
        {
			try
			{
                return new ObjectResult(await new GetRecordsEndpointHandler().HandleAsync(req, log));
			}
			catch (Exception e)
			{
                return new ObjectResult(ErrorResponseModel.FromInternal(e.Message));
			}
        }
    }
}
