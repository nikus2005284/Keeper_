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
using System.Drawing;

namespace Web.Pages
{
    public class GroupsModel : PageModel
    {
        private readonly IFlasher _f;
        private readonly IWebHostEnvironment _appEnvironment;


        public GroupsModel(IFlasher f, IWebHostEnvironment appEnvironment)
        {
            _f = f;
            _appEnvironment = appEnvironment;
        }

        static HttpClient client = new HttpClient();

        public static List<Target> Targets { get; set; } = new List<Target>();

        public static List<Division> Divisions { get; set; } = new List<Division>();

        public static List<Employees> Employees { get; set; } = new List<Employees>();

        [BindProperty]
        public static List<GroupUsers> groupUser { get; set; }

        [BindProperty]
        public GroupUsers User { get; set; }



        /*public async Task<IActionResult> OnGetAsync(List<GroupUsers> users)
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
        }*/

        public async Task<IActionResult> OnGetAsync()
        {
            Targets = await client.GetFromJsonAsync<List<Target>>("https://localhost:7108/api/Api/GetTargets");
            Divisions = await client.GetFromJsonAsync<List<Division>>("https://localhost:7108/api/Api/GetDivisions");
            Employees = await client.GetFromJsonAsync<List<Employees>>("https://localhost:7108/api/Api/GetEmployees");

            groupUser = await client.GetFromJsonAsync<List<GroupUsers>>("https://localhost:7108/api/Api/GetKostil");

            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync(IFormFileCollection uploadedPhotoGroupUsers, IFormFileCollection uploadedPdfGroupUsers)
        {

            if (groupUser.Count > 0)
            {
                for (int i = 0; i < groupUser.Count; i++)
                {
                    byte[] fileByteArray;
                    if (uploadedPhotoGroupUsers[i] != null)
                    {
                        using (var item = new MemoryStream())
                        {
                            uploadedPhotoGroupUsers[i].CopyTo(item);
                            fileByteArray = item.ToArray(); //2nd change here
                            groupUser[i].photo = fileByteArray;
                        }
                    }

                    if (uploadedPdfGroupUsers[i] != null)
                    {
                        using (var item = new MemoryStream())
                        {
                            uploadedPdfGroupUsers[i].CopyTo(item);
                            fileByteArray = item.ToArray(); //2nd change here
                            groupUser[i].pdf = fileByteArray;
                        }
                    }
                }
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
            }
            else
            {
                _f.Flash(Types.Danger, $"Не добавлены пользователи", dismissable: true);
                return Page();
            }
        }



        public async Task<IActionResult> OnPostAddAsync(string seriesPassport, string numberPassport)
        {
            User.passport = seriesPassport + " " + numberPassport;
            groupUser.Add(User);
            using var response = await client.PostAsJsonAsync($"https://localhost:7108/api/Api/PostKostil", groupUser);
            return Page();
        }

        public async Task<IActionResult> OnPostExcelAsync(IFormFile uploadedExelGroupUsers)
        {
            if (uploadedExelGroupUsers != null)
            {
                string pathExel = "/GroupUsers/" + uploadedExelGroupUsers.FileName;

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathExel, FileMode.Create))
                {
                    await uploadedExelGroupUsers.CopyToAsync(fileStream);
                }

                using (Workbook wb = new Workbook(_appEnvironment.WebRootPath + pathExel))
                {

                    // Получить рабочий лист, используя его индекс
                    Worksheet worksheet = wb.Worksheets[0];

                    int rows = worksheet.Cells.MaxDataRow + 1;

                    for (int i = 1; i < rows; i++)
                    {
                        GroupUsers user = new GroupUsers();
                        user.beginDate = User.beginDate;
                        user.endDate = User.endDate;
                        user.target = User.target;
                        user.division = User.division;
                        user.employee = User.employee;
                        user.firstName = Convert.ToString(worksheet.Cells[i, 1].Value);
                        user.name = Convert.ToString(worksheet.Cells[i, 2].Value);
                        user.lastName = Convert.ToString(worksheet.Cells[i, 3].Value);
                        user.number = Convert.ToString(worksheet.Cells[i, 4].Value);
                        user.email = Convert.ToString(worksheet.Cells[i, 5].Value);
                        user.organization = Convert.ToString(worksheet.Cells[i, 6].Value);
                        user.note = Convert.ToString(worksheet.Cells[i, 7].Value);
                        user.birthday = Convert.ToDateTime(Convert.ToString(worksheet.Cells[i, 8].Value));
                        user.passport = Convert.ToString(worksheet.Cells[i, 9].Value) + " " + Convert.ToString(worksheet.Cells[i, 10].Value);
                        groupUser.Add(user);
                    }

                    using var response = await client.PostAsJsonAsync($"https://localhost:7108/api/Api/PostKostil", groupUser);
                }
            }
            return Page();

        }

        public async Task<IActionResult> OnPostClearAsync()
        {
            return RedirectToPage("Groups");
        }
    }
}
