using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace authtest4
{
	public class Tenant
	{

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("tenantId")]
		public string TenantId { get; set; }
	}

	public class TenantResponse
	{

		[JsonProperty("value")]
		public Tenant[] TenantCollection { get; set; }
	}
}
