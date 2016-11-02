using System;
using Newtonsoft.Json;

namespace authtest4
{
	public partial class SubscriptionResponse
	{
		public class SubscriptionPolicies
		{

			[JsonProperty("locationPlacementId")]
			public string LocationPlacementId { get; set; }

			[JsonProperty("quotaId")]
			public string QuotaId { get; set; }
		}
	}

	public partial class SubscriptionResponse
	{
		public class Subscription
		{

			[JsonProperty("id")]
			public string Id { get; set; }

			[JsonProperty("subscriptionId")]
			public string SubscriptionId { get; set; }

			[JsonProperty("displayName")]
			public string DisplayName { get; set; }

			[JsonProperty("state")]
			public string State { get; set; }

			[JsonProperty("subscriptionPolicies")]
			public SubscriptionPolicies SubscriptionPolicy { get; set; }
		}
	}

	public partial class SubscriptionResponse
	{

		[JsonProperty("value")]
		public Subscription[] SubscriptionCollection { get; set; }
	}
}
