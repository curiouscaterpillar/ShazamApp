using System;

namespace ShazamApp.Core.Models
{
	public class RecordResponseModel
    {
        public string Artist { get; set; }

        public string Song { get; set; }

		public DateTime Timestamp { get; set; }
	}
}
