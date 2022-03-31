using System.Runtime.Serialization;

namespace Inventory.Application.Exceptions;

public class UpdateOperationException : Exception
{
	public UpdateOperationException() : base()
	{
	}
	protected UpdateOperationException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
	public UpdateOperationException(string message)
		: base(message)
	{
	}
	public UpdateOperationException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}