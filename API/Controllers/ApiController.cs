using API.Models;
using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using API.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2019.Presentation;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        #region Конструктор ебыря
        private readonly DBContext _db;
        public ApiController(DBContext db)
        {
            _db = db;

        }
        #endregion




        #region Вывести всех пользователей
        [HttpGet]
        public IActionResult GetUsers()
        {
            var user = _db.user;
            return Ok(user);
        }
        #endregion

        #region Вывести все цели
        [HttpGet("GetTargets")]
        public IActionResult GetTargets()
        {
            var targets = _db.target;
            return Ok(targets);
        }
        #endregion

        #region Вывести все подразделения
        [HttpGet("GetDivisions")]
        public IActionResult GetDivisions()
        {
            var divisions = _db.division;
            return Ok(divisions);
        }
        #endregion

        #region Вывести всех сотрудников
        [HttpGet("GetEmployees")]
        public IActionResult GetEmployees()
        {
            var employees = _db.employees;
            return Ok(employees);
        }
        #endregion

        #region Вывести все индивидуальные заявки по пользователю
        [HttpGet("GetIndivid/{id}")]
        public IActionResult GetIndivid(int id)
        {
            var user = GetById(id);
            var individs = _db.individ.Where(u => u.passport == user.passport);
            return Ok(individs);
        }
        #endregion

        #region Вывести все груповые заявки по пользователю
        [HttpGet("GetGroups/{id}")]
        public IActionResult GetGroups(int id)
        {
            var user = GetById(id);
            var groups = _db.groupUsers.Where(u => u.passport == user.passport);
            return Ok(groups);
        }
        #endregion

        #region Вывести все групповые заявки
        [HttpGet("GetGroups")]
        public IActionResult GetGroups()
        {
            var groups = _db.groupUsers;
            return Ok(groups);
        }
        #endregion

        #region Регистрация

        [HttpPost("SignUp")]
        public IActionResult SignUp([FromBody] User InputSignUp)
        {
            if (FindUserForPassport(InputSignUp.passport) == null)
            {
                SignUpIfFind(InputSignUp);
                return Ok();
            }
            else
            {
                try
                {
                    InputSignUp.password = InputSignUp.HashPassword(InputSignUp.password);
                    InputSignUp.blFirst = true;
                    InputSignUp.blLast = true;

                    _db.user.Add(InputSignUp);
                    _db.SaveChanges();

                    var user = _db.user;
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("SignUpIfFind")]
        public IActionResult SignUpIfFind([FromBody] User InputSignUp)
        {
            _db.user.Attach(InputSignUp).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Авторизация
        [HttpPost("Auth")]
        public IActionResult Auth([FromBody] User InputAuth)
        {
            try
            {
                InputAuth.password = InputAuth.HashPassword(InputAuth.password);

                User user = _db.user.Where(u => u.login == InputAuth.login && u.password == InputAuth.password).FirstOrDefault<User>();
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Отправка индивидуальной заявки

        [HttpPost("CreateIndivid")]
        public IActionResult CreateIndivid([FromBody] Individ Individ)
        {
            bool BlackList = true;
            if (!CheckBlackList(Individ.passport))
            {
                BlackList = false;
            }
            Individ.blackList = BlackList;
            try
            {
                _db.individ.Add(Individ);
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Одобрение индивидуальной заявки
        [HttpPut("ApprovalIndivid")]
        public IActionResult ApprovalIndivid([FromBody] Individ PutIndivid)
        {
            _db.individ.Attach(PutIndivid).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(PutIndivid.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        #endregion

        #region Хуй пойми что
        private bool UserExists(int id)
        {
            return _db.individ.Any(e => e.id == id);
        }
        #endregion

        #region Поиск нового номера группы
        [HttpGet("GetNextApplicationNumber")]
        public int GetNextApplicationNumber()
        {
            var NextApplicationNumber = _db.groupUsers.Count() == 0 ? 1 : _db.groupUsers.Max(u => u.applicationNumber) + 1;
            return (int)NextApplicationNumber;
        }
        #endregion

        #region Отправка групповой заявки
        [HttpPost("CreateGroups")]
        public IActionResult CreateGroups([FromBody] List<GroupUsers> groupUser)
        {
            if (groupUser != null)
            {
                bool BlackList = true;
                foreach (var user in groupUser)
                {
                    if (!CheckBlackList(user.passport))
                    {
                        BlackList = false;
                    }
                }
                foreach (var user in groupUser)
                {
                    user.applicationNumber = GetNextApplicationNumber();
                    user.blackList = BlackList;
                    _db.groupUsers.Add(user);
                }
                try
                {
                    _db.SaveChanges();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok(groupUser);

        }
        #endregion

        #region Поиск по паспорту
        [HttpGet("FindUserForPassport")]
        public User FindUserForPassport(string passport)
        {
            User user = _db.user.Where(u => u.passport == passport).FirstOrDefault();
            return user;
        }
        #endregion

        #region Проверка на черный список
        [HttpPost("CheckBlackList")]
        public bool CheckBlackList(string Passport)
        {
            bool result = true;
            User user = FindUserForPassport(Passport);
            if (user == null)
            {
                User User = new User();
                try
                {
                    User.passport = Passport;
                    User.blFirst = true;
                    User.blLast = true;

                    _db.user.Add(User);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                }
                result = true;
                return result;
            }
            else
            {
                if (user.blFirst == false && user.blLast == false)
                {
                    result = false;
                    return result;
                }
                else
                {
                    result = true;
                    return result;
                }
            }
        }
        #endregion

        #region Костыль
        static int Option = 1;
        public List<string> Options { get; set; } = new List<string> { "Личная", "Групповая" };
        [HttpPost("PostOption")]
        public IActionResult PostOption(int option)
        {
            Option = option;

            return Ok();
        }
        [HttpGet("GetOption")]
        public IActionResult GetOption()
        {
            return Ok(Option);
        }
        [HttpGet("GetOptions")]
        public IActionResult GetOptions()
        {
            return Ok(Options);
        }
        static List<GroupUsers> Kostil { get; set; } = new List<GroupUsers>();
        [HttpPost("PostKostil")]
        public IActionResult PostKostil(List<GroupUsers> kostil)
        {
            Kostil = kostil;

            return Ok();
        }

        [HttpGet("GetKostil")]
        public IActionResult GetKostil()
        {
            return Ok(Kostil);
        }
        #endregion

        #region Добавление подразделения

        [HttpPost("Division")]
        public IActionResult Division([FromBody] Division CreateDivision)
        {

            try
            {
                _db.division.Add(CreateDivision);
                _db.SaveChanges();

                var Divisions = _db.division;
                return Ok(Divisions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.HelpLink);
            }

        }
        #endregion

        #region Добавление цели

        [HttpPost("Target")]
        public IActionResult Target([FromBody] Target CreateTarget)
        {

            try
            {
                _db.target.Add(CreateTarget);
                _db.SaveChanges();

                var Targets = _db.target;
                return Ok(Targets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Добавление сотрудника

        [HttpPost("Employees")]
        public IActionResult Employees([FromBody] Employees CreateEmployees)
        {

            try
            {
                _db.employees.Add(CreateEmployees);
                _db.SaveChanges();

                var Employee = _db.employees;
                return Ok(Employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Найти пользователя по id
        [NonAction]
        public User GetById(int id)
        {
            var users = _db.user.Find(id);
            if (users == null) { throw new KeyNotFoundException("User Not Found"); }
            return users;
        }
        #endregion

        #region Удалить по id((
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = GetById(id);

            _db.user.Remove(user);
            _db.SaveChanges();

            return Ok(new { Message = "User Deleted" });

        }
        #endregion
    }
}