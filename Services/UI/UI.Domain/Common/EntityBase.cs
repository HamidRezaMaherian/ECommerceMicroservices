namespace UI.Domain.Common
{
	public interface IBaseActive
	{
		public bool IsActive { get; set; }
	}
	public abstract class EntityPrimaryBase<T>
	{
		public T Id { get; set; }
	}
	public abstract class EntityBase<T> : EntityPrimaryBase<T>, IBaseActive
	{
		public bool IsActive { get; set; }
	}
}
