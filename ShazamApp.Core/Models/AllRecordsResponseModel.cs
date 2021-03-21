using System;
using System.Collections.Generic;
using System.Text;

namespace ShazamApp.Core.Models
{
	public class AllRecordsResponseModel
	{
		public IEnumerable<RecordResponseModel> Records { get; set; }
	}
}
