using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models;

public class AccountViewModel
	{
    [Required]
    [DisplayName("Usuário")]
    public string Username { get; set; }

    [Required]
    [DisplayName("Senha")]
    public string Password { get; set; }

}
