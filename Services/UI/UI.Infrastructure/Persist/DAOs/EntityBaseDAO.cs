using MongoDB.Bson.Serialization.Attributes;
using UI.Domain.Common;

namespace UI.Infrastructure.Persist.DAOs
{
	public interface IBaseDelete
	{
		public bool IsDelete { get; set; }
	}
	public abstract class EntityFlagBaseDAO : IBaseActive, IBaseDelete
	{
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }
	}
	public abstract class EntityPrimaryBaseDAO<T>
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public T Id { get; set; }
	}
	public abstract class EntityBaseDAO<T> : EntityPrimaryBaseDAO<T>, IBaseActive, IBaseDelete
	{
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }
	}
	public abstract class EntityActiveBaseDAO<T> : EntityPrimaryBaseDAO<T>, IBaseActive
	{
		public bool IsActive { get; set; }
	}
	public abstract class EntityDeleteBaseDAO<T> : EntityPrimaryBaseDAO<T>, IBaseDelete
	{
		public bool IsDelete { get; set; }
	}
}
