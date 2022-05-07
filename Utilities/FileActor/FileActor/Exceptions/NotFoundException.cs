using System;
using System.Runtime.Serialization;

namespace FileActor.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException()
		{
		}

		protected NotFoundException(SerializationInfo info, StreamingContext context)
		{
		}

		public NotFoundException(string message)
		{
		}

		public NotFoundException(string message, Exception innerException)
		{
		}

	}
}
