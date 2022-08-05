using System;

namespace FileActor.Internal
{
	public class ObjectStreamConfiguration
	{
		protected ObjectStreamConfiguration()
		{

		}
		public string Name { get; protected set; }
		public string RelativePath { get; protected set; }
		public virtual Action<object, FileInfo> OnAfterSaved { get; }
		public virtual Action<object> OnAfterDeleted { get; }
		public virtual Func<object, object> GetFileObj { get; }
		public virtual Func<object, string> GetFileName { get; }
	}
	public class ObjectStreamConfiguration<T> : ObjectStreamConfiguration
	{
		protected Action<T, FileInfo> GenericOnAfterSaved { get; set; }
		protected Action<T> GenericOnAfterDeleted { get; set; }
		protected Func<T, object> GenericGetFileObject { get; set; }
		protected Func<T, string> GenericGetFileName { get; set; }

		public override Action<object, FileInfo> OnAfterSaved =>
						(obj, info) => GenericOnAfterSaved?.Invoke((T)obj, info);

		public override Action<object> OnAfterDeleted =>
						(obj) => GenericOnAfterDeleted?.Invoke((T)obj);
		public override Func<object, object> GetFileObj =>
						(obj) => GenericGetFileObject?.Invoke((T)obj);

		public override Func<object, string> GetFileName =>
						(obj) => GenericGetFileName?.Invoke((T)obj);
	}
	public class ObjectStreamConfigProxy<T> : ObjectStreamConfiguration<T>
	{
		public ObjectStreamConfigProxy(string name)
		{
			Name = name;
		}
		public ObjectStreamConfigProxy<T> SetOnAfterSaved(Action<T, FileInfo> expression)
		{
			GenericOnAfterSaved = expression;
			return this;
		}
		public ObjectStreamConfigProxy<T> SetOnAfterDeleted(Action<T> expression)
		{
			GenericOnAfterDeleted = expression;
			return this;
		}
		public ObjectStreamConfigProxy<T> SetFileGet(Func<T, object> expression)
		{
			GenericGetFileObject = expression;
			return this;
		}
		public ObjectStreamConfigProxy<T> SetGetFileName(Func<T, string> expression)
		{
			GenericGetFileName = expression;
			return this;
		}

		public ObjectStreamConfigProxy<T> SetRelativePath(string relativePath)
		{
			RelativePath = relativePath;
			return this;
		}
	}
}