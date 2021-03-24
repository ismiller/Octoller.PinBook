using System.ComponentModel.DataAnnotations;

namespace Octoller.PinBook.Web.ViewModels.Account
{
    public class LoginViewModel : ExternalProviderViewModel
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Display(Name = "Passsword")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }

        [Display(Name = "IsPersistent")]
        public bool IsPersistent { get; set; } = false;
    }
}
