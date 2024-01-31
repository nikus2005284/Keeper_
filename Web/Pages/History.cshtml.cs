using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System;
using ClassLibrary;

namespace Web.Pages
{
    public class HistoryModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public List<Individ> Individs { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Individs = await client.GetFromJsonAsync<List<Individ>>($"https://localhost:7108/api/Api/GetIndivid/{id}");
            return Page();
        }
    }
}
