using System.ComponentModel.DataAnnotations;

namespace Octoller.PinBook.Web.ViewModels.Account
{
    public class RegisterViewModel : ExternalProviderViewModel
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Display(Name = "Passsword")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
