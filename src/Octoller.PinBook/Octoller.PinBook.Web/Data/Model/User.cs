using Microsoft.AspNetCore.Identity;

namespace Octoller.PinBook.Web.Data.Model
{
    public class User : IdentityUser
    {
        /// <summary>
        /// Данные о профиле пользователя
        /// </summary>
        public Profile Profile { get; set; }
    }
}
