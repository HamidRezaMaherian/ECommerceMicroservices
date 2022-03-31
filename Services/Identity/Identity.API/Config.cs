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
                // machine to machine client
                new Client
					 {
						  ClientId = "client",
						  ClientSecrets = { new Secret("secret".Sha256()) },
						  AllowedGrantTypes = GrantTypes.ClientCredentials,
						                      // scopes that client has access to
                    AllowedScopes = {
							 	IdentityServerConstants.StandardScopes.OpenId,
								IdentityServerConstants.StandardScopes.Profile
							 ,"api1","api2" }
					 },
                
                // interactive ASP.NET Core MVC client
                new Client
					 {
						  ClientId = "mvc",
						  ClientSecrets = { new Secret("secret".Sha256()) },

						  AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

						  AllowedScopes = new List<string>
						  {
								IdentityServerConstants.StandardScopes.OpenId,
								IdentityServerConstants.StandardScopes.Profile,
								IdentityServerConstants.StandardScopes.Email,
								IdentityServerConstants.StandardScopes.Address,
								"api1"
						  }
					 }
			 };
	}
}