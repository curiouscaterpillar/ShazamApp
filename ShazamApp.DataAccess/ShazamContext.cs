using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ShazamApp.Core;
using ShazamApp.Entities;
using Microsoft.Azure.Cosmos.Table;

namespace ShazamApp.DataAccess
{
    public class ShazamContext : IRecordContext<Record>
    {
        private const string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=shazamstorage;AccountKey=bTJZ7qVlObtyi22kpdgXW5hLYma/UyqXyqf3KaQ03ZusV1Qu/uw2kB4DO0RegkSfPhcnnlngTqS445R7D7WsRA==;EndpointSuffix=core.windows.net";

        private const string TableName = "shazamRecords";

        private readonly CloudStorageAccount _storageAccount;

        private readonly CloudTableClient _tableClient;

        public ShazamContext()
        {
            _storageAccount = CloudStorageAccount.Parse(ConnectionString);
            _tableClient = _storageAccount.CreateCloudTableClient(new TableClientConfiguration());
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Record>> GetAsync()
        {
            CloudTable table = _tableClient.ListTables()
                .FirstOrDefault(t => t.Name == TableName);

            if (table == null)
            {
                return default;
            }

            IEnumerable<Record> records = table.ExecuteQuery(new TableQuery<Record>());

            return Task.FromResult(records);
        }

        public async Task<Record> InsertAsync(Record record)
        {
            CloudTable table = _tableClient.ListTables()
                .FirstOrDefault(t => t.Name == TableName);

            if (table == null)
            {
                return default;
            }

            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(record);

            TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
            Record insertedArtist = result.Result as Record;

            insertedArtist.Timestamp = new DateTimeOffset(DateTime.Now);

            return insertedArtist;
        }
    }
}
