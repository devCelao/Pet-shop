
using Infrastructure.Data.Usuario;

namespace Services;

public interface IControleAcessoService
{
    Task<bool> IsAdmin();
    Task<Usuario?> GetUsuario(string codUsuario);
    Task<IList<string>> AdicionarPermissao(string codUsuario, Permissao role);
    Task<IList<string>> RemoverPermissao(string codUsuario, string codPermissao);
    Task<IList<string>> BuscarPermissoes(string codUsuario);
}
public class ControleAcessoService : BaseService, IControleAcessoService
{
    private readonly IControleAcessoRepository _repository;
    private readonly IAspNetUserService _user;
    public ControleAcessoService(IControleAcessoRepository repository, IAspNetUserService user)
    {
        _repository = repository;
        _user = user;
    }

    public async Task<bool> IsAdmin() => _user.PossuiChave("ADM");

    public async Task<Usuario?> GetUsuario(string codUsuario) => await _repository.GetByCodUsuario(codUsuario);

    public async Task<IList<string>> AdicionarPermissao(string codUsuario, Permissao role) => await _repository.AdicionarPermissaoUsuario(codUsuario, role);

    public async Task<IList<string>> RemoverPermissao(string codUsuario, string codPermissao) => await _repository.RemovePermissaoUsuario(codUsuario, codPermissao);

    public async Task<IList<string>> BuscarPermissoes(string codUsuario)
    {
        var permissoes = await _repository.BuscarPermissoesUsuario(codUsuario);

        return permissoes.Any() ? permissoes.Select(x => x.Permissao.COD_PERMISSAO).ToList() : new List<string>();
    }
}
