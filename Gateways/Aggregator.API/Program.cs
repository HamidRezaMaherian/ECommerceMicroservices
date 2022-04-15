using Polly;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddHttpClient("Discount", opt =>
		{
			opt.BaseAddress = new Uri(builder.Configuration["ApiConfigurations:Discount"]);
		}).AddTransientHttpErrorPolicy(cfg =>
			cfg.WaitAndRetryAsync(new[]
			{
				TimeSpan.FromSeconds(5),
				TimeSpan.FromSeconds(10),
				TimeSpan.FromSeconds(15),
			}));
		builder.Services.AddHttpClient("Inventory", opt =>
		{
			opt.BaseAddress = new Uri(builder.Configuration["ApiConfigurations:Inventory"]);
		}).AddTransientHttpErrorPolicy(cfg =>
			cfg.WaitAndRetryAsync(new[]
			{
				TimeSpan.FromSeconds(5),
				TimeSpan.FromSeconds(10),
				TimeSpan.FromSeconds(15),
			})); ;
		builder.Services.AddHttpClient("Basket", opt =>
		{
			opt.BaseAddress = new Uri(builder.Configuration["ApiConfigurations:Basket"]);
		}).AddTransientHttpErrorPolicy(cfg =>
			cfg.WaitAndRetryAsync(new[]
			{
				TimeSpan.FromSeconds(5),
				TimeSpan.FromSeconds(10),
				TimeSpan.FromSeconds(15),
			})); ;
		builder.Services.AddHttpClient("Basket", opt =>
		{
			opt.BaseAddress = new Uri(builder.Configuration["ApiConfigurations:Basket"]);
		}).AddTransientHttpErrorPolicy(cfg =>
			cfg.WaitAndRetryAsync(new[]
			{
				TimeSpan.FromSeconds(5),
				TimeSpan.FromSeconds(10),
				TimeSpan.FromSeconds(15),
			})); ;
		builder.Services.AddHttpClient("Order", opt =>
		{
			opt.BaseAddress = new Uri(builder.Configuration["ApiConfigurations:Order"]);
		}).AddTransientHttpErrorPolicy(cfg =>
			cfg.WaitAndRetryAsync(new[]
			{
				TimeSpan.FromSeconds(5),
				TimeSpan.FromSeconds(10),
				TimeSpan.FromSeconds(15),
			})); ;
		builder.Services.AddHttpClient("Product", opt =>
		{
			opt.BaseAddress = new Uri(builder.Configuration["ApiConfigurations:Product"]);
		}).AddTransientHttpErrorPolicy(cfg =>
			cfg.WaitAndRetryAsync(new[]
			{
				TimeSpan.FromSeconds(5),
				TimeSpan.FromSeconds(10),
				TimeSpan.FromSeconds(15),
			})); ;
		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}
