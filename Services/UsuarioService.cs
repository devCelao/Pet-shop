using Infrastructure.Data.Usuario;

namespace Services;

public interface IUsuarioService
{
    Task<IDictionary<string, bool>> GetUsuariosStatus();
    Task<IList<string>> Registrar(Usuario usuario);
    Task<IList<string>> AlterarStatusAtivo(string codUsuario, bool indAtivo);
}
public class UsuarioService : BaseService, IUsuarioService
{
    private readonly IControleAcessoRepository _repository;

    public UsuarioService(IControleAcessoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IDictionary<string, bool>> GetUsuariosStatus()
    {
        var restorno = new Dictionary<string, bool>();
        var usuarios = await _repository.BuscarTodos();

        if (usuarios.Any())
        {
            foreach (var usuario in usuarios)
            {
                restorno.Add(usuario.COD_USUARIO, usuario.IND_ATIVO);
            }
        }

        return restorno;
    }

    public async Task<IList<string>> Registrar(Usuario usuario) => await _repository.RegistrarUsuario(usuario);

    public async Task<IList<string>> AlterarStatusAtivo(string codUsuario, bool indAtivo)
    {
        if (string.IsNullOrEmpty(codUsuario))
        {
            AdicionarErroProcessamento("Não foi possível realizar ação! Entre em contato com o suporte..");
            return Erros;
        }

        if (indAtivo)
            Erros = await _repository.DesativarCadastro(codUsuario);
        else
            Erros = await _repository.ReativarCadastro(codUsuario);

        return Erros;
    }
}
