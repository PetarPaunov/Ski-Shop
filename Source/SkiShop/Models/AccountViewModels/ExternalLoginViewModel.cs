using System.ComponentModel.DataAnnotations;

namespace SkiShop.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 7)]
        public string PhoneNumber { get; set; }

    }
}
