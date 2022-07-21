using Consul;
using Newtonsoft.Json;

namespace ServiceDiscovery
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.ConfigureConsul();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapPost("/service/register", (IConsulClient consulClient, ServiceRegisterDTO serviceRegistrationInput) =>
			{
				var serviceRegister = JsonConvert.DeserializeObject<AgentServiceRegistration>(
					JsonConvert.SerializeObject(serviceRegistrationInput)
					);
				serviceRegister.ID = $"{serviceRegister.Name}:{Guid.NewGuid()}";
				consulClient?.Agent.ServiceDeregister(serviceRegister.ID).Wait();
				consulClient?.Agent.ServiceRegister(serviceRegister).Wait();
			})
			.WithName("ServiceRegister");

			app.MapDelete("/service/unregister/{id}", (IConsulClient consulClient,string id) =>
			{
				consulClient?.Agent.ServiceDeregister(id).Wait();
			})
			.WithName("ServiceUnregister");

			app.Run();
		}
	}
}