using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;
using ShazamApp.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ShazamApp.Handlers
{
    public class SaveEndpointHandler : EndpointHandler
    {
        private const string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=shazamstorage;AccountKey=bTJZ7qVlObtyi22kpdgXW5hLYma/UyqXyqf3KaQ03ZusV1Qu/uw2kB4DO0RegkSfPhcnnlngTqS445R7D7WsRA==;EndpointSuffix=core.windows.net";

        private CloudStorageAccount _storageAccount;
        private CloudTableClient _tableClient;

        public SaveEndpointHandler() : base()
        {
            _storageAccount = CloudStorageAccount.Parse(ConnectionString);
            _tableClient = _storageAccount.CreateCloudTableClient(new TableClientConfiguration());
        }

        public override async Task<IActionResult> HandleAsync(HttpRequest req, ILogger log)
        {
            string artist = req.Query["artist"];
            string song = req.Query["song"];

            string tableName = "shazamRecords";

            CloudTable table = _tableClient.ListTables()
                .FirstOrDefault(t => t.Name == tableName);

            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(
                new ShazamRecordEntity(artist, song));

            TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
            ShazamRecordEntity insertedArtist = result.Result as ShazamRecordEntity;

            if (result.HttpStatusCode >= StatusCodes.Status200OK && result.HttpStatusCode < StatusCodes.Status300MultipleChoices)
            {
                log.LogInformation($"Created Artist: {insertedArtist.Artist} with song {song}");

                return new StatusCodeResult(StatusCodes.Status200OK);
            }

            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
    }
}
