using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
	public class IndexModel : PageModel
	{
        public IActionResult OnGet()
        {
            var sessionId = HttpContext.Session.GetString("SampleSession");

            if (sessionId != null)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("Auth");
            }
        }
	}
}