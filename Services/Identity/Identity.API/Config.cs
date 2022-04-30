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
			 };
		public static IEnumerable<TestUser> TestUsers = new List<TestUser>
		{
			new TestUser()
			{
				SubjectId="lasjioasdf",
				Username="hamid",
				Password="hamidalireza",
				IsActive=true,
			}
		};
		public static IEnumerable<ApiScope> ApiScopes =>
			 new List<ApiScope>
			 {
					 new ApiScope("api1", "My API"),
					 new ApiScope("api2", "My API")
			 };
		public static IEnumerable<Client> Clients =>
			 new List<Client>
			 {
                // interactive ASP.NET Core MVC client
                new Client
					 {
						  ClientId = "webapp",
						  ClientSecrets = { new Secret("webapp-secret".Sha256()) },

						  AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5019/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5019/signout-callback-oidc" },

						  AllowedScopes = new List<string>
						  {
								IdentityServerConstants.StandardScopes.OpenId,
								IdentityServerConstants.StandardScopes.Profile,
						  }
					 }
			 };
	}
}