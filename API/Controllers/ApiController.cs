using API.Models;
using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using API.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using DocumentFormat.OpenXml.Spreadsheet;

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
            var employees = _db.individ.Where(u => u.passport == user.passport);
            return Ok(employees);
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

            try
            {
                InputSignUp.password = InputSignUp.HashPassword(InputSignUp.password);

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
            _db.Attach(PutIndivid).State = EntityState.Modified;

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
                foreach (var user in groupUser)
                {
                    user.applicationNumber = GetNextApplicationNumber();

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

        #region Костыль
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