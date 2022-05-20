using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Identity.API.Pages.Diagnostics;

[SecurityHeaders]
[Authorize]
public class Index : PageModel
{
    public ViewModel View { get; set; }
        
    public async Task<IActionResult> OnGet()
    {
        var localAddresses = new string[] { "127.0.0.1", "::1", HttpContext?.Connection?.LocalIpAddress?.ToString() };
        if (!localAddresses.Contains(HttpContext?.Connection?.RemoteIpAddress?.ToString()))
        {
            return NotFound();
        }

        View = new ViewModel(await HttpContext?.AuthenticateAsync());
            
        return Page();
    }
}