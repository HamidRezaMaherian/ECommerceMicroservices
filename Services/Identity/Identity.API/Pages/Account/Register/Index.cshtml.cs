using Identity.API.Pages.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.API.Pages.Account.Register
{
	[AllowAnonymous]
	[AutoValidateAntiforgeryToken]
	public class Index : PageModel
	{
		private readonly UserManager<IdentityUser> _userManager;

		[BindProperty]
		public InputModel Input { get; set; }

		public Index(
			 UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IActionResult> OnGetAsync(string returnUrl)
		{
			await BuildModelAsync(returnUrl);

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser()
				{
					Email = Input.Email,
					UserName = Input.UserName,
				};
				var creationResult = await _userManager.CreateAsync(user, Input.Password);
				if (!creationResult.Succeeded)
				{
					ModelState.AddModelError("", "CreationFailed");
					return Page();
				}
				else
				{
					// request for a local page
					if (Url.IsLocalUrl(Input.ReturnUrl))
					{
						return Redirect(Input.ReturnUrl);
					}
					else if (!string.IsNullOrEmpty(Input.ReturnUrl))
					{
						return Redirect($"~/login?returnUrl={Input.ReturnUrl}");
					}
					else
					{
						// user might have clicked on a malicious link - should be logged
						throw new Exception("invalid return URL");
					}
				}
			}
			// something went wrong, show form with error
			await BuildModelAsync(Input.ReturnUrl);
			return Page();
		}

		private Task BuildModelAsync(string returnUrl)
		{
			Input = new InputModel
			{
				ReturnUrl = returnUrl
			};
			return Task.CompletedTask;
		}
	}
}
