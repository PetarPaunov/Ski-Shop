using System.ComponentModel.DataAnnotations;

namespace SkiShop.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsPersistent { get; set; } = false;
    }
}
