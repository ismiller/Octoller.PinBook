using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octoller.PinBook.Web.Data.Model;
using Octoller.PinBook.Web.Kernel.Services;
using Octoller.PinBook.Web.ViewModels.Account;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Octoller.PinBook.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> UserManager { get; }
        private SignInManager<User> SignInManager { get; }
        private ProfileManager ProfileManager { get; }
        private AccountManager AccountManager { get; }

        private IActionResult Home { get => RedirectToAction("Index", "Home"); }

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ProfileManager profileManager,
            AccountManager accountManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            ProfileManager = profileManager;
            AccountManager = accountManager;
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Home;
            }

            var providers = await SignInManager.GetExternalAuthenticationSchemesAsync();

            return View(new LoginViewModel 
            {
                Providers = providers
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Home;
            }

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(loginModel.Email);

                if (user is not null)
                {
                    var signInResult = await SignInManager.PasswordSignInAsync(
                       user: user, 
                       password: loginModel.Password, 
                       isPersistent: loginModel.IsPersistent, 
                       lockoutOnFailure: false);

                    if (signInResult.Succeeded)
                    {
                        await ProfileManager.CreateProfileAsync(user, new Profile());

                        return Home;
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }

            ModelState.AddModelError(string.Empty, "Неверно указан пароль или логин");

            loginModel.Password = string.Empty;

            loginModel.Providers = await SignInManager.GetExternalAuthenticationSchemesAsync();

            return View(loginModel);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Home;
            }

            var providers = await SignInManager.GetExternalAuthenticationSchemesAsync();

            return View(new RegisterViewModel
            {
                Providers = providers
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Home;
            }

            if (ModelState.IsValid)
            {
                var resultCreate = await AccountManager.CreateAccount(
                    email: registerModel.Email, password: registerModel.Password);

                if (resultCreate.Succeeded)
                {
                    var user = await UserManager.FindByEmailAsync(registerModel.Email);

                    await ProfileManager.CreateProfileAsync(user, new Profile());
                    await SignInManager.SignInAsync(user, true);

                    return Home;
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
        public IActionResult ExternalLogin(string providerName) =>
            ExternalChallenge(nameof(ExternalLoginCallback), providerName);

        [HttpGet]
        public IActionResult ExternalRegister(string providerName) =>
            ExternalChallenge(nameof(ExternalRegisterCallback), providerName);

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var loginInfo = await SignInManager.GetExternalLoginInfoAsync();

            if (loginInfo is not null)
            {
                var result = await IsSuccessExternalSignIn(loginInfo);
                if (result)
                {
                    return Home;
                }
            }

            return RedirectToAction(nameof(ExternalRegisterCallback), "Account", Home);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalRegisterCallback(string returnUrl)
        {
            var loginInfo = await SignInManager.GetExternalLoginInfoAsync();

            if (loginInfo is null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (await IsSuccessExternalSignIn(loginInfo))
            {
                return Redirect(returnUrl);
            } 
            else
            {
                var userEmail = loginInfo.Principal.Claims
                    .Where(c => c.Type == ClaimTypes.Email)
                    .FirstOrDefault()
                    .Value;

                var createAccount = await AccountManager.CreateAccount(userEmail);
                if (createAccount.Succeeded)
                {
                    var user = await UserManager.FindByEmailAsync(userEmail);

                    await ProfileManager.CreateProfileAsync(user);

                    var addLoginInfoResult = await UserManager.AddLoginAsync(user, loginInfo);
                    if (addLoginInfoResult.Succeeded)
                    {
                        await SignInManager.SignOutAsync();
                        await SignInManager.SignInAsync(user, true);

                        return Redirect(returnUrl);
                    }
                }
            }

            return RedirectToAction("Login", new LoginViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return Home;
        }

        private async Task<bool> IsSuccessExternalSignIn(ExternalLoginInfo loginInfo)
        {
            var result = await SignInManager.ExternalLoginSignInAsync(
               loginProvider: loginInfo.LoginProvider,
               providerKey: loginInfo.ProviderKey,
               isPersistent: true,
               bypassTwoFactor: false);

            return result.Succeeded;
        }

        private IActionResult ExternalChallenge(string methodName, string providerName)
        {
            string returnUrl = Url.Action("Index", "Home");
            var redirectUrl = Url.Action(methodName, "Account", new { returnUrl });
            var properties = SignInManager.ConfigureExternalAuthenticationProperties(providerName, redirectUrl);

            return Challenge(properties, providerName);
        }
    }
}
