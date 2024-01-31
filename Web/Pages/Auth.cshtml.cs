using ClassLibrary;
using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Web.Pages
{
    public class AuthModel : PageModel
    {
        private readonly IFlasher _f;

        public AuthModel(IFlasher f)
        {
            _f = f;
        }

        static HttpClient httpClient = new HttpClient();

        [BindProperty]
        public User InputAuth { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            using var response = await httpClient.PostAsJsonAsync($"https://localhost:7108/api/Api/Auth", InputAuth);
            // ���� ������ �� ������� ������, �� ���� ��������� ��� ����� 404
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // ��������� �����
                User person = await response.Content.ReadFromJsonAsync<User>();
                if (person != null)
                {
                    HttpContext.Session.SetString("SampleSession", $"{person.id}");
                    _f.Flash(Types.Success, $"����� ����������, {person.userName}!", dismissable: true);
                    return RedirectToPage("Index");
                }
            }
            else
            {
                _f.Flash(Types.Danger, $"�������� ����� ��� ������!", dismissable: true);
                return Page();
            }
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            // ����� ������
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("SampleSession");
            Response.Cookies.Delete("SampleSession");
            _f.Flash(Types.Success, $"Session clear!", dismissable: true);
            return RedirectToPage("Auth");
        }
    }
}
