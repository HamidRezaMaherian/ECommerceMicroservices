using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Infrastructure.Persist.DAOs
{
	public interface IBaseActive
	{
		public bool IsActive { get; set; }
	}
	public interface IBaseDelete : IEntityTypeConfiguration<IBaseDelete>
	{
		public bool IsDelete { get; set; }
	}

	public abstract class EntityFlagBase : IBaseActive, IBaseDelete
	{
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }
		public void Configure(EntityTypeBuilder<IBaseDelete> builder)
		{
			builder.HasQueryFilter(i => !i.IsDelete);
		}
	}
	public abstract class EntityPrimaryBase<T>
	{
		public virtual T Id { get; set; }
	}
	public abstract class EntityBase<T> : EntityPrimaryBase<T>, IBaseActive, IBaseDelete
	{
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }
		public void Configure(EntityTypeBuilder<IBaseDelete> builder)
		{
			builder.HasQueryFilter(i => !i.IsDelete);
		}

	}
	public abstract class EntityActiveBase<T> : EntityPrimaryBase<T>, IBaseActive
	{
		public bool IsActive { get; set; }
	}
	public abstract class EntityDeleteBase<T> : EntityPrimaryBase<T>, IBaseDelete
	{
		public bool IsDelete { get; set; }
		public void Configure(EntityTypeBuilder<IBaseDelete> builder)
		{
			builder.HasQueryFilter(i => !i.IsDelete);
		}
	}
}
