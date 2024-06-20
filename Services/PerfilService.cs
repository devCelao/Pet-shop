
using Infrastructure.Data.Usuario;
using Services.Models;

namespace Services;
public interface IPerfilService
{
    Task<IList<PerfilModel>> GetUsuariosSistema();
    Task<IList<string>> CadastrarPerfil(PerfilModel perfil);
    Task<IList<string>> Deletar(string COD_USUARIO);
    Task<IList<string>> Desativar(string COD_USUARIO);
    Task<IList<string>> Ativar(string COD_USUARIO);
    Task<Usuario> BuscarUsuario(string COD_USUARIO);
    Task<IList<string>> AtualizarPerfil(PerfilModel perfil);
}
public class PerfilService : BaseService,IPerfilService
{
    private readonly IControleAcessoRepository _repository;
    private readonly IAspNetUserService _user;

    public PerfilService(IControleAcessoRepository repository, 
                         IAspNetUserService user)
    {
        _repository = repository;
        _user = user;
    }

    public async Task<IList<PerfilModel>> GetUsuariosSistema() => new PerfilModel().MapListToPerfilModel(await _repository.BuscarTodos());

    public async Task<IList<string>> CadastrarPerfil(PerfilModel perfil) => await _repository.RegistrarUsuario(perfil.MapToUsuario());

    public async Task<IList<string>> Deletar(string COD_USUARIO) => await _repository.DeletarPermanete(COD_USUARIO);

    public async Task<Usuario> BuscarUsuario(string COD_USUARIO) => await _repository.GetByCodUsuario(COD_USUARIO);

    public async Task<IList<string>> AtualizarPerfil(PerfilModel perfil) => await _repository.AtualiarUsuario(perfil.MapToUsuario());

    public async Task<IList<string>> Ativar(string COD_USUARIO) => await _repository.ReativarCadastro(COD_USUARIO);

    public async Task<IList<string>> Desativar(string COD_USUARIO)
    {
        if (string.Equals(COD_USUARIO, _user.UserName))
        {
            AdicionarErroProcessamento("Ação não permitida! Você não pode desativar seu próprio cadastro.");

            return Erros;
        }

        return await _repository.DesativarCadastro(COD_USUARIO);
    }
}
