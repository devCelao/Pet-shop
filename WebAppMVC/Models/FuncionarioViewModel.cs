using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models;

public class FuncionarioViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    [Required]
    [StringLength(50)]
    public string Cargo { get; set; }

    [Required]
    public DateTime DataDeContratacao { get; set; } = DateTime.MinValue;

    public DateTime DataAlteracao { get; set; }
    public Guid UsuarioAlteracao { get; set; } = Guid.Empty;

    [Phone]
    [StringLength(17)]
    public string Telefone { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }


    public FuncionarioViewModel RetornaUsuarioTeste(Guid id)
    {
        var funcionario = new FuncionarioViewModel
        {
            Id = id.Equals(Guid.Empty) ? new Guid() : id,
            Nome = "Marcelo Fernandes",
            Cargo = "Administrador do Sistema",
            Telefone = "(21) 97041-6745",
            Email = "dev.celao@gmail.com"
        };

        return funcionario;
    }
}
