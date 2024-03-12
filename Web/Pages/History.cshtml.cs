using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System;
using ClassLibrary;
using Microsoft.Extensions.Options;

namespace Web.Pages
{
    public class HistoryModel : PageModel
    {
        static HttpClient client = new HttpClient();
        public List<Individ> Individs { get; set; } = new List<Individ>();
        public List<GroupUsers> GroupUsers { get; set; } = new List<GroupUsers>();
        public static List<string> Options { get; set; } = new List<string> { "Личная", "Групповая" };
        public int Option;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Individs = await client.GetFromJsonAsync<List<Individ>>($"https://localhost:7108/api/Api/GetIndivid/{id}");
            GroupUsers = await client.GetFromJsonAsync<List<GroupUsers>>($"https://localhost:7108/api/Api/GetGroups/{id}");
            Option = await client.GetFromJsonAsync<int>("https://localhost:7108/api/Api/GetOption");
            return Page();
        }

        public async Task<IActionResult> OnPostOptionAsync(int option)
        {
            Option = option;
            using var response = await client.PostAsJsonAsync($"https://localhost:7108/api/Api/PostOption", Option);
            return Page();
        }
    }
}
