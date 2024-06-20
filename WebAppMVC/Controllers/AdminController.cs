using Infrastructure.Data.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers;

public class AdminController : Controller
{
    private readonly IControleAcessoRepository acesso;
    public AdminController(IControleAcessoRepository acesso)
    {
        this.acesso = acesso;
    }

    [HttpGet("adminstrador/registro")]
    public async Task<IActionResult> Index()
    {
        var usuario = new Usuario
        {
            ID = new Guid(),
            COD_USUARIO = "ADMIN",
            DT_ALTERACAO = DateTime.Now,
            DT_REGISTRO = DateTime.Now,
            IND_ATIVO = true,
            IND_EMAIL_CONFIRMADO = true,
            NOM_USUARIO = "Administrador",
            TXT_EMAIL = "dev.celao@gmail.com",
            TXT_SENHA = "123456"
        };
        await acesso.RegistrarUsuario(usuario);

        return RedirectToAction("Index", "Account");
    }

    [HttpGet("administrador/permissao")]
    public async Task<IActionResult> Permissao()
    {
        var permissao = new Permissao
        {
            ID = new Guid(),
            COD_PERMISSAO = "ADMIN",
            TXT_DESCRICAO = "Todas as permissões"
        };

        await acesso.AdicionarPermissaoUsuario("ADMIN", permissao);

        return RedirectToAction("Index", "Account");
    }
}
