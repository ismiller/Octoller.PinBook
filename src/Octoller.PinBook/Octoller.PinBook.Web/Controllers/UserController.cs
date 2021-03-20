using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octoller.PinBook.Web.Data.Model;
using Octoller.PinBook.Web.Kernel.Services;
using Octoller.PinBook.Web.ViewModels.Profiles;
using System.Linq;
using System.Threading.Tasks;

namespace Octoller.PinBook.Web.Controllers
{
    public class UserController : Controller
    {
        private ProfileManager ProfileManager { get; }
        private UserManager<User> UserManager { get; }
        private SignInManager<User> SignInManager { get; }

        public UserController(
            ProfileManager profileManager,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            ProfileManager = profileManager;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [Authorize(Policy = "Users")]
        [HttpGet]
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
                        Site = profile.Site,
                        Avatar = profile.Avatar ?? null
                    });
                }
            }

            return View(new ProfileViewModel());
        }

        [HttpPost]
        [Authorize(Policy = "Users")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel profileModel)
        {
            if (profileModel is not null)
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
                            Site = profile.Site,
                            Avatar = profile.Avatar ?? null
                        });
                    }
                }
            }

            return View(new ProfileViewModel());
        }

        [Authorize(Policy = "Users")]
        [HttpPost]
        public async Task<IActionResult> Account()
        {

            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            if (user is not null)
            {
                var profile = await ProfileManager.FindProfileByUserAsync(user); 
                if (profile is not null)
                {
                    var vk = (await SignInManager.GetExternalAuthenticationSchemesAsync())
                        .Any(s => s.Name == "VKontakte");

                    return View(new AccountViewModel
                    {
                        Name = profile.Name,
                        Email = user.Email,
                        VkAccount = vk
                    });
                }
            }

            return View(new AccountViewModel());
        }
    }
}
