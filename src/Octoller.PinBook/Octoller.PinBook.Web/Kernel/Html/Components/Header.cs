using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octoller.PinBook.Web.Data.Model;
using Octoller.PinBook.Web.Kernel.Services;
using Octoller.PinBook.Web.ViewModels;

namespace Octoller.PinBook.Web.Kernel.Html.Components
{
    public class Header : ViewComponent 
    {
        private readonly UserManager<User> userManager;
        private readonly VkontakteApiService vkontakteApi;

        public Header(UserManager<User> userManager, VkontakteApiService vkontakteApi)
        {

            this.userManager = userManager;
            this.vkontakteApi = vkontakteApi;
        }

        public IViewComponentResult Invoke()
        {

            HeaderViewModel headerInfo = new HeaderViewModel
            {
                IsAuthenticated = User.Identity.IsAuthenticated
            };

            if (headerInfo.IsAuthenticated)
            {

                string id = this.userManager.FindByNameAsync(User.Identity.Name).Result?.Id;

                headerInfo.ShowName = vkontakteApi
                    .FindAccounByUserIdAsync(id).Result?.Name
                    ?? User.Identity.Name;
            }

            return View(headerInfo);
        }
    }
}
