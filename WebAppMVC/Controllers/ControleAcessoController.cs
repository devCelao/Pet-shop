using Infrastructure.Data.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers;

[Authorize]
[Route("controle-acesso")]
public class ControleAcessoController : BaseController
{
    private readonly IControleAcessoService _service;
    public ControleAcessoController(IOptions<ClientSettings> clientSettings, IControleAcessoService service)
        : base(clientSettings)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {

        if (await _service.IsAdmin()) return View();

        AdicionaErroProcessamento("Você não tem permissão para alterar dados de perfil!");
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("buscar-permissoes")]
    public async Task<IActionResult> BuscarDados(ControleAcessoViewModel model)
    {
        if (!ModelState.IsValid) return View("Index", model);

        var usuario = await _service.GetUsuario(model.Username);

        if (usuario == null)
        {
            AdicionaErroProcessamento("Usuário não cadastrado na base de dados.");
            return View("Index");
        }

        return View(await Buscar(model.Username));
    }

    [HttpPost("cadastrar-permissao")]
    public async Task<IActionResult> CadastrarPermissao(ControleAcessoViewModel model)
    {
        if (!ModelState.IsValid) return View("Index", model);

        var role = forAddPermissao(model);

        var erros = await _service.AdicionarPermissao(model.Username, role);

        if (erros.Any())
            AdicionaErroProcessamento($"Não foi possível adicionar permissão {model.Permissao} para o usuário {model.Username}");

        return View("BuscarDados", await Buscar(model.Username));
    }

    [HttpPost("remover-permissao")]
    public async Task<IActionResult> RemoverPermissao(ControleAcessoViewModel model)
    {
        if (!ModelState.IsValid) return View("Index", model);

        var erros = await _service.RemoverPermissao(model.Username, model.Permissao);

        if (erros.Any())
            AdicionaErroProcessamento($"Não foi possível remover permissão {model.Permissao} para o usuário {model.Username}");

        return View("BuscarDados", await Buscar(model.Username));
    }

    private async Task<ControleAcessoViewModel> Buscar(string Username)
    {
        var Permissoes = await _service.BuscarPermissoes(Username);
        
        return new ControleAcessoViewModel
        {
            Username = Username,
            Permissoes = Permissoes
        };

    }

    private Permissao forAddPermissao(ControleAcessoViewModel model)
    {
        return new Permissao
        {
            COD_PERMISSAO = model.Permissao,
            TXT_DESCRICAO = model.Txt_Descricao_Permissao
        };
    }
}
