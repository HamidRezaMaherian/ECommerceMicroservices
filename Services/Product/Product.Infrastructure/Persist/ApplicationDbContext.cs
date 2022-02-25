using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.Persist.DAOs;
using System.Reflection;

namespace Product.Infrastructure.Persist
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			SeedData(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)));
			base.OnModelCreating(builder);
		}

		#region Product
		public DbSet<BrandDAO> Brands { get; set; }
		public DbSet<ProductDAO> Products { get; set; }
		public DbSet<ProductCategoryDAO> ProductCategories { get; set; }
		public DbSet<ProductImageDAO> ProductImages { get; set; }
		public DbSet<PropertyDAO> Properties { get; set; }
		public DbSet<CategoryPropertyDAO> CategoryProperties { get; set; }
		public DbSet<ProductPropertyDAO> ProductProperties { get; set; }
		#endregion


		#region Methods
		private static void SeedData(ModelBuilder builder)
		{
		}
		#endregion
	}
}
