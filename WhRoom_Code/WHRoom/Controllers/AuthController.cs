using Microsoft.AspNetCore.Mvc;
using WHRoom.DataAccessLayer;
using WHRoom.Models;
using WHRoom.Models.Auth;

namespace WHRoom.Controllers
{
    public class AuthController : Controller
    {
        public readonly IAuthDataAccess _authDataAccess;
        public AuthController(IAuthDataAccess authDataAccess)
        {
            _authDataAccess = authDataAccess;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(Signup request)
        {
            Response response = new Response();
            try
            {
                response = await _authDataAccess.Signup(request);
                request.result = response.res;
            }
            catch
            {

            }
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login request)
        {
            Response response = new Response();
            try
            {
                response = await _authDataAccess.Login(request);
                request.result = response.res;
            }
            catch
            {

            }
            return View(request.result);
        }
        [HttpPost]
        public async Task<IActionResult> LoginwithOtp(Login request)
        {
            Response response = new Response();
            try
            {
                response = await _authDataAccess.LoginwithOtp(request);
                request.OTPres = response.res;
            }
            catch
            {

            }
            return View(request.OTPres);
        }
    }
}
