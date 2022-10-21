using System.ComponentModel.DataAnnotations;

namespace SkiShop.Models.AccountViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
