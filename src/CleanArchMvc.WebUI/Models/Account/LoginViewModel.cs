using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.WebUI.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}
