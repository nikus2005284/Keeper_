using ClassLibrary;
using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Web.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly IFlasher _f;

        public SignUpModel(IFlasher f)
        {
            _f = f;
        }

        static HttpClient httpClient = new HttpClient();

        [BindProperty]
        public User InputSignUp { get; set; }
         public async Task<IActionResult> OnPageHandlerSelection()
        {
          

            if (!ModelState.IsValid)
            {
                _f.Flash(Types.Danger, $"��������� �� ��������!", dismissable: true);
                return Page();
            }
            using var response = await httpClient.PostAsJsonAsync($"https://localhost:7108/api/Api/SignUp", InputSignUp);

            // ���� ������ �� ������� ������, �� ���� ��������� ��� ����� 404
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToPage("Auth");

            }
            return Page();
        }
    }
}
