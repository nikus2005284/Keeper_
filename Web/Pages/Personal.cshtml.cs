using ClassLibrary;
using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System;
using System.IO;
using System.Net;


namespace Web.Pages
{
    public class PersonalModel : PageModel
    {

        private readonly IFlasher _f;
        private readonly IWebHostEnvironment _appEnvironment;
       

        public PersonalModel(IFlasher f, IWebHostEnvironment appEnvironment)
        {
            _f = f;
            _appEnvironment = appEnvironment;
        }

        static HttpClient client = new HttpClient();

        public List<Target> Targets { get; set; } = new List<Target>();
        public List<Division> Divisions { get; set; } = new List<Division>();
        public List<Employees> Employees { get; set; } = new List<Employees>();
        public IFormFile Photo { get; }

        [BindProperty]
        public Individ Individ { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Targets = await client.GetFromJsonAsync<List<Target>>("https://localhost:7108/api/Api/GetTargets");
            Divisions = await client.GetFromJsonAsync<List<Division>>("https://localhost:7108/api/Api/GetDivisions");
            Employees = await client.GetFromJsonAsync<List<Employees>>("https://localhost:7108/api/Api/GetEmployees");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string sPassport, string nPassport, IFormFile uploadedPhoto, IFormFile uploadedPdf)
        {
            Individ.passport = sPassport + " " + nPassport;
            string pathPhoto = "/Files/" + uploadedPhoto.FileName;

            // ��������� ���� � ����� Files � �������� wwwroot
            using (var fileStream = new FileStream(@"API\Data" + pathPhoto, FileMode.Create))
            {
                await uploadedPhoto.CopyToAsync(fileStream);
            }
            
            string pathPdf = "/Files/" + uploadedPdf.FileName;

            // ��������� ���� � ����� Files � �������� wwwroot
            using (var fileStream = new FileStream(@"API\Data" + pathPdf, FileMode.Create))
            {
                await uploadedPdf.CopyToAsync(fileStream);
            }

            Individ.photoPath = pathPhoto;
            Individ.pdfPath = pathPdf;

            using var response = await client.PostAsJsonAsync($"https://localhost:7108/api/Api/CreateIndivid", Individ);

            // ���� ������ �� ������� ������, �� ���� ��������� ��� ����� 404
            if (response.StatusCode == HttpStatusCode.OK)
            {
               
                _f.Flash(Types.Success, $"������ ������� ����������!", dismissable: true);
                return RedirectToPage();

            }
            else
            {
                _f.Flash(Types.Danger, $"���-�� ����� �� ���", dismissable: true);
                return Page();
            }
        }
    }
}
