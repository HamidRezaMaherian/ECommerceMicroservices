using Consul;

public static class Statics
{
	public static IEnumerable<AgentServiceRegistration> Discoveries = new List<AgentServiceRegistration>()
	{
		new AgentServiceRegistration()
		{
			ID="basket:3",
			Name="basket",
			Port=5000,
			Address="localhost"
		},
		new AgentServiceRegistration()
		{
			ID="discount:e",
			Name="discount",
			Port=5002,
			Address="localhost"
		},
		new AgentServiceRegistration()
		{
			ID="identity:1",
			Name="identity",
			Port=5005,
			Address="localhost",
		},
		new AgentServiceRegistration()
		{
			ID="inventory:c",
			Name="inventory",
			Port=5006,
			Address="localhost"
		},
		new AgentServiceRegistration()
		{
			ID="order:3",
			Name="order",
			Port=5008,
			Address="localhost"
		},
		new AgentServiceRegistration()
		{
			ID="payment:2",
			Name="payment",
			Port=5010,
			Address="localhost"
		},
		new AgentServiceRegistration()
		{
			ID="product:3",
			Name="product",
			Port=5012,
			Address="localhost"
		},
		new AgentServiceRegistration()
		{
			ID="ui:5",
			Name="ui",
			Port=5014,
			Address="localhost"
		},
		new AgentServiceRegistration()
		{
			ID="apigateway:3",
			Name="apigateway",
			Port=5016,
			Address="localhost"
		},
	};
}