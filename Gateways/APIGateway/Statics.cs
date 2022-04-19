using Consul;

public static class Statics
{
	public static IEnumerable<AgentServiceRegistration> Discoveries = new List<AgentServiceRegistration>()
	{
		new AgentServiceRegistration()
		{
			Name="basket",
			Port=5000,
			Address="http://localhost"
		},
		new AgentServiceRegistration()
		{
			Name="discount",
			Port=5001,
			Address="https://localhost"
		},
		new AgentServiceRegistration()
		{
			Name="identity",
			Port=5002,
			Address="https://localhost"
		},
		new AgentServiceRegistration()
		{
			Name="inventory",
			Port=5003,
			Address="https://localhost"
		},
		new AgentServiceRegistration()
		{
			Name="order",
			Port=5004,
			Address="https://localhost"
		},
		new AgentServiceRegistration()
		{
			Name="payment",
			Port=5005,
			Address="https://localhost"
		},
		new AgentServiceRegistration()
		{
			Name="product",
			Port=5006,
			Address="https://localhost"
		},
		new AgentServiceRegistration()
		{
			Name="ui",
			Port=5007,
			Address="https://localhost"
		},
	};
}