using Microsoft.AspNetCore.Identity;
using Octoller.PinBook.Web.Data.Model.Abstraction;

namespace Octoller.PinBook.Web.Data.Model
{
    public class User : IdentityUser, IIdEntity
    {
        /// <summary>
        /// Данные о профиле пользователя
        /// </summary>
        public Profile Profile { get; set; }
    }
}
