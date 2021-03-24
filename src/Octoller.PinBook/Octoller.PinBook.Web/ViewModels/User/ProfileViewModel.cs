using Octoller.PinBook.Web.ViewModels.Base;

namespace Octoller.PinBook.Web.ViewModels.User
{
    public class ProfileViewModel : NamedBase
    {
        /// <summary>
        /// Сайт пользователя
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Местоположение пользователя
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Дополнительная информация о пользователе
        /// </summary>
        public string About { get; set; }
    }
}
