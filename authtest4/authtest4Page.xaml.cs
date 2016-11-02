
using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
//using Microsoft.Identity.Client;
using Xamarin.Forms;
using System.Linq;

namespace authtest4
{
	public partial class authtest4Page : ContentPage
	{
		public IPlatformParameters platformParameters { get; set; }
		AuthenticationResult authResult = null;

		public authtest4Page()
		{
			InitializeComponent();
			//platformParameters = platparams;
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
				authResult = await authContext.AcquireTokenAsync(App.graphResourceUri, 	App.ClientID, App.returnUri, platformParameters);

				Console.WriteLine("Auth done");
				Console.WriteLine("AR: " + authResult.IdToken);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error");
				Console.WriteLine(ex.Message);
				// doesn't matter, we go in interactive more
				//btnSignInSignOut.Text = "Sign in";
			}
		}
	}
}
