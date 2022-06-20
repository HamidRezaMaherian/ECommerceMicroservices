using System;
using System.Linq.Expressions;

namespace FileActor.Internal
{
	public class ObjectStreamConfiguration
	{
		protected ObjectStreamConfiguration()
		{

		}
		public string Target { get; protected set; }
		public string RelativePath { get; protected set; }
		public Expression<Func<object>> Expression { get; protected set; }
	}
	public class ObjectStreamConfigProxy : ObjectStreamConfiguration
	{
		public ObjectStreamConfigProxy(string target)
		{
			Target = target;
		}
		public ObjectStreamConfigProxy SetExpression(Expression<Func<object>> expression)
		{
			Expression = expression;
			return this;
		}
		public ObjectStreamConfigProxy SetRelativePath(string relativePath)
		{
			RelativePath = relativePath;
			return this;
		}
	}
}