//using Microsoft.Identity.Client;
using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

namespace authtest4
{
	public partial class App : Application
	{
		//public static PublicClientApplication PCA = null;
		//public static string ClientID = "6f86e4f1-4dcf-4d51-8151-5492c1ec4291";
		public static string ClientID = "1950a258-227b-4e31-a9cf-717495945fc2";	
		public static string[] Scopes = { "User.Read" };
		public static string Username = string.Empty;
		//public static string commonAuthority = "https://login.windows.net/d3d30b62-81ff-4ec8-8966-23bb6b5346dd";//common";
		public static string commonAuthority = "https://login.windows.net/common";
		//public static string commonAuthority = "https://login.microsoftonline.com/common";
		//public static Uri returnUri = new Uri("http://azurestorageexplorer");
		public static Uri returnUri = new Uri("urn:ietf:wg:oauth:2.0:oob");
		public const string graphResourceUri = "https://graph.windows.net";
		public static string graphApiVersion = "2013-11-08";
		public static string ManagementResourceUri = "https://management.core.windows.net/";

		public static AuthenticationResult authResult = null;

		public App()
		{
			InitializeComponent();
			//PCA = new PublicClientApplication(ClientID);
			//PCA = new PublicClientApplication(commonAuthority, ClientID);

			MainPage = new authtest4Page();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
