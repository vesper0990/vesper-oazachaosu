using System.ComponentModel.DataAnnotations;

namespace Oazachaosu.Api.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
