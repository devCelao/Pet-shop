
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Data.Usuario;

/// <summary>
/// Usuário no sistema
/// </summary>
public class Usuario
{
    #region ENTITY
    [Key]
    public Guid ID { get; set; }
    public string COD_USUARIO { get; set; }
    public string NOM_USUARIO { get; set; }
    public string? TXT_SENHA { get; set; }
    public string? TXT_EMAIL { get; set; }
    public bool IND_EMAIL_CONFIRMADO { get; set; }
    public bool IND_ATIVO { get; set; }
    public DateTime DT_REGISTRO { get; set; }
    public DateTime DT_ALTERACAO { get; set; }
    public ICollection<PermissaoUsuario> Permissoes { get; set; }
    #endregion


    #region Construtores

    // EF
    public Usuario()
    {
        Permissoes = new HashSet<PermissaoUsuario>();
    }
    #endregion

    

    #region Métodos
    public string Criptografy(string senha)
    {
        using(var hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
             return builder.ToString();
        }
    }

    public void CriptografaSenha(string senhaIn)
    {
        this.TXT_SENHA = Criptografy(senhaIn);
    }

    public bool ComparaSenha(string senha)
    {
        return string.Equals(this.TXT_SENHA, Criptografy(senha));
    }

    public void ConfirmaEmail()
    {
        this.IND_EMAIL_CONFIRMADO = true;
    }

    public void DesativaCadastro()
    {
        IND_ATIVO = false;
        DT_ALTERACAO = DateTime.Now;
    }

    public void AtivarCadastro()
    {
        IND_ATIVO = true;
        DT_ALTERACAO = DateTime.Now;
    }

    public void AtualizarDadosCadastro(Usuario data)
    {
        this.NOM_USUARIO = data.NOM_USUARIO ?? this.NOM_USUARIO;
        this.TXT_EMAIL = data.TXT_EMAIL ?? this.TXT_EMAIL;
        this.DT_ALTERACAO = DateTime.Now;
    }
    #endregion
}


/// <summary>
/// Permissões do sistema
/// </summary>

public class Permissao
{
    #region ENTITY
    [Key]
    public Guid ID { get; set; }
    public string COD_PERMISSAO { get; set; }
    public string TXT_DESCRICAO { get; set; }

    public ICollection<PermissaoUsuario> Permissoes { get; set; }

    public Permissao()
    {
        Permissoes = new HashSet<PermissaoUsuario>();
    }
    #endregion

}


/// <summary>
/// Relacionamento entre Usuario e Permissao
/// </summary>

public class PermissaoUsuario
{
    #region ENTITY
    public Guid ID { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid PermissaoId { get; set; }

    // Propriedades de navegação
    public Usuario Usuario { get; set; }
    public Permissao Permissao { get; set; }

    public PermissaoUsuario() { }

    #endregion

}