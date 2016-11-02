using System;
using System.Threading.Tasks;
using authtest4.Droid;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using System.Linq;
using Android.App;

[assembly: Dependency(typeof(Authenticator))]
namespace authtest4.Droid
{
	public class Authenticator : IAuthenticator
	{
		public async Task<AuthenticationResult> AuthenticateSilently(string tenantId, string resource, string clientId)
		{
			var loginAuthnority = "https://login.microsoftonline.com/" + tenantId;
			var authContext = new AuthenticationContext(loginAuthnority);
			var authResult = await authContext.AcquireTokenSilentAsync(resource, clientId, new UserIdentifier(App.authResult.UserInfo.UniqueId, UserIdentifierType.UniqueId));
			return authResult;
		}

		public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
		{
			var authContext = new AuthenticationContext(authority);
			if (authContext.TokenCache.ReadItems().Any())
				authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
			var authResult = await authContext.AcquireTokenAsync(resource, clientId, new Uri(returnUri), new PlatformParameters((Activity)Forms.Context));
			return authResult;
		}
	}
}
