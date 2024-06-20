using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Data.Usuario;

public interface IControleAcessoRepository
{
    /* Usuario */
    Task<IList<string>> RegistrarUsuario(Usuario usuario);
    Task<Usuario?> GetByCodUsuario(string COD_USUARIO);
    Task<IList<Usuario>> BuscarTodos();
    Task<IList<string>> AtualiarUsuario(Usuario usuario);

    Task<IList<string>> DesativarCadastro(string COD_USUARIO);
    Task<IList<string>> ReativarCadastro(string COD_USUARIO);
    Task<IList<string>> SetPassword(string COD_USUARIO, string TXT_SENHA);
    Task<IList<string>> DeletarPermanete(string COD_USUARIO);

    /* Permissoes Usuario */

    Task<IList<string>> AdicionarPermissaoUsuario(string COD_USUARIO, Permissao role);
    Task<List<PermissaoUsuario>?> BuscarPermissoesUsuario(string COD_USUARIO);
    Task<IList<string>> RemovePermissaoUsuario(string COD_USUARIO, string COD_PERMISSAO);


    /* Funções de login */
    Task<Usuario?> VerificarLoginUsuario(string COD_USUARIO, string TXT_SENHA);

}
public class ControleAcessoRepository : BaseRepository, IControleAcessoRepository
{
    private readonly ControleAcessoContext _context;
    
    public ControleAcessoRepository(ControleAcessoContext context)
    {
        _context = context;
    }

    #region CRUD Usuario
    public async Task<IList<string>> RegistrarUsuario(Usuario usuario)
    {
        var user = await GetByCodUsuario(usuario.COD_USUARIO);

        if(user != null)
        {
            AdicionaErroProcessamento($"Usuário {usuario.COD_USUARIO} já cadastrado na base de dados");

            return Erros;
        }

        //usuario.Criptografy(); Ao cadastrar usuário, não será cadastrada senha!

        _context.Usuarios.Add(usuario);

       return await PersistirDados();
    }

    public async Task<IList<Usuario>> BuscarTodos()
    {
        return await _context.Usuarios.ToListAsync();
    }
    public async Task<Usuario?> GetByCodUsuario(string COD_USUARIO)
    {
        return await _context.Usuarios.Where(x => x.COD_USUARIO == COD_USUARIO.ToUpper()).FirstOrDefaultAsync();
    }

    public async Task<IList<string>> AtualiarUsuario(Usuario usuario)
    {
        var user = await GetByCodUsuario(usuario.COD_USUARIO);

        if(user is null)
        {
            AdicionaErroProcessamento($"Usuário {usuario.COD_USUARIO} não localizado!");
            return Erros;
        }

        user.AtualizarDadosCadastro(usuario);

        _context.Update(user);

        return await PersistirDados();
    }

    public async Task<IList<string>> SetPassword(string COD_USUARIO,string TXT_SENHA)
    {
        var usuario = await GetByCodUsuario(COD_USUARIO);

        if (usuario is null)
        {
            AdicionaErroProcessamento($"Usuário {COD_USUARIO} não localizado!");
            return Erros;
        }

        usuario.CriptografaSenha(TXT_SENHA);

        _context.Update(usuario);

        return await PersistirDados();
    }


    public async Task<IList<string>> DesativarCadastro(string COD_USUARIO)
    {
        var usuario = await GetByCodUsuario(COD_USUARIO);

        if (usuario == null)
        {
            AdicionaErroProcessamento($"Usuário {COD_USUARIO} não localizado!");
            return Erros;
        }

        usuario.DesativaCadastro();

        return await PersistirDados();
    }

    public async Task<IList<string>> ReativarCadastro(string COD_USUARIO)
    {
        var usuario = await GetByCodUsuario(COD_USUARIO);

        if (usuario == null)
        {
            AdicionaErroProcessamento($"Usuário {COD_USUARIO} não localizado!");
            return Erros;
        }

        usuario.AtivarCadastro();

        return await PersistirDados();
    }

    public async Task<IList<string>> DeletarPermanete(string COD_USUARIO)
    {
        var usuario = await GetByCodUsuario(COD_USUARIO);

        if (usuario == null)
        {
            AdicionaErroProcessamento($"Usuário {COD_USUARIO} não localizado!");
            return Erros;
        }

        _context.Remove(usuario);

        return await PersistirDados();
    }
    #endregion

    #region CRUD Permissões Usuario
    public async Task<IList<string>> AdicionarPermissaoUsuario(string COD_USUARIO, Permissao role)
    {
        var usuario = await GetByCodUsuario(COD_USUARIO);

        if(usuario == null)
        {
            AdicionaErroProcessamento($"Usuário {COD_USUARIO} não localizado!");

            return Erros;
        }

        var permissao = await _context.Permissoes.Where(x=> x.COD_PERMISSAO == role.COD_PERMISSAO).FirstOrDefaultAsync();

        if(permissao == null)
        {
            await AdicionarPermissao(role);
        }
        else
        {
            role = permissao;
        }

        var permissaoUsuario = await _context.PermissoesUsuarios.Where(x=> x.PermissaoId == role.ID && x.UsuarioId == usuario.ID).FirstOrDefaultAsync();

        if (permissaoUsuario == null)
        {
            var userRole = new PermissaoUsuario
            {
                ID = new Guid(),
                UsuarioId = usuario.ID,
                PermissaoId = role.ID,
                
            };
            await AdicionarRoleUsuario(userRole);
        }

        return await PersistirDados();
    }

    public async Task<List<PermissaoUsuario>?> BuscarPermissoesUsuario(string COD_USUARIO)
    {
        var usuario = await GetByCodUsuario(COD_USUARIO);

        if (usuario == null)
        {
            return new List<PermissaoUsuario>();
        }

        var retorno = await _context.PermissoesUsuarios
                                        .Where(x => x.UsuarioId == usuario.ID)
                                        .Include(pu=>pu.Permissao)
                                        .ToListAsync();

        return retorno;

    }

    private async Task AdicionarRoleUsuario(PermissaoUsuario role)
    {
        _context.PermissoesUsuarios.Add(role);
    }

    public async Task<IList<string>> RemovePermissaoUsuario(string COD_USUARIO, string COD_PERMISSAO)
    {
        var usuario = await GetByCodUsuario(COD_USUARIO);

        if (usuario == null)
        {
            AdicionaErroProcessamento($"Usuário {COD_USUARIO} não localizado!");

            return Erros;
        }

        var permissao = await _context.Permissoes.Where(x => x.COD_PERMISSAO == COD_PERMISSAO).FirstOrDefaultAsync();

        if (permissao == null)
        {
            AdicionaErroProcessamento($"Permissão {COD_PERMISSAO} não existe!");

            return Erros;
        }

        var permissaoUsuario = await _context.PermissoesUsuarios.Where(x => x.UsuarioId == usuario.ID && x.PermissaoId == permissao.ID).FirstOrDefaultAsync();

        if (permissaoUsuario == null)
        {
            AdicionaErroProcessamento($"Permissão {COD_PERMISSAO} não existe para o usuário {usuario.NOM_USUARIO}!");

            return Erros;
        }

        _context.PermissoesUsuarios.Remove(permissaoUsuario);

        return await PersistirDados();
    }

    #endregion

    #region CRUD Permissões
    private async Task AdicionarPermissao(Permissao role)
    {
        _context.Permissoes.Add(role);
    }

    private async Task RemoverPermissao(Permissao role)
    {
        _context.Permissoes.Remove(role);
    }

    #endregion

    #region Outros
    public void Dispose()
    {
        _context.Dispose();
    }

    private async Task<IList<string>> PersistirDados()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            AdicionaErroProcessamento(ex.Message);
            //AdicionaErroProcessamento($"Não foi possível persistir dados!");
        }

        return Erros;
    }
    
   
    #endregion

    #region Funções para login
    public async Task<Usuario?> VerificarLoginUsuario(string COD_USUARIO, string TXT_SENHA)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.COD_USUARIO == COD_USUARIO.ToUpper());

        return usuario?.ComparaSenha(TXT_SENHA) == true ? usuario : null;

    }
    #endregion
}
