using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ShazamApp.Core.Abstractions;
using ShazamApp.Application;
using ShazamApp.Core.Models;

namespace ShazamApp.Handlers
{
	public class SaveEndpointHandler : EndpointHandler
    {
        private readonly IRecordService _recordService;

        public SaveEndpointHandler() : base()
        {
            _recordService = new RecordService();  
        }

        public override async Task<IActionResult> HandleAsync(HttpRequest req, ILogger log)
        {
            string artist = req.Query["artist"];
            string song = req.Query["song"];

            RecordResponseModel result = await _recordService.SaveRecordAsync(artist, song);

            if (result != null)
            {
                log.LogInformation($"Created Artist: {result.Artist} with song {result.Song}");

                return new ObjectResult(result);
            }

            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
    }
}
