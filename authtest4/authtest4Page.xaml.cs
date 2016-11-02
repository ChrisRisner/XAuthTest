
using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
//using Microsoft.Identity.Client;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading;
using System.Collections.ObjectModel;

namespace authtest4
{
	public partial class authtest4Page : ContentPage
	{
		public IPlatformParameters platformParameters { get; set; }
		ObservableCollection<SubscriptionResponse.Subscription> SubscriptionCollection { get; set; }
		bool IsFirstRunComplete { get; set; } = false;

		public authtest4Page()
		{
			InitializeComponent();
			//platformParameters = platparams;
			SubscriptionCollection = new ObservableCollection<SubscriptionResponse.Subscription>();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
//			App.PCA.PlatformParameters = platformParameters;
		}

		async void Login_Clicked(object sender, System.EventArgs e)
		{
			// let's see if we have a user in our belly already
			try
			{
				//AuthenticationResult ar = await App.PCA.AcquireTokenSilentAsync(App.Scopes);
				//RefreshUserData(ar.Token);
				//btnSignInSignOut.Text = "Sign out";
				//App.PCA.RedirectUri = "http://localhost";
				//AuthenticationResult ar = await App.PCA.AcquireTokenAsync(App.Scopes);


				//Console.WriteLine("AR Response: " + ar.IdToken);


				AuthenticationContext authContext = new AuthenticationContext(App.commonAuthority);
				if (authContext.TokenCache.ReadItems().Count() > 0)
					authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
				//authResult = await authContext.AcquireTokenAsync(App.graphResourceUri, 	App.ClientID, App.returnUri, platformParameters);
				App.authResult = await authContext.AcquireTokenAsync(App.ManagementResourceUri, App.ClientID, App.returnUri, platformParameters);

				Console.WriteLine("Auth done");
				Console.WriteLine("AR: " + App.authResult.IdToken);
				GetTenants();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error");
				Console.WriteLine(ex.Message);
				// doesn't matter, we go in interactive more
				//btnSignInSignOut.Text = "Sign in";
			}
		}


		private async Task GetTenants()
		{
			TenantResponse tenantCollection = null;
			var requestUrl = "https://management.azure.com/tenants?api-version=2015-01-01";
			try
			{
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.authResult.AccessToken);
				                                                                                                   
					
				var tenantResponse = await client.GetStringAsync(requestUrl);
				tenantCollection = JsonConvert.DeserializeObject<TenantResponse>(tenantResponse);
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error!", ex.Message, "Dismiss");
			}
			foreach (var tenant in tenantCollection.TenantCollection)
			{
				Console.WriteLine(tenant.Id + " :: " + tenant.TenantId);
				await GetSubscriptions(tenant.TenantId);
			}

			//dosomething();
		}

		private async Task GetSubscriptions(string tenantId)
		{
			var requestUrl = "https://management.azure.com/subscriptions?api-version=2014-04-01";
			try
			{
				HttpClient client = new HttpClient();
				//var data = await DependencyService.Get<IAuthenticator>().AuthenticateSilently(tenantId, App.ManagementResourceUri, App.ClientID);
				var loginAuthnority = "https://login.windows.net/" + tenantId;
				var data = await DependencyService.Get<IAuthenticator>().Authenticate(loginAuthnority, App.ManagementResourceUri, App.ClientID, App.returnUri.ToString());

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", data.AccessToken);


				//client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.authResult.AccessToken);

				var subsriptionResponse = await client.GetStringAsync(requestUrl);
				var subscriptions = JsonConvert.DeserializeObject<SubscriptionResponse>(subsriptionResponse);
				foreach (var subscription in subscriptions.SubscriptionCollection)
				{
					Console.WriteLine("subscrip: " + subscription.Id);
					SubscriptionCollection.Add(subscription);
				}
				IsFirstRunComplete = true;
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error!", ex.Message, "Dismiss");
			}

		}









		public async void dosomething()
		{
			try
			{
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.authResult.AccessToken);

				var storageClient = new Microsoft.WindowsAzure.Management.Storage.StorageManagementClient(client);

				CancellationTokenSource source = new CancellationTokenSource();
				CancellationToken token = source.Token;

				var result = await storageClient.StorageAccounts.ListAsync(token);



				foreach (var sa in result.StorageAccounts)
				{
					Console.WriteLine("SA: " + sa.Name);

				}


			}
			catch (Exception ex)
			{
				Console.WriteLine("EX: " + ex.Message);
			}

			//result
		}
	}
}
