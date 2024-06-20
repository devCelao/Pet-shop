using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models;

public class ControleAcessoViewModel
{
    [Required]
    [DisplayName("Usuário")]
    public string Username { get; set; }

    [DisplayName("Permissões Usuário")]
    public IList<string> Permissoes { get; set; } = new List<string>();

    [DisplayName("Permissão Cadastro")]
    public string? Permissao { get; set; }

    [DisplayName("Descrição Permissão Cadastro")]
    public string? Txt_Descricao_Permissao { get; set; }
}
