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
		public virtual Expression<Func<object, object>> Expression { get; }
	}
	public class ObjectStreamConfiguration<T> : ObjectStreamConfiguration
	{
		public override Expression<Func<object, object>> Expression =>
			(obj) => GenericExpression.Compile().Invoke((T)obj);

		protected Expression<Func<T, object>> GenericExpression { get; set; }
	}
	public class ObjectStreamConfigProxy<T> : ObjectStreamConfiguration<T> where T : class
	{
		public ObjectStreamConfigProxy(string target)
		{
			Target = target;
		}
		public ObjectStreamConfigProxy<T> SetExpression(Expression<Func<T, object>> expression)
		{
			GenericExpression = expression;
			return this;
		}
		public ObjectStreamConfigProxy<T> SetRelativePath(string relativePath)
		{
			RelativePath = relativePath;
			return this;
		}
	}
}