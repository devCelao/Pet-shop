using System.Security.Claims;

namespace Core.Entities;

/// <summary>
/// Classe para cadastro e update da entidade usuário
/// </summary>
public class UsuarioSistema
{
    public string NomeUsuario { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool? IndExcluido { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}

/// <summary>
/// Classe para login no sistema
/// </summary>
public class UsuarioLogin
{
    public string Username { get; set; }
    public string Password { get; set; }
}

/// <summary>
/// Classe que representa o usuário logado no sistema
/// </summary>
public class UsuarioLogado
{
    public string Username { get; set; }
    public string Email { get; set; }
    public IList<Claim> Permissoes { get; set; }
}