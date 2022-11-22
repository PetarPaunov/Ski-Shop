namespace SkiShop.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static SkiShop.Core.Constants.AccountConstants;

    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; } = null!;

    }
}