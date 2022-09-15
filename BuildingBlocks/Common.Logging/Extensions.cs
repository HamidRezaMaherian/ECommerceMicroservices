using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;

public static class Extensions
{
	public static ILoggingBuilder RegisterLoggingProvider(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
	{
		var loggingConfig = new LoggerConfiguration()
						  .Enrich.FromLogContext()
						  .WriteTo.Console()
						  .WriteTo.Elasticsearch(
								new ElasticsearchSinkOptions(new Uri(configuration["ELS_URI"]))
								{
									//IndexFormat = $"applogs-{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
									AutoRegisterTemplate = true,
									NumberOfShards = 2,
									NumberOfReplicas = 1
								})
						  //.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
						  //.Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
						  .ReadFrom.Configuration(configuration)
						  .CreateLogger();
		loggingBuilder.ClearProviders();
		loggingBuilder.AddSerilog(loggingConfig);
		return loggingBuilder;
	}
}