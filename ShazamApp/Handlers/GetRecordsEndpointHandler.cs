using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShazamApp.Core.Abstractions;
using ShazamApp.Application;
using ShazamApp.Core.Models;
using System.Linq;

namespace ShazamApp.Handlers
{
    public class GetRecordsEndpointHandler : EndpointHandler
    {
        private readonly IRecordService _recordService;

        public GetRecordsEndpointHandler()
        {
            _recordService = new RecordService();
        }

        public override async Task<IActionResult> HandleAsync(HttpRequest req, ILogger log)
        {
            try
            {
                AllRecordsResponseModel records = await _recordService.GetRecordsAsync();

                var result = records
                    .Records
                    .GroupBy(r => r.Artist)
                    .Select(
                        gr => new
                        {
                            Artist = gr.Key,
                            Titles = gr.ToList()
                                .Select(
                                    t => new
                                    {
                                        song = t.Song,
                                        timestamp = t.Timestamp
                                    })
                        });

                return new ObjectResult(result) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return new ObjectResult(ErrorResponseModel.FromInternal(e.Message));
            }
        }
    }
}
