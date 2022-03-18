using System.Runtime.Serialization;

namespace Product.Application.DTOs;

public class InsertOperationException : Exception
{
	public InsertOperationException() : base()
	{
	}
	protected InsertOperationException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
	public InsertOperationException(string? message)
		: base(message)
	{
	}
	public InsertOperationException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}
}