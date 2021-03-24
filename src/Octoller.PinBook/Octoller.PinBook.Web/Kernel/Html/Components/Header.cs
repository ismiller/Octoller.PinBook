using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octoller.PinBook.Web.Data.Model;
using Octoller.PinBook.Web.Kernel.Services;
using Octoller.PinBook.Web.ViewModels;

namespace Octoller.PinBook.Web.Kernel.Html.Components
{
    public class Header : ViewComponent 
    {
        private UserManager<User> UserManager { get; }
        private ProfileManager ProfileManager { get; }

        public Header(
            UserManager<User> userManager,
            ProfileManager profileManager)
        {
            UserManager = userManager;
            ProfileManager = profileManager;
        }

        public IViewComponentResult Invoke()
        {

            HeaderViewModel headerInfo = new HeaderViewModel
            {
                IsAuthenticated = User.Identity.IsAuthenticated
            };

            if (headerInfo.IsAuthenticated)
            {
                var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
                var showName = ProfileManager.FindProfileByUserAsync(user).Result?.Name;

                headerInfo.ShowName = showName ?? User.Identity.Name;
            }

            return View(headerInfo);
        }
    }
}
