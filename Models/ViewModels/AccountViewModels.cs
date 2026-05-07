using System.ComponentModel.DataAnnotations;

namespace MadarfigyeloWeb.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Az email cím megadása kötelező")]
        [EmailAddress(ErrorMessage = "Érvénytelen email cím")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A jelszó megadása kötelező")]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Emlékezz rám")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Az email cím megadása kötelező")]
        [EmailAddress(ErrorMessage = "Érvénytelen email cím")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A jelszó megadása kötelező")]
        [StringLength(100, ErrorMessage = "A {0} legalább {2} karakter hosszú kell legyen.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Jelszó megerősítése")]
        [Compare("Password", ErrorMessage = "A jelszó és a megerősítő jelszó nem egyezik.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "Keresztnév")]
        public string? FirstName { get; set; }

        [Display(Name = "Vezetéknév")]
        public string? LastName { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Az email cím megadása kötelező")]
        [EmailAddress(ErrorMessage = "Érvénytelen email cím")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Az email cím megadása kötelező")]
        [EmailAddress(ErrorMessage = "Érvénytelen email cím")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "A jelszó megadása kötelező")]
        [StringLength(100, ErrorMessage = "A {0} legalább {2} karakter hosszú kell legyen.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Új jelszó")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Új jelszó megerősítése")]
        [Compare("Password", ErrorMessage = "A jelszó és a megerősítő jelszó nem egyezik.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
