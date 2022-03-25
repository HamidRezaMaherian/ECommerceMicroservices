using System.Runtime.Serialization;

namespace Discount.Application.Exceptions;

public class DeleteOperationException : Exception
{
	public DeleteOperationException() : base()
	{
	}
	protected DeleteOperationException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
	public DeleteOperationException(string message)
		: base(message)
	{
	}
	public DeleteOperationException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}