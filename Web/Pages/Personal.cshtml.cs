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
            byte[] fileByteArray;
            if (uploadedPhoto != null)
            {
                using (var item = new MemoryStream())
                {
                    uploadedPhoto.CopyTo(item);
                    fileByteArray = item.ToArray(); //2nd change here
                    Individ.photo = fileByteArray;
                }
            }

            if (uploadedPdf != null)
            {
                using (var item = new MemoryStream())
                {
                    uploadedPdf.CopyTo(item);
                    fileByteArray = item.ToArray(); //2nd change here
                    Individ.pdf = fileByteArray;
                }
            }

            using var response = await client.PostAsJsonAsync($"https://localhost:7108/api/Api/CreateIndivid", Individ);

            // если объект на сервере найден, то есть статусный код равен 404
            if (response.StatusCode == HttpStatusCode.OK)
            {
               
                _f.Flash(Types.Success, $"Заявка успешно отправлена!", dismissable: true);
                return RedirectToPage();

            }
            else
            {
                _f.Flash(Types.Danger, $"Что-то пошло не так", dismissable: true);
                return Page();
            }
        }
    }
}
