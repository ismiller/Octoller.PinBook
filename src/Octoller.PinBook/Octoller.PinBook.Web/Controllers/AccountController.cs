using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octoller.PinBook.Web.Data.Model;
using Octoller.PinBook.Web.ViewModels;
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
                return RedirectToAction(returnUrl);
            }

            var providers = await SignInManager.GetExternalAuthenticationSchemesAsync();

            return View(new LoginViewModel { 
                ReturnUrl = returnUrl ?? Url.Action("Index", "Home"),
                Providers = providers
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(loginModel.ReturnUrl);
            }

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(loginModel.Email);

                if (user is null)
                {
                    var signInResult = await SignInManager.PasswordSignInAsync(
                       user: user, 
                       password: loginModel.Password, 
                       isPersistent: loginModel.IsPersistent, 
                       lockoutOnFailure: false);

                    if (signInResult.Succeeded)
                    {
                        return Redirect(loginModel.ReturnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }

            ModelState.AddModelError(string.Empty, "Неверно указан пароль или логин");

            loginModel.Password = string.Empty;

            return View(loginModel);
        }

        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }

            var providers = await SignInManager.GetExternalAuthenticationSchemesAsync();

            return View(new RegisterViewModel
            {
                ReturnUrl = returnUrl ?? Url.Action("Index", "Home"),
                Providers = providers
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(registerModel.ReturnUrl);
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = registerModel.Email,
                    Email = registerModel.Email
                };

                var resultCreate = await UserManager.CreateAsync(user, registerModel.Password);

                if (resultCreate.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false);

                    return Redirect(registerModel.ReturnUrl);
                } 
                else
                {
                    foreach (var error in resultCreate.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(registerModel);
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
