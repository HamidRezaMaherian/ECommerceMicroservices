using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

internal class IdentityProfile : IProfileService
{
	private readonly UserManager<IdentityUser> _userManager;

	public IdentityProfile(UserManager<IdentityUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task GetProfileDataAsync(ProfileDataRequestContext context)
	{
		if (context?.Subject?.Claims.Any() ?? false)
		{
			if (context?.Subject?.Claims!=null)
			{
			context.IssuedClaims.AddRange(context.Subject.Claims);
			}
		}
		else
		{
			var user = await _userManager.FindByNameAsync(context?.Subject?.Identity?.Name ?? "");
			if (user != null)
			{
				context?.IssuedClaims.AddRange(
					Enumerable.Concat(
						await _userManager.GetClaimsAsync(user),
						(await _userManager.GetRolesAsync(user)).Select(i => new Claim(JwtClaimTypes.Role, i))
					).ToList()
					);
			}
		}
	}

	public Task IsActiveAsync(IsActiveContext context)
	{
		context.IsActive = true;
		return Task.CompletedTask;
	}
}