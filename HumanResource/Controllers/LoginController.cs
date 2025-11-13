using HumanResource.Interface;
using HumanResource.Interface.Login;
using HumanResource.Models;
using HumanResource.Models.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace HumanResource.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILoginRepository _loginRepository;
        public LoginController(IConfiguration config, ILoginRepository loginRepository)
        {
            _config = config;
            _loginRepository = loginRepository;
          
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserModel _user)
        {
            UserModel user = await _loginRepository.GetEmployeeById(_user);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return View(_user);
            }
            else
            {
                HttpContext.Session.SetString("UserDetails", JsonConvert.SerializeObject(user));
                return RedirectToAction("Index", "Employee");
            }


        }
    }
}
