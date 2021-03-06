using Order.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Infrastructure.Persist.DAOs
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
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public virtual T Id { get; set; }
	}
	public abstract class EntityBaseDAO<T> : EntityPrimaryBaseDAO<T>, IBaseActive, IBaseDelete
	{
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }
	}
	public abstract class EntityActiveBaseDAO<T> : EntityPrimaryBase<T>, IBaseActive
	{
		public bool IsActive { get; set; }
	}
	public abstract class EntityDeleteBaseDAO<T> : EntityPrimaryBaseDAO<T>, IBaseDelete
	{
		public bool IsDelete { get; set; }
	}
}
