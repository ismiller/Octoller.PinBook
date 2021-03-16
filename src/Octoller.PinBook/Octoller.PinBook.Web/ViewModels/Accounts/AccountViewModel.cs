using System.ComponentModel.DataAnnotations;

namespace Octoller.PinBook.Web.ViewModels
{
    public class AccountViewModel
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Display(Name = "New passsword")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан новый пароль")]
        public string Password { get; set; }

        [Display(Name = "Current password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        public string CurrentPassword { get; set; }

        public bool VkAccount { get; set; }
    }
}
