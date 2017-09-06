using System.ComponentModel.DataAnnotations;

namespace Oazachaosu.Models {
  public class ExternalLoginConfirmationViewModel {
    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }
  }

  public class ManageUserViewModel {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Obecne hasło")]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Pole musi mieć conajmnie {2} znaki.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Nowe hasło")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Powtórz nowe hasło")]
    [Compare("NewPassword", ErrorMessage = "Nowe hasło i powtórzenie różnią się.")]
    public string ConfirmPassword { get; set; }
  }

  public class LoginViewModel {
    [Required]
    [Display(Name = "Nazwa użytkownika")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Hasło")]
    public string Password { get; set; }

    [Display(Name = "Zapamiętaj mnie")]
    public bool RememberMe { get; set; }
  }

  public class RegisterViewModel {
    [Required]
    [Display(Name = "Nazwa użytkownika")]
    [StringLength(32, ErrorMessage = "Pole musi mieć conajmnie {2} znaki.", MinimumLength = 4)]
    public string UserName { get; set; }

    [Required]
    [StringLength(32, ErrorMessage = "Pole musi mieć conajmnie {2} znaki.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Hasło")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Powtórz hasło")]
    [Compare("Password", ErrorMessage = "Hasło i powtórzenie hasło różni się.")]
    public string ConfirmPassword { get; set; }
  }
}
