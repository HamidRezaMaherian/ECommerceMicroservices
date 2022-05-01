using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace Identity.API
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> IdentityResources =>
			 new List<IdentityResource>
			 {
					 new IdentityResources.OpenId(),
					 new IdentityResources.Profile(),
					 new IdentityResource(
						 "user-claims",
						 typeof(ClaimTypes).GetFields().Select(i=>i.GetRawConstantValue()?.ToString()))
			 };
		public static IEnumerable<Client> Clients =>
			 new List<Client>
			 {
                // interactive ASP.NET Core MVC client
                new Client
					 {
						  ClientId = "webapp",
						  ClientSecrets = { new Secret("webapp-secret".Sha512()) },

						  AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5019/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5019/signout-callback-oidc" },
						  AllowOfflineAccess=true,
						  AllowedScopes = new List<string>
						  {
								IdentityServerConstants.StandardScopes.OpenId,
								IdentityServerConstants.StandardScopes.Profile,
								//"user-claims"
						  }
					 }
			 };
	}
}