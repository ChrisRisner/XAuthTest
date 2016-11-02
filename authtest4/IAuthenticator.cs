using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace authtest4
{
	public interface IAuthenticator
	{
		Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri);

		Task<AuthenticationResult> AuthenticateSilently(string tenantId, string resource, string clientId);
	}
}
