using System.ComponentModel.DataAnnotations;


namespace AuthTest.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password requied")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
