using Microsoft.AspNetCore.Identity;

namespace Octoller.PinBook.Web.Data.Model
{
    public class User : IdentityUser
    {
        /// <summary>
        /// Данные о аккаунте пользователя
        /// </summary>
        public Account Account { get; set; }
    }
}
