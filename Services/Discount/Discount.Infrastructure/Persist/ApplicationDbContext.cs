using Discount.Infrastructure.Persist.DAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace Discount.Infrastructure.Persist
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			SeedData(builder);
			builder.ApplyGlobalFilters<IBaseDelete>(i => !i.IsDelete);

			builder.ApplyConfigurationsFromAssembly(
				Assembly.GetAssembly(typeof(ApplicationDbContext)) ??
				Assembly.GetExecutingAssembly()
				);

			base.OnModelCreating(builder);
		}

		#region Discount
		public DbSet<PercentDiscountDAO> PercentDiscounts { get; private set; }
		public DbSet<PriceDiscountDAO> PriceDiscounts { get; private set; }
		#endregion


		#region Methods
		private static void SeedData(ModelBuilder builder)
		{
		}
		#endregion
	}
	public static class DbOptionsExtensions
	{
		public static void ApplyGlobalFilters<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
		{
			var entities = modelBuilder.Model
				 .GetEntityTypes()
				 .Where(e => e.ClrType.GetInterface(typeof(TInterface).Name) != null)
				 .Select(e => e.ClrType);
			foreach (var entity in entities)
			{
				var newParam = Expression.Parameter(entity);
				var newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
				modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(newbody, newParam));
			}
		}
	}
}
