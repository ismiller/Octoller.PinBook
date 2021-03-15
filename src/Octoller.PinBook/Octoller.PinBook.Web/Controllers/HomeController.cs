using Microsoft.AspNetCore.Mvc;
using Octoller.PinBook.Web.ViewModels;

namespace Octoller.PinBook.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new HeaderViewModel { 
                IsAuthenticated = User.Identity.IsAuthenticated
            });
        }
    }
}
