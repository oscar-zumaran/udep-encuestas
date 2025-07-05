using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace UDEP.Encuestas.Web.Pages
{
    public class MenuModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public MenuModel(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public record MenuItem(int iIdMenu, string cNombre, string? cUrl, int iNivel, int? iIdPadre, int iOrden);

        public List<MenuItem> Items { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int rolId = 1)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToPage("Login");

            var client = _factory.CreateClient("api");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"api/Menu/ByRol/{rolId}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Items = JsonSerializer.Deserialize<List<MenuItem>>(json)!;
            return Page();
        }
    }
}
