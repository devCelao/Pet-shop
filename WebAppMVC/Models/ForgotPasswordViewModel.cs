using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models;

public class ForgotPasswordViewModel
{
    [EmailAddress]
    [Required]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    [Required]
    [DisplayName("Usuário")]
    public string Username { get; set; }
}
