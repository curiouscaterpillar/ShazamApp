using System;
using System.Collections.Generic;
using System.Text;

namespace ShazamApp.Core.Models
{
	public class ErrorResponseModel
	{
		public int Status { get; set; }

		public string Message { get; set; }

		public static ErrorResponseModel FromInternal(string messasge)
		{
			return new ErrorResponseModel() { Status = 500, Message = messasge };
		}

		public static ErrorResponseModel FromBadrequest(string message)
		{
			return new ErrorResponseModel() { Status = 401, Message = message };
		}
	}
}
