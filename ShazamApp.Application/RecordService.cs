using System;
using System.Threading.Tasks;
using ShazamApp.Core;
using ShazamApp.Core.Abstractions;
using ShazamApp.Core.Models;
using ShazamApp.DataAccess;
using ShazamApp.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ShazamApp.Application
{
	public class RecordService : IRecordService
	{
		private readonly IRecordContext<Record> _context;

		public RecordService()
		{
			_context = new ShazamContext();
		}

		public async Task<AllRecordsResponseModel> GetRecordsAsync()
		{
			IEnumerable<Record> records = await _context.GetAsync();

			if (records == null)
			{
				return new AllRecordsResponseModel() { Records = new List<RecordResponseModel>() };
			}

			return new AllRecordsResponseModel
			{
				Records = records.Select(
					r => new RecordResponseModel
					{
						Artist = r.Artist,
						Song = r.Song,
						Timestamp = r.Timestamp.DateTime
					})
			};
		}

		public async Task<RecordResponseModel> SaveRecordAsync(string artist, string title)
		{
			if (string.IsNullOrEmpty(artist) || string.IsNullOrEmpty(title))
			{
				return default;
			}

			Record record = await _context.InsertAsync(new Record(artist, title));

			return new RecordResponseModel()
			{
				Artist = record.Artist,
				Song = record.Song,
				Timestamp = record.Timestamp.DateTime
			};
		}
	}
}
