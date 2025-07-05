using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Net.Http.Json;

namespace UDEP.Encuestas.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public LoginModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [BindProperty]
        public string Username { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _factory.CreateClient("api");
            var response = await client.PostAsJsonAsync("api/auth/login", new { Username, Password });
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas");
                return Page();
            }

            var tokenJson = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<string>(tokenJson);
            HttpContext.Session.SetString("token", token!);
            return RedirectToPage("Menu");
        }
    }
}
