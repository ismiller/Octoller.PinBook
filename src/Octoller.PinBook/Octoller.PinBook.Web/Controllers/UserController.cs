using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octoller.PinBook.Web.Data.Model;
using Octoller.PinBook.Web.Kernel.Services;
using Octoller.PinBook.Web.ViewModels.Profiles;
using System.Threading.Tasks;

namespace Octoller.PinBook.Web.Controllers
{
    public class UserController : Controller
    {
        private ProfileManager ProfileManager { get; }
        private UserManager<User> UserManager { get; }

        public UserController(
            ProfileManager profileManager,
            UserManager<User> userManager)
        {
            ProfileManager = profileManager;
            UserManager = userManager;
        }

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
                        Site = profile.Site,
                        Avatar = profile.Avatar ?? null
                    });
                }
            }

            return View(new ProfileViewModel());
        }

        [Authorize(Policy = "Users")]
        public async Task<IActionResult> Account()
        {

            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            if (user is not null)
            {

            }

            return View();
        }
    }
}
