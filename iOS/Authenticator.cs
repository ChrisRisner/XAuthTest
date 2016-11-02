using System;
using System.Threading.Tasks;
using authtest4.iOS;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using System.Linq;
using UIKit;

[assembly: Dependency(typeof(Authenticator))]
namespace authtest4.iOS
{
	class Authenticator : IAuthenticator
	{
		public async Task<AuthenticationResult> AuthenticateSilently(string tenantId, string resource, string clientId)
		{
			try
			{
				var loginAuthnority = "https://login.windows.net/" + tenantId;
				var authContext = new AuthenticationContext(loginAuthnority);
				var authResult = await authContext.AcquireTokenSilentAsync(resource, clientId, new UserIdentifier(App.authResult.UserInfo.UniqueId, UserIdentifierType.UniqueId));
				return authResult;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Ex: " + ex.Message);
				Console.WriteLine("SO: " + ex.StackTrace);
				throw ex;
			}
		}

		public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
		{
			var authContext = new AuthenticationContext(authority);

			if (authContext.TokenCache.ReadItems().Any())
				authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
			var authResult = await authContext.AcquireTokenAsync(resource, clientId, new Uri(returnUri),
				new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController));
			return authResult;
		}

	}
}