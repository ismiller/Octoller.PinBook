using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octoller.PinBook.Web.Data.Model;
using System.Threading.Tasks;

namespace Octoller.PinBook.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> UserManager { get; }
        private SignInManager<User> SignInManager { get; }

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl)
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register()
        {

            return View();
        }

        [HttpGet]
        public IActionResult ExternalLogin(string returnUrl, string providerName)
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {

            return RedirectToAction("Index", "Home");
        }
    }
}
