using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShazamApp.Core.Models;

namespace ShazamApp.Core.Abstractions
{
	public interface IRecordService
	{
		Task<AllRecordsResponseModel> GetRecordsAsync();

		Task<RecordResponseModel> SaveRecordAsync(string artist, string title);
	}
}
