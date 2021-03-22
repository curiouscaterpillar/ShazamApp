using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShazamApp.Entities
{
    public class Record : TableEntity
    {
        public Record() { }

        public Record(string artist, string song)
        {
            PartitionKey = artist;
            RowKey = song;
        }

        public string Artist
        {
            get => PartitionKey;
            set => PartitionKey = value;
        }

        public string Song
        {
            get => RowKey;
            set => RowKey = value;
        }
    }
}
