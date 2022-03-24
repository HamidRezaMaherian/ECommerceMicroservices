using System.Runtime.Serialization;

namespace UI.Application.Exceptions;

public class ReadOperationException : Exception
{
	public ReadOperationException() : base()
	{
	}
	protected ReadOperationException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
	public ReadOperationException(string message)
		: base(message)
	{
	}
	public ReadOperationException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}