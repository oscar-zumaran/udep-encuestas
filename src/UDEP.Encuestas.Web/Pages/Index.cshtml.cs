using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UDEP.Encuestas.Web.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                return RedirectToPage("Login");
            return RedirectToPage("Menu");
        }
    }
}
