using System.ComponentModel.DataAnnotations;

namespace AuthTest.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Login required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password requied")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords have to match")]
        public string ConfirmPassword { get; set; }
    }
}
