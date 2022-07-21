using Consul;
using Newtonsoft.Json;

public class ServiceRegisterDTO
{
	public string Name { get; set; }

	public string Address { get; set; }

	public int Port { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string[] Tags { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public bool EnableTagOverride { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public AgentServiceCheck Check { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public AgentServiceCheck[] Checks { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public IDictionary<string, string> Meta { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public IDictionary<string, ServiceTaggedAddress> TaggedAddresses { get; set; }
}