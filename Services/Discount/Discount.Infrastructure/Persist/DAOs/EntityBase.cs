using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discount.Infrastructure.Persist.DAOs
{
	public interface IBaseActive
	{
		public bool IsActive { get; set; }
	}
	public interface IBaseDelete
	{
		public bool IsDelete { get; set; }
	}

	public abstract class EntityFlagBase : IBaseActive, IBaseDelete
	{
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }
	}
	public abstract class EntityPrimaryBase<T>
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public virtual T Id { get; set; }
	}
	public abstract class EntityBase<T> : EntityPrimaryBase<T>, IBaseActive, IBaseDelete
	{
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }
	}
	public abstract class EntityActiveBase<T> : EntityPrimaryBase<T>, IBaseActive
	{
		public bool IsActive { get; set; }
	}
	public abstract class EntityDeleteBase<T> : EntityPrimaryBase<T>, IBaseDelete
	{
		public bool IsDelete { get; set; }
	}
}
