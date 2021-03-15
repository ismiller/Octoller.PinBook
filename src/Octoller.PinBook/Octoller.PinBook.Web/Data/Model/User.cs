using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Octoller.PinBook.Web.Data.Model
{
    public class User : IdentityUser
    {
        /// <summary>
        /// Данные о профиле пользователя
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// Списки книг
        /// </summary>
        public IEnumerable<BookList> BookLists { get; set; }
    }
}
