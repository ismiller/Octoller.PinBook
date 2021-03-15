using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octoller.PinBook.Web.Data.Model;
using Octoller.PinBook.Web.ViewModels;

namespace Octoller.PinBook.Web.Kernel.Html.Components
{
    public class Header : ViewComponent 
    {
        private readonly UserManager<User> userManager;

        public Header(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {

            HeaderViewModel headerInfo = new HeaderViewModel
            {
                IsAuthenticated = User.Identity.IsAuthenticated
            };

            if (headerInfo.IsAuthenticated)
            {
                //string id = this.userManager.FindByNameAsync(User.Identity.Name).Result?.Id;

                headerInfo.ShowName = User.Identity.Name;
            }

            return View(headerInfo);
        }
    }
}
