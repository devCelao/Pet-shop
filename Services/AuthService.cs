using Infrastructure.Data.Usuario;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Services;

public interface IAuthService
{
    Task<IList<string>> Logar(string username, string password);
    Task RealizaLogout();
    Task<List<Claim>?> BuscarPermissoesUsuario(string COD_USUARIO);
}
public class AuthService : BaseService, IAuthService
{
    private readonly IControleAcessoRepository _repository;
    private readonly IAuthenticationService _service;
    private readonly IAspNetUserService _user;
    public AuthService(IControleAcessoRepository repository, IAuthenticationService authenticationService, IAspNetUserService user)
    {
        _repository = repository;
        _service = authenticationService;
        _user = user;
    }

    public async Task<IList<string>> Logar(string username, string password)
    {
        var errosLogin = await VerificarLoginUsuario(username, password);

        if (errosLogin.Any())
        {
            AdicionaErrosProcessamento(errosLogin);

            return Erros;
        }

        await RealizarLogin(username);

        return Erros;
    }


    private async Task<IList<string>> VerificarLoginUsuario(string username, string password)
    {
        var usuario =  await _repository.VerificarLoginUsuario(username, password);

        if(usuario == null)
        {
            AdicionarErroProcessamento("Usuário ou senha incorretos!");
        }
        else if (!usuario.IND_ATIVO)
        {
            AdicionarErroProcessamento("Este cadastro não está ativo! Entre em contato com o suporte ou administrador do sistema");
        }

        return Erros;
    }

    public async Task<List<Claim>?> BuscarPermissoesUsuario(string COD_USUARIO)
    {
        var permissoesUsuario = await _repository.BuscarPermissoesUsuario(COD_USUARIO);
        var retorno = new List<Claim>();

        if (permissoesUsuario.Any())
        {
            foreach(var row in permissoesUsuario)
                retorno.Add(new Claim("role", row.Permissao.COD_PERMISSAO));

            var usuario = permissoesUsuario[0].Usuario;

            retorno.Add(new Claim(ClaimTypes.Name, usuario.COD_USUARIO));
        }


        return retorno;
    }

    private async Task RealizarLogin(string Username)
    {
        var claims = await BuscarPermissoesUsuario(Username);

        var claimsIdentity = new ClaimsIdentity(claims: claims, authenticationType: CookieAuthenticationDefaults.AuthenticationScheme);

        await _service.SignInAsync(
                                    context: _user.HttpContext,
                                    scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                                    principal: new ClaimsPrincipal(claimsIdentity),
                                    properties: SetAuthProperties());

    }

    public async Task RealizaLogout()
    {

        await _service.SignOutAsync(
                                     context: _user.HttpContext,
                                     scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                                     properties: SetAuthProperties()
                                     );
    }

    private AuthenticationProperties SetAuthProperties()
    {
        return new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
            IsPersistent = true
        };
    }
}
