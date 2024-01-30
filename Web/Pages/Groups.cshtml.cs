using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClassLibrary;
using Core.Flash;
using System.Net.Http;
using System;
using System.IO;
using Aspose.Cells;
using System.Net;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace Web.Pages
{
    public class GroupsModel : PageModel
    {
        #region Для парсинга Exel
       
        #endregion



        private readonly IFlasher _f;
        private readonly IWebHostEnvironment _appEnvironment;


        public GroupsModel(IFlasher f, IWebHostEnvironment appEnvironment)
        {
            _f = f;
            _appEnvironment = appEnvironment;
        }

        static HttpClient client = new HttpClient();

        public List<Target> Targets { get; set; } = new List<Target>();
        public List<Division> Divisions { get; set; } = new List<Division>();
        public List<Employees> Employees { get; set; } = new List<Employees>();
        [BindProperty]
        public List<GroupUsers> groupUser { get; set; }

        [BindProperty]
        public GroupUsers User { get; set; }



        public async Task<IActionResult> OnGetAsync(List<GroupUsers> users)
        {
            Targets = await client.GetFromJsonAsync<List<Target>>("https://localhost:7108/api/Api/GetTargets");
            Divisions = await client.GetFromJsonAsync<List<Division>>("https://localhost:7108/api/Api/GetDivisions");
            Employees = await client.GetFromJsonAsync<List<Employees>>("https://localhost:7108/api/Api/GetEmployees");

            if(users.Count > 0)
            {
                foreach(var user in users.ToList())
                {
                    groupUser.Add(user);
                }
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync(IFormFile uploadedPhotoGroupUsers, IFormFile uploadedPdfGroupUsers, IFormFile uploadedExelGroupUsers)
        {
            if(groupUser != null)
            {
                string pathPhoto = "/GroupUsers/" + uploadedPhotoGroupUsers.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathPhoto, FileMode.Create))
                {
                    await uploadedPhotoGroupUsers.CopyToAsync(fileStream);
                }
                groupUser[0].photoPath = pathPhoto;

                string pathPdf = "/GroupUsers/" + uploadedPdfGroupUsers.FileName;

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathPdf, FileMode.Create))
                {
                    await uploadedPdfGroupUsers.CopyToAsync(fileStream);
                }
                groupUser[0].pdfPath = pathPdf;
            }
            

            if(uploadedExelGroupUsers != null)
            {
                string pathExel = "/GroupUsers/" + uploadedExelGroupUsers.FileName;

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathExel, FileMode.Create))
                {
                    await uploadedExelGroupUsers.CopyToAsync(fileStream);
                }

                using (Workbook wb = new Workbook(_appEnvironment.WebRootPath + pathExel))
                {
                    WorksheetCollection collection = wb.Worksheets;
                    for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
                    {
                        // Получить рабочий лист, используя его индекс
                        Worksheet worksheet = collection[worksheetIndex];

                        User.firstName = Convert.ToString(worksheet.Cells[1, 1].Value);
                        User.name = Convert.ToString(worksheet.Cells[1, 2].Value);
                        User.lastName = Convert.ToString(worksheet.Cells[1, 3].Value);
                        User.number = Convert.ToString(worksheet.Cells[1, 4].Value);
                        User.email = Convert.ToString(worksheet.Cells[1, 5].Value);
                        User.organization = Convert.ToString(worksheet.Cells[1, 6].Value);
                        User.note = Convert.ToString(worksheet.Cells[1, 7].Value);
                        User.birthday = Convert.ToDateTime(Convert.ToString(worksheet.Cells[1, 8].Value));
                        User.passport = Convert.ToString(worksheet.Cells[1, 9].Value) + " " + Convert.ToString(worksheet.Cells[1, 10].Value);

                        groupUser.Add(User);
                    }
                }
            }
            //groupUser.Add(User);

            using var response = await client.PostAsJsonAsync($"https://localhost:7108/api/Api/CreateGroups", groupUser);


            if (response.StatusCode == HttpStatusCode.OK)
            {

                _f.Flash(Types.Success, $"Заявка успешно отправлена!", dismissable: true);
                return RedirectToPage("Groups");

            }
            else
            {
                _f.Flash(Types.Danger, $"Что-то пошло не так", dismissable: true);
                return Page();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(string seriesPassport, string numberPassport)
        {
            User.passport = seriesPassport + " " + numberPassport;
            groupUser.Add(User);
            /*using var response = await client.PostAsJsonAsync($"https://localhost:7108/api/Api/PostKostil", User);*/
            return await OnGetAsync(groupUser);
        }

        public async Task<IActionResult> OnPostClearAsync()
        {
            return RedirectToPage("Groups");
        }
    }
}
