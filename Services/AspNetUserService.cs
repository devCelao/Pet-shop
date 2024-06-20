using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Services;

public interface IAspNetUserService
{
    string UserName { get; }
    bool IsAuthenticated { get; }
    HttpContext HttpContext { get; }
    IEnumerable<Claim> Permissoes { get; }
    bool PossuiChave(string chave);
    bool isAdmin { get; }
}

public class AspNetUserService : BaseService, IAspNetUserService
{
    private readonly IHttpContextAccessor _accessor;

    public AspNetUserService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string UserName => _accessor.HttpContext.User.Identity.Name;
    public bool IsAuthenticated => _accessor.HttpContext.User.Identity.IsAuthenticated;
    public HttpContext HttpContext => _accessor.HttpContext;
    public bool isAdmin => PossuiChave("ADM");


    public IEnumerable<Claim> Permissoes => _accessor.HttpContext.User.Claims;


    public bool PossuiChave(string chave)
    {
        return Permissoes.Where(x => x.Value == chave).Any();
    }
}
