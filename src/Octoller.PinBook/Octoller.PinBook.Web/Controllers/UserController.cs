using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octoller.PinBook.Web.Data.Model;
using Octoller.PinBook.Web.Kernel.Services;
using Octoller.PinBook.Web.ViewModels.User;
using System.Linq;
using System.Threading.Tasks;

namespace Octoller.PinBook.Web.Controllers
{
    public class UserController : Controller
    {
        private ProfileManager ProfileManager { get; }
        private UserManager<User> UserManager { get; }
        private SignInManager<User> SignInManager { get; }
        private AccountManager AccountManager { get; }

        public UserController(
            ProfileManager profileManager,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            AccountManager accountManager)
        {
            ProfileManager = profileManager;
            UserManager = userManager;
            SignInManager = signInManager;
            AccountManager = accountManager;
        }

        public async Task<IActionResult> Index(string id)
        {

            return View();
        }

        [HttpGet]
        [Authorize(Policy = "Users")]
        public async Task<IActionResult> Profile()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            if (user is not null)
            {
                var profile = await ProfileManager.FindProfileByUserAsync(user);
                if (profile is not null)
                {
                    return View(new ProfileViewModel
                    {
                        Name = profile.Name,
                        About = profile.About,
                        Location = profile.Location,
                        Site = profile.Site
                    });
                }
            }

            return View(new ProfileViewModel());
        }

        [HttpPost]
        [Authorize(Policy = "Users")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel profileModel)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);

                if (user is not null)
                {
                    var profile = new Profile
                    {
                        Name = profileModel.Name,
                        Site = profileModel.Site,
                        Location = profileModel.Location,
                        About = profileModel.About
                    };

                    var updateResult = await ProfileManager.UpdateProfileAsync(user, profile);
                    if (updateResult.Succeeded)
                    {
                        return View(profileModel);
                    }
                    else
                    {
                        foreach(var e in updateResult.Errors)
                        {
                            ModelState.AddModelError("", e.Description);
                        }

                        return View(profile);
                    }
                }
            }

            return View(profileModel);
        }

        [HttpGet]
        [Authorize(Policy = "Users")]
        public async Task<IActionResult> Account()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user is not null)
            {
                var profile = await ProfileManager.FindProfileByUserAsync(user); 
                return View(new AccountViewModel
                {
                    Name = profile?.Name ?? user.UserName,
                    Email = user.Email
                });
            }

            return View(new AccountViewModel());
        }

        [HttpPost]
        [Authorize(Policy = "Users")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Account(AccountViewModel accountModel)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                if (user is not null)
                {
                    if (await UserManager.CheckPasswordAsync(user, accountModel.CurrentPassword))
                    {
                        var profile = await ProfileManager.FindProfileByUserAsync(user);
                        var updateResult = await AccountManager.UpdateAccount(user.Id, accountModel.Email, accountModel.Password);

                        if (updateResult.Succeeded)
                        {
                            var newUser = await UserManager.FindByNameAsync(User.Identity.Name);

                            return View(new AccountViewModel
                            {
                                Name = profile?.Name ?? user.UserName,
                                Email = newUser.Email
                            });
                        } else
                        {
                            foreach (var e in updateResult.Errors)
                            {
                                ModelState.AddModelError("", e.Description);
                            }

                            return View(new AccountViewModel
                            {
                                Name = profile?.Name ?? user.UserName,
                                Email = user.Email
                            });
                        }
                        
                    } 
                    else
                    {
                        ModelState.AddModelError("", "Неверно указан текущий пароль.");
                    }
                } 
                else
                {
                    ModelState.AddModelError("", "Пользователь не найден.");
                }
            }

            return View(accountModel);
        }

        [HttpGet]
        [Authorize(Policy = "Users")]
        public async Task<IActionResult> Networks()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            var profile = await ProfileManager.FindProfileByUserAsync(user);
            
            return View(new NetworksViewModel
            {
                Name = profile?.Name ?? user.UserName,
                VKontakte = await IsExternalAuthSchem(user, "VK"),
                Yandex = await IsExternalAuthSchem(user, "Yandex")
            });
        }

        [HttpPost]
        [Authorize(Policy = "Users")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Networks(string providerName)
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            var isConnect = await IsExternalAuthSchem(user, providerName);

            if (!isConnect)
            {
                var returnUrl = Url.Action("Networks", "User");
                var redirectUrl = Url.Action(nameof(LinkExternalAccountCallback), "User", new { returnUrl });
                var properties = SignInManager.ConfigureExternalAuthenticationProperties("VK", redirectUrl);

                return Challenge(properties, "VK");
            }
            else
            {
                var profile = await ProfileManager.FindProfileByUserAsync(user);
                var info = (await UserManager.GetLoginsAsync(user))
                    .Where(ul => ul.LoginProvider == providerName)
                    .FirstOrDefault();

                var removeResult = await UserManager.RemoveLoginAsync(user, info.LoginProvider, info.ProviderKey); 
                if (!removeResult.Succeeded)
                {
                    foreach (var e in removeResult.Errors)
                    {
                        ModelState.AddModelError("", e.Description);
                    }
                }

                return View(new NetworksViewModel
                {
                    Name = profile?.Name ?? user.UserName,
                    VKontakte = await IsExternalAuthSchem(user, "VK"),
                    Yandex = await IsExternalAuthSchem(user, "Yandex")
                });
            }
        }

        [HttpGet]
        [Authorize(Policy = "Users")]
        public async Task<IActionResult> LinkExternalAccountCallback(string returnUrl)
        {
            var info = await SignInManager.GetExternalLoginInfoAsync();
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            var addLoginResult = await UserManager.AddLoginAsync(user, info);
            if (addLoginResult.Succeeded)
            {
                await SignInManager.SignOutAsync();
                await SignInManager.SignInAsync(user, true);
            }

            return Redirect(returnUrl);
        }

        private async Task<bool> IsExternalAuthSchem(User user, string schemeName) =>
            (await UserManager.GetLoginsAsync(user)).Any(ul => ul.LoginProvider == schemeName);
    }
}
